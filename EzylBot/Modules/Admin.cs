using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzylBot.Modules
{
    public class Admin: ModuleBase<SocketCommandContext>
    {
        private static ulong muteGetMutedrole = 851938008755994695;
        private static ulong memberrole = 731953332909768794;
        private static Color _color=new Color(156, 40, 40);

        [Command("1984")]
        [Summary("1984 someone pinged")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task MuteUser(IGuildUser user=null)
        {
            if (user == null) 
            { 
                await ReplyAsync("You need to specify a User.");
            }
            else {
                await user.AddRoleAsync(muteGetMutedrole);
                await user.RemoveRoleAsync(memberrole);
                EmbedBuilder embedBuilder = new EmbedBuilder()
                    .WithCurrentTimestamp()
                    .WithTitle("1984")
                    .WithDescription($"Get Muted {user.Mention}!")
                    .WithImageUrl("https://cdn.discordapp.com/attachments/803282469717540866/811883605399568434/GifMeme16543118022021.gif")
                    .WithColor(_color);
                Embed embed = embedBuilder.Build();
                await ReplyAsync(embed: embed);
            }
        }

        [Command("uncoom")]
        [Summary("UnMute a specified user.")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task UnMuteUser(IGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync("You need to specify a User.");
            }
            else
            {
                await user.AddRoleAsync(memberrole);
                await user.RemoveRoleAsync(muteGetMutedrole);
                EmbedBuilder embedBuilder = new EmbedBuilder()
                    .WithCurrentTimestamp()
                    .WithDescription($"Get UnCoomed {user.Mention}!")
                    .WithImageUrl("https://cdn.discordapp.com/attachments/659503733352169482/847454710243786752/pat.jpg")
                    .WithColor(_color);
                Embed embed = embedBuilder.Build();
                await ReplyAsync(embed: embed);
            }
        }

        [Command("purge")]
        [Summary("Removes X messages from the current channel.")]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task PurgeAsync(int amount)
        {
            // Check if the amount provided by the user is positive.
            if (amount <= 0)
            {
                await ReplyAsync("The amount of messages to remove must be positive.");
                return;
            }
            // Download X messages starting from Context.Message, which means
            // that it won't delete the message used to invoke this command.
            var messages = await Context.Channel.GetMessagesAsync(Context.Message, Direction.Before, amount).FlattenAsync();
            // Ensure that the messages aren't older than 14 days,
            // because trying to bulk delete messages older than that
            // will result in a bad request.
            var filteredMessages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14);
            // Get the total amount of messages.
            var count = filteredMessages.Count();
            // Check if there are any messages to delete.
            if (count == 0)
            {
                await ReplyAsync("Nothing to delete.");
            }
            else
            {
                // The cast here isn't needed if you're using Discord.Net 1.x,
                // but I'd recommend leaving it as it's what's required on 2.x, so
                // if you decide to update you won't have to change this line.
                await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);
                var EmbedBuilder = new EmbedBuilder()
                    .WithTitle("Message Deletion.")
                    .WithDescription($"Done. Removed {count} {(count > 1 ? "messages" : "message")}.")
                    .WithThumbnailUrl("https://cdn.discordapp.com/emojis/786364570343571527.png?v=1")
                    .WithColor(_color)
                    .WithCurrentTimestamp();
                Embed embed = EmbedBuilder.Build();
                await ReplyAsync(embed: embed);
            }
        }

        [Command("ban")]
        [Summary("Ban la personnes mentionné dans l'appel de la commande.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task Ban(IGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null)
            {
                await ReplyAsync("You need to specify a user.");
            }
            else 
            {
                if (reason == null) reason = "No reason specified.";
                SocketGuild guild = ((SocketGuildChannel)Context.Message.Channel).Guild;
                IEmote emote = guild.Emotes.First(e => e.Name == "GunRushia");
                EmbedBuilder embedBuilder = new EmbedBuilder()
                    .WithCurrentTimestamp()
                    .WithDescription($"{user.Mention} a été banni. {emote}")
                    .AddField("Raison :", reason);
                EmbedAuthorBuilder embedAuthor = new EmbedAuthorBuilder()
                    .WithName(Context.User.Mention)
                    .WithIconUrl(Context.User.GetAvatarUrl());
                embedBuilder.WithAuthor(embedAuthor);
                Embed embed = embedBuilder.Build();
                await ReplyAsync(embed: embed);
                await Context.Guild.AddBanAsync(user, 0, reason);
            }
        }
    }
}
