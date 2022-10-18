using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using EzylBot.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Threading.Tasks;
using RunMode = Discord.Commands.RunMode;

namespace EzylBot
{
    public class EzylBot
    {
        private string _token;
        private DiscordSocketClient _client;
        private DiscordSocketConfig _clientConfig;
        private CommandService _command;
        private InteractionService _interaction;
        private IServiceProvider _services;
        private CommandHandler _commandHandler;
        private CommandServiceConfig _commandConfig;
        private InteractionHandler _interactionHandler;

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
                GatewayIntents = GatewayIntents.AllUnprivileged | 
                GatewayIntents.Guilds | 
                GatewayIntents.GuildMembers |
                GatewayIntents.GuildMessageReactions |
                GatewayIntents.GuildMessages |
                GatewayIntents.GuildVoiceStates,
                AlwaysDownloadUsers = true,
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 10000,
                DefaultRetryMode = RetryMode.AlwaysFail
            };
            _client = new DiscordSocketClient(_clientConfig);
            _command = new CommandService(_commandConfig);
            _interaction = new InteractionService(_client);
            _token = ConfigurationManager.AppSettings.Get("token");
            _services = new ServiceCollection()
                    .AddSingleton<DiscordSocketClient>(_client)
                    .AddSingleton<CommandService>(_command)
                    .AddSingleton<InteractionService>(_interaction)
                    .BuildServiceProvider();
            _commandHandler = new CommandHandler(_client, _command, _services);
            _interactionHandler = new InteractionHandler(_client,_interaction, _services);
            await _commandHandler.InstallCommandAsync();
            await _interactionHandler.InitializeAsync();
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            await _client.SetActivityAsync(new Game("Watching people...", ActivityType.Playing, ActivityProperties.None));
            await Task.Delay(-1);
        }
    }
}
