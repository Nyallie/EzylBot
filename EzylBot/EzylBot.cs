using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace EzylBot
{
    public class EzylBot
    {
        private string _token;
        private DiscordSocketClient _client;
        private DiscordSocketConfig _clientConfig;
        private CommandService _command;
        private IServiceProvider _services;
        private CommandHandler _commandHandler;
        private CommandServiceConfig _commandConfig;

        public async Task Run()
        {
            _commandConfig = new CommandServiceConfig()
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                IgnoreExtraArgs = true,
                LogLevel = LogSeverity.Info
            };
            _clientConfig = new DiscordSocketConfig()
            {
                AlwaysDownloadUsers = false,
                ExclusiveBulkDelete = false,
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 100,
                DefaultRetryMode = RetryMode.AlwaysFail
            };
            _client = new DiscordSocketClient(_clientConfig);
            _command = new CommandService(_commandConfig);
            _token = ConfigurationManager.AppSettings.Get("token");
            _services = new ServiceCollection()
                    .AddSingleton<DiscordSocketClient>(_client)
                    .AddSingleton<CommandService>(_command)
                    .BuildServiceProvider();
            //loggingService = new LoggingService(_client, _command);
            _commandHandler = new CommandHandler(_client, _command, _services);
            await _commandHandler.InstallCommandAsync();
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            await _client.SetActivityAsync(new Game("Watching AethelMeth people...", ActivityType.Playing, ActivityProperties.None));
            await Task.Delay(-1);
        }
    }
}
