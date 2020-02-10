using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Generic.UI.Proxy
{
	public static class ProxyObservable
	{
		private const string ConstModuleName = "Notify";

		public const string ConstAssemblyName = "Generic.UI.Proxy.Dynamic";

		private const string ConstTypePrefix = "Generic.UI.Proxy";

		private readonly static ModuleBuilder DynamicModule;

		private readonly static Dictionary<string, Type> TypeCache;

		static ProxyObservable()
		{
			AssemblyName assemblyName = new AssemblyName("Generic.UI.Proxy.Dynamic");
			assemblyName.SetPublicKey(Assembly.GetExecutingAssembly().GetName().GetPublicKey());
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			ProxyObservable.TypeCache = new Dictionary<string, Type>();
			ProxyObservable.DynamicModule = assemblyBuilder.DefineDynamicModule("Notify");
		}

		public static T CastAsObservable<T>(T entity)
		where T : class, new()
		{
			Type item;
			T t;
			if (!typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T)))
			{
				string str = string.Concat("Generic.UI.Proxy", typeof(T).Name);
				lock (ProxyObservable.TypeCache)
				{
					if (!ProxyObservable.TypeCache.ContainsKey(str))
					{
						item = ProxyObservable.MakeDynamicType(str, typeof(T));
						ProxyObservable.TypeCache.Add(str, item);
					}
					else
					{
						item = ProxyObservable.TypeCache[str];
					}
				}
				if (item == null)
				{
					throw new TypeUnloadedException(string.Concat("Can't load Type ", str));
				}
				t = (T)(Activator.CreateInstance(item, new object[] { entity }) as T);
			}
			else
			{
				t = entity;
			}
			return t;
		}

		private static FieldInfo MakeConstructor(TypeBuilder typeBuilder, Type baseType)
		{
			FieldBuilder fieldBuilder = typeBuilder.DefineField("proxiedObj", baseType, FieldAttributes.Private | FieldAttributes.InitOnly);
			ConstructorInfo constructor = baseType.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				throw new Exception("Proxed object don't have constructor with no parameter");
			}
			ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { baseType });
			constructorBuilder.DefineParameter(1, ParameterAttributes.None, "proxiedObj");
			ILGenerator lGenerator = constructorBuilder.GetILGenerator();
			Label label = lGenerator.DefineLabel();
			lGenerator.Emit(OpCodes.Ldarg_0);
			lGenerator.Emit(OpCodes.Call, constructor);
			lGenerator.Emit(OpCodes.Ldarg_1);
			lGenerator.Emit(OpCodes.Brtrue_S, label);
			lGenerator.Emit(OpCodes.Ldstr, "proxiedObj");
			ConstructorInfo constructorInfo = typeof(ArgumentNullException).GetConstructor(new Type[] { typeof(string) });
			if (constructorInfo != null)
			{
				lGenerator.Emit(OpCodes.Newobj, constructorInfo);
				lGenerator.Emit(OpCodes.Throw);
			}
			lGenerator.MarkLabel(label);
			lGenerator.Emit(OpCodes.Ldarg_0);
			lGenerator.Emit(OpCodes.Ldarg_1);
			lGenerator.Emit(OpCodes.Stfld, fieldBuilder);
			lGenerator.Emit(OpCodes.Ret);
			return fieldBuilder;
		}

		private static Type MakeDynamicType(string dynamicTypeName, Type baseType)
		{
			TypeBuilder typeBuilder = ProxyObservable.DynamicModule.DefineType(dynamicTypeName, TypeAttributes.Public | TypeAttributes.Serializable, baseType, new Type[] { typeof(INotifyPropertyChanged) });
			FieldInfo fieldInfo = ProxyObservable.MakeConstructor(typeBuilder, baseType);
			MethodInfo methodInfo = ProxyObservable.MakeOnPropertyChanged(typeBuilder, ProxyObservable.MakePropertyChangedEvent(typeBuilder));
			ProxyObservable.MakeProperties(typeBuilder, fieldInfo, methodInfo, baseType);
			return typeBuilder.CreateType();
		}

		private static void MakeEventMethod(MethodBuilder methodBuilder, FieldBuilder fieldBuilder, MethodInfo compareInfo, MethodInfo addOrRemove)
		{
			methodBuilder.DefineParameter(1, ParameterAttributes.None, "value");
			ILGenerator lGenerator = methodBuilder.GetILGenerator();
			Label label = lGenerator.DefineLabel();
			lGenerator.DeclareLocal(typeof(PropertyChangedEventHandler));
			lGenerator.DeclareLocal(typeof(PropertyChangedEventHandler));
			lGenerator.DeclareLocal(typeof(PropertyChangedEventHandler));
			lGenerator.Emit(OpCodes.Ldarg_0);
			lGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
			lGenerator.Emit(OpCodes.Stloc_0);
			lGenerator.MarkLabel(label);
			lGenerator.Emit(OpCodes.Ldloc_0);
			lGenerator.Emit(OpCodes.Stloc_1);
			lGenerator.Emit(OpCodes.Ldloc_1);
			lGenerator.Emit(OpCodes.Ldarg_1);
			lGenerator.EmitCall(OpCodes.Call, addOrRemove, null);
			lGenerator.Emit(OpCodes.Castclass, typeof(PropertyChangedEventHandler));
			lGenerator.Emit(OpCodes.Stloc_2);
			lGenerator.Emit(OpCodes.Ldarg_0);
			lGenerator.Emit(OpCodes.Ldflda, fieldBuilder);
			lGenerator.Emit(OpCodes.Ldloc_2);
			lGenerator.Emit(OpCodes.Ldloc_1);
			lGenerator.EmitCall(OpCodes.Call, compareInfo, null);
			lGenerator.Emit(OpCodes.Stloc_0);
			lGenerator.Emit(OpCodes.Ldloc_0);
			lGenerator.Emit(OpCodes.Ldloc_1);
			lGenerator.Emit(OpCodes.Bne_Un_S, label);
			lGenerator.Emit(OpCodes.Ret);
		}

		private static MethodInfo MakeOnPropertyChanged(TypeBuilder typeBuilder, FieldInfo eventField)
		{
			MethodInfo method = typeof(PropertyChangedEventHandler).GetMethod("Invoke");
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("OnPropertyChanged", MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.NewSlot, typeof(void), new Type[] { typeof(string) });
			methodBuilder.DefineParameter(1, ParameterAttributes.None, "propertyName");
			ILGenerator lGenerator = methodBuilder.GetILGenerator();
			Label label = lGenerator.DefineLabel();
			lGenerator.Emit(OpCodes.Ldarg_0);
			lGenerator.Emit(OpCodes.Ldfld, eventField);
			lGenerator.Emit(OpCodes.Brfalse_S, label);
			lGenerator.Emit(OpCodes.Ldarg_0);
			lGenerator.Emit(OpCodes.Ldfld, eventField);
			lGenerator.Emit(OpCodes.Ldarg_0);
			lGenerator.Emit(OpCodes.Ldarg_1);
			ConstructorInfo constructor = typeof(PropertyChangedEventArgs).GetConstructor(new Type[] { typeof(string) });
			if (constructor == null)
			{
				throw new Exception("No constructor find in type PropertyChangedEventArgs");
			}
			lGenerator.Emit(OpCodes.Newobj, constructor);
			if (method == null)
			{
				throw new InvalidOperationException();
			}
			lGenerator.EmitCall(OpCodes.Callvirt, method, null);
			lGenerator.MarkLabel(label);
			lGenerator.Emit(OpCodes.Ret);
			return methodBuilder;
		}

		private static void MakeProperties(TypeBuilder typeBuilder, FieldInfo proxiedObj, MethodInfo eventInvoke, Type targetType)
		{
			PropertyInfo[] properties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < (int)properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyInfo.Name, PropertyAttributes.None, propertyInfo.PropertyType, null);
				if ((!propertyInfo.CanRead ? false : propertyInfo.GetGetMethod().IsVirtual))
				{
					string str = string.Concat("get_", propertyInfo.Name);
					MethodBuilder methodBuilder = typeBuilder.DefineMethod(str, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual, propertyInfo.PropertyType, Type.EmptyTypes);
					ILGenerator lGenerator = methodBuilder.GetILGenerator();
					lGenerator.Emit(OpCodes.Ldarg_0);
					lGenerator.Emit(OpCodes.Ldfld, proxiedObj);
					lGenerator.EmitCall(OpCodes.Callvirt, propertyInfo.GetGetMethod(), null);
					lGenerator.Emit(OpCodes.Ret);
					propertyBuilder.SetGetMethod(methodBuilder);
				}
				if ((!propertyInfo.CanWrite ? false : propertyInfo.GetSetMethod().IsVirtual))
				{
					string str1 = string.Concat("set_", propertyInfo.Name);
					MethodBuilder methodBuilder1 = typeBuilder.DefineMethod(str1, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual, typeof(void), new Type[] { propertyInfo.PropertyType });
					methodBuilder1.DefineParameter(1, ParameterAttributes.None, "value");
					ILGenerator lGenerator1 = methodBuilder1.GetILGenerator();
					lGenerator1.Emit(OpCodes.Ldarg_0);
					lGenerator1.Emit(OpCodes.Ldfld, proxiedObj);
					lGenerator1.Emit(OpCodes.Ldarg_1);
					lGenerator1.EmitCall(OpCodes.Callvirt, propertyInfo.GetSetMethod(), null);
					lGenerator1.Emit(OpCodes.Ldarg_0);
					lGenerator1.Emit(OpCodes.Ldstr, propertyInfo.Name);
					lGenerator1.EmitCall(OpCodes.Callvirt, eventInvoke, null);
					lGenerator1.Emit(OpCodes.Ret);
					propertyBuilder.SetSetMethod(methodBuilder1);
				}
			}
		}

		private static FieldInfo MakePropertyChangedEvent(TypeBuilder typeBuilder)
		{
			MethodInfo method = typeof(Delegate).GetMethod("Combine", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(Delegate), typeof(Delegate) }, null);
			MethodInfo methodInfo = typeof(Delegate).GetMethod("Remove", BindingFlags.Static | BindingFlags.Public);
			MethodInfo methodInfo1 = ((IEnumerable<MethodInfo>)typeof(Interlocked).GetMethods(BindingFlags.Static | BindingFlags.Public)).First<MethodInfo>((MethodInfo m) => (!string.Equals(m.Name, "CompareExchange") ? false : m.IsGenericMethod)).MakeGenericMethod(new Type[] { typeof(PropertyChangedEventHandler) });
			FieldBuilder fieldBuilder = typeBuilder.DefineField("PropertyChanged", typeof(PropertyChangedEventHandler), FieldAttributes.Private);
			EventBuilder eventBuilder = typeBuilder.DefineEvent("PropertyChanged", EventAttributes.None, typeof(PropertyChangedEventHandler));
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("add_PropertyChanged", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.NewSlot | MethodAttributes.SpecialName, typeof(void), new Type[] { typeof(PropertyChangedEventHandler) });
			ProxyObservable.MakeEventMethod(methodBuilder, fieldBuilder, methodInfo1, method);
			eventBuilder.SetAddOnMethod(methodBuilder);
			MethodBuilder methodBuilder1 = typeBuilder.DefineMethod("remove_PropertyChanged", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.NewSlot | MethodAttributes.SpecialName, typeof(void), new Type[] { typeof(PropertyChangedEventHandler) });
			ProxyObservable.MakeEventMethod(methodBuilder1, fieldBuilder, methodInfo1, methodInfo);
			eventBuilder.SetRemoveOnMethod(methodBuilder1);
			return fieldBuilder;
		}
	}
}