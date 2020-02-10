namespace Hao.Launcher.Data
{
	public enum StatusCodeEnum
	{
		Success = 200,
		ParameterError = 400,
		Unauthorized = 401,
		TokenInvalid = 403,
		HttpMehtodError = 405,
		HttpRequestError = 406,
		URLExpireError = 407,
		MethodError = 408,
		Error = 500
	}
}