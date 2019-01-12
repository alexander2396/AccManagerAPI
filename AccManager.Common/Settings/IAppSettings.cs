namespace AccManager.Common.Settings
{
    public interface IAppSettings
    {
        ConnectionStrings ConnectionStrings { get; }
        AuthOptions AuthOptions { get; }
    }
}
