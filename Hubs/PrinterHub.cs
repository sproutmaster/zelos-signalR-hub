using Microsoft.AspNetCore.SignalR;
using zelos_hub.Utilities;
namespace zelos_hub.Hubs;
public class PrinterHub: Hub {
    public async Task SendMessage(string user, string message) {
        
        // var redis = RedisStore.RedisCache;
        // if(redis.StringSet("printer_data", "testValue")) {
        //     var val = redis.StringGet("testKey");
        //     Console.WriteLine(val);
        // }
        
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
