namespace WatchAllApi.Interfaces
{
    interface IDbConfiguration
    {
        string ConnectionString { get; }
        string Database { get; }
    }
}
