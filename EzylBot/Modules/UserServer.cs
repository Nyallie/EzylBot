using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace EzylBot.Modules
{
    public class UserServer : ModuleBase<SocketCommandContext>
    {
        [Command("avatar")]
        [Summary("Give link to the discord avatar")]
        public async Task Avatar(IGuildUser user=null)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder().WithCurrentTimestamp();
            if (user == null)
            {
                string url = Context.User.GetAvatarUrl(ImageFormat.Auto,4096);
                string name = Context.User.Username;
                embedBuilder.WithTitle(name)
                    .WithDescription($"[Lien direct]({url})")
                    .WithImageUrl(url);
            }
            else
            {
                string url = user.GetAvatarUrl(ImageFormat.Auto, 4096);
                string name = user.Username;
                embedBuilder.WithTitle(name)
                    .WithDescription($"[Lien direct]({url})")
                    .WithImageUrl(url);
            }
            Embed embed = embedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
    }
}
