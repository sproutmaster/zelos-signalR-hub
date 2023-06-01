using StackExchange.Redis;

namespace zelos_hub.Hubs;

using Microsoft.AspNetCore.SignalR;
using zelos_hub.Utilities;
using System;


public class PrinterHub: Hub {

    public StackExchange.Redis.IDatabase? Redis;
    public string? printer_data;
    public bool discconnected = false;

    public override async Task OnConnectedAsync() {

        await base.OnConnectedAsync();

        Console.WriteLine("Client connected");
        Console.WriteLine("Connecting to Redis..");

        Redis = RedisStore.RedisCache;

        Console.WriteLine("Connected to Redis");

        while (true) {

            Console.WriteLine("Sleeping 4 sec");
            Thread.Sleep(4000);

            if (discconnected)
                break;
            
            if (Redis.IsConnected("running")) {
                string? tempData = Redis.StringGet("printer_data");

                if (tempData == printer_data) {
                    Console.WriteLine("No new data");
                    continue;
                }

                printer_data = tempData;

                Console.WriteLine("Sending printer data to client");
                await Clients.All.SendAsync("update_printer", printer_data);
            }

            else 
                Console.WriteLine("Disconnected from Redis");
        }
    }


    public override async Task OnDisconnectedAsync(Exception? exception) {
        discconnected = true;
        Console.WriteLine("Client disconnected");
        await base.OnDisconnectedAsync(exception);

    }

}
