using System;
using System.Collections.Generic;
using System.Linq;

namespace Hao.Geometry.Shapes.OpenCascade
{
	// Token: 0x0200001A RID: 26
	public class Face
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005C19 File Offset: 0x00003E19
		public Face(Orientation orientation, ISurface surface, IEnumerable<Wire3> wires)
		{
			this.Orientation = orientation;
			this.Surface = surface;
			this.Wires = new List<Wire3>(wires).AsReadOnly();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005C40 File Offset: 0x00003E40
		public Face(Plane3 plane)
		{
			AffineTransform3 affineTransform = Transform3Factory.CreateOrthonormalBasis(plane.Normal);
			this.Surface = new PlaneSurface(plane.Normal * plane.Constant, affineTransform.AxisX, affineTransform.AxisY, true);
			this.Orientation = Orientation.Forward;
			this.Wires = new List<Wire3>().AsReadOnly();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public Face(Cylinder3 cylinder)
		{
			AffineTransform3 affineTransform = Transform3Factory.CreateOrthonormalBasis(cylinder.Axis.Direction);
			this.Surface = new CylinderSurface(cylinder.Axis.Origin, affineTransform.AxisX, affineTransform.AxisY, true, cylinder.Radius);
			this.Orientation = Orientation.Forward;
			this.Wires = new List<Wire3>().AsReadOnly();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005D14 File Offset: 0x00003F14
		public Face(Torus3 torus, AffineTransform3 location)
		{
			this.Surface = new TorusSurface(location.Origin, location.AxisX, location.AxisY, true, torus.OuterRadius, torus.InnerRadius);
			this.Orientation = Orientation.Forward;
			this.Wires = new List<Wire3>().AsReadOnly();
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005D70 File Offset: 0x00003F70
		public Plane3? Plane
		{
			get
			{
				PlaneSurface planeSurface = this.Surface as PlaneSurface;
				if (planeSurface == null)
				{
					return null;
				}
				return new Plane3?(new Plane3(planeSurface.Normal, planeSurface.Origin));
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005DAC File Offset: 0x00003FAC
		public Cylinder3? Cylinder
		{
			get
			{
				CylinderSurface cylinderSurface = this.Surface as CylinderSurface;
				if (cylinderSurface == null)
				{
					return null;
				}
				return new Cylinder3?(new Cylinder3(new Line3(cylinderSurface.Origin, cylinderSurface.Direction), cylinderSurface.Radius, double.MaxValue));
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005DFC File Offset: 0x00003FFC
		public AxisAlignedBox3? BoundingBox
		{
			get
			{
				AxisAlignedBox3? result = null;
				foreach (Wire3 wire in this.Wires)
				{
					foreach (Edge3 edge in wire.Edges)
					{
						AxisAlignedBox3? boundingBox = edge.BoundingBox;
						if (boundingBox != null)
						{
							if (result != null)
							{
								result = new AxisAlignedBox3?(result.Value.CreateMergedWith(boundingBox.Value));
							}
							else
							{
								result = boundingBox;
							}
						}
					}
				}
				return result;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005EB4 File Offset: 0x000040B4
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00005EBC File Offset: 0x000040BC
		public Orientation Orientation { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005EC5 File Offset: 0x000040C5
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00005ECD File Offset: 0x000040CD
		public ISurface Surface { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005ED6 File Offset: 0x000040D6
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00005EDE File Offset: 0x000040DE
		public IList<Wire3> Wires { get; private set; }

		// Token: 0x06000151 RID: 337 RVA: 0x00005EE8 File Offset: 0x000040E8
		public Torus3? TryGetTorus(out AffineTransform3 location)
		{
			TorusSurface torusSurface = this.Surface as TorusSurface;
			if (torusSurface != null)
			{
				AffineTransform3 affineTransform = Transform3Factory.CreateOrthonormalBasis(torusSurface.UnitZ);
				location = new AffineTransform3(affineTransform.AxisX, affineTransform.AxisY, affineTransform.AxisZ, torusSurface.Origin);
				return new Torus3?(new Torus3(torusSurface.MajorRadius, torusSurface.MinorRadius));
			}
			SurfaceOfRevolution surfaceOfRevolution = this.Surface as SurfaceOfRevolution;
			if (surfaceOfRevolution != null)
			{
				CircleCurve3 circleCurve = surfaceOfRevolution.Meridian as CircleCurve3;
				if (circleCurve != null)
				{
					UnitVector3 normalized = (circleCurve.Origin - surfaceOfRevolution.Origin).GetNormalized();
					UnitVector3 axisY = surfaceOfRevolution.Direction.UnitCross(normalized);
					location = new AffineTransform3(normalized, axisY, surfaceOfRevolution.Direction, surfaceOfRevolution.Origin);
					return new Torus3?(new Torus3((surfaceOfRevolution.Origin - circleCurve.Origin).Length, circleCurve.Radius));
				}
			}
			location = AffineTransform3.Identity;
			return null;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005FF8 File Offset: 0x000041F8
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((Face)obj);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006020 File Offset: 0x00004220
		public bool Equals(Face other)
		{
			return this.Orientation.Equals(other.Orientation) && this.Surface.Equals(other.Surface) && this.Wires.SequenceEqual(other.Wires);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006074 File Offset: 0x00004274
		public override int GetHashCode()
		{
			return this.Orientation.GetHashCode() ^ this.Surface.GetHashCode() ^ this.Wires.GetHashCode();
		}
	}
}
