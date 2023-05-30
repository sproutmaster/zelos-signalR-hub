namespace zelos_hub.Utilities;
using StackExchange.Redis;


public class RedisStore
{
    private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

    static RedisStore()
    {
        var configurationOptions = new ConfigurationOptions { EndPoints = { "localhost:6379" }, AbortOnConnectFail = false};

        LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(configurationOptions));
    }

    public static ConnectionMultiplexer Connection => LazyConnection.Value;
    public static IDatabase RedisCache => Connection.GetDatabase();
}
