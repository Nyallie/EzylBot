using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Discord;
using Discord.Audio;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using EzylBot.Services;

namespace EzylBot
{
    public class Program
    {
        //private LoggingService loggingService;
        static void Main(string[] args) 
        {
            var bot = new EzylBot();
            bot.Run().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
