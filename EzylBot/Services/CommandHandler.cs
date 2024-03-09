using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Discord;
using EzylBot.Services;

namespace EzylBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly LoggingService _log;
        private readonly IServiceProvider _services;

        public CommandHandler(DiscordSocketClient client, CommandService command, IServiceProvider service)
        {
            _command = command;
            _client = client;
            _services = service;
            _log = new LoggingService(client, command);
        }

        public async Task InstallCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            _command.CommandExecuted += OnCommandExecutedAsync;
            await _command.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),services: _services);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            string prefix = ConfigurationManager.AppSettings.Get("prefix");
            if (!(message.HasStringPrefix(prefix, ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;
            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _command.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
        public async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!string.IsNullOrEmpty(result?.ErrorReason))
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
            var commandName = command.IsSpecified ? command.Value.Name : "A command";
            await _log.LogAsync(new LogMessage(LogSeverity.Info,
                "CommandExecution",
                $"{commandName} was executed at {DateTime.Now} by {context.User.Username}.\n {context.Message}"));
        }
    }
}
