using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EzylBot.Modules
{
    [Group("music")]
    [Alias("m")]
    public class Music: ModuleBase<SocketCommandContext>
    {
        private readonly Color _color = new Color(96, 68, 219);

        [Command("join", RunMode = RunMode.Async)]
        [Summary("Make the bot join the vocal channel you're in.")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null) { 
                await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); 
                return; 
            }

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            var audioClient = await channel.ConnectAsync();
            var EmbedBuilder = new EmbedBuilder()
                .WithDescription(":white_check_mark: Join channel :speaker: : ``" + channel.Name+"``.")
                .WithColor(_color);
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }

        [Command("leave", RunMode = RunMode.Async)]
        [Summary("Make the Bot leave a vocal channel.")]
        public async Task LeaveChannel(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null)
            {
                await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument.");
                return;
            }
            await channel.DisconnectAsync();
            var EmbedBuilder = new EmbedBuilder()
                .WithDescription("Leave channel vocal.")
                .WithColor(_color);
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
    }
}
