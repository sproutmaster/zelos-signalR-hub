using StackExchange.Redis;

namespace zelos_hub.Hubs;

using Microsoft.AspNetCore.SignalR;
using zelos_hub.Utilities;
using System;


public class PrinterHub: Hub {

    public StackExchange.Redis.IDatabase? Redis;
    public string? printer_data;

    public override async Task OnConnectedAsync() {

        await base.OnConnectedAsync();

        Redis = RedisStore.RedisCache;

        while (true) {

            Thread.Sleep(3000);

            if (Redis.IsConnected("printer_data"))
            {
                if (Redis.StringGet("printer_data") == printer_data)
                    continue;

                printer_data = Redis.StringGet("printer_data");
                Console.WriteLine(printer_data);
                await Clients.All.SendAsync("printer_data", printer_data);
            }

            else {
                Console.WriteLine("Redis is not connected");
            }
        }
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

}
