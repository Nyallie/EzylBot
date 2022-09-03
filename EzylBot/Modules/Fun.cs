using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace EzylBot.Modules
{
    public class Fun : ModuleBase<SocketCommandContext>
    {
        private Color _color =new Color(0,0,0);

        [Command("say")]
        [Summary("Make the bot say something...")]
        public async Task Say([Remainder]string message=null)
        {
            if (message == null)
            {
                await ReplyAsync("Please say something after the command you bitch...");
            }
            else 
            {
                await ReplyAsync(message);
                await Context.Message.DeleteAsync();
            }
        }
    }
}
