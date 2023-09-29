namespace Identity.Domain.Constants
{
    /// <summary>
    /// Controller names
    /// </summary>
    public static class ControllerName
    {
        public const string Home = "Home";
        public const string User = "User";
        public const string Authorize = "Authorize";
        public const string Error = "Error";
    }

    /// <summary>
    /// Action names
    /// </summary>
    public static class ActionName
    {
        public const string Index = "Index";
        public const string SignIn = "SignIn";
        public const string AccessDenied = "AccessDenied";
        public const string Handle = "Handle";
        public const string NotFound = "NotFound";
        public const string ResetPassword = "ResetPassword";
    }

    /// <summary>
    /// Route names
    /// </summary>
    public static class RouteName
    {
        public const string Controller = "Controller";
        public const string Action = "Action";
    }

    /// <summary>
    /// Route parameter names
    /// </summary>
    public static class RouteParameterName
    {
        public const string StatusCode = "StatusCode";
        public const string Id = "Id";
    }
}
