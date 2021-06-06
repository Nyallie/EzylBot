﻿using System;
using System.Collections.Generic;
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
            await ReplyAsync(message);
        }

        [Command("amongus")]
        [Summary("Lancer un evenement among us")]
        public async Task AmongUs(string heure=null, [Remainder]string extraDescrip=null)
        {
            if (heure == null) await ReplyAsync("Need an hour");
            EmbedBuilder embedBuilder = new EmbedBuilder();
            EmbedFooterBuilder footerBuilder = new EmbedFooterBuilder();
            SocketGuild guild = ((SocketGuildChannel)Context.Message.Channel).Guild;
            IEmote emote = guild.Emotes.First(e => e.Name == "SUS");
            IRole role = guild.Roles.First(e => e.Id == 822976056566349826);
            footerBuilder.WithText(Context.User.Username).WithIconUrl(Context.User.GetAvatarUrl());
            embedBuilder.WithTitle($"AMONG US")
                .WithDescription($"{role.Mention} à {heure}! \n" +
                $"Réagir avec {emote} pour vous inscrire (vous avez 2h pour réagir)!\n" +
                $"S'il on est plus de 8, on va sur Airship." +
                $"\n*Mp {Context.User.Mention} si vous voulez join en cour de route.*");
            string message = "- Avoir [Better CrewLink](https://github.com/OhMyGuus/BetterCrewLink/releases/) **à jour**, " +
                "ou lancer BetterCrewLink.\n" +
                "- Pas de Team avec les imposteur!\n " +
                "- Avoir un micro. (On s'en fous de la qualité du moment où l'on vous entends.)";
            if (extraDescrip != null) message = message + "\n - " + extraDescrip;
            embedBuilder.AddField("Important !", message);
            embedBuilder.WithFooter(footerBuilder).WithCurrentTimestamp();
            Embed embed = embedBuilder.Build();
            IMessage message1 = await ReplyAsync(embed: embed);
            await message1.AddReactionAsync(emote);
        }
    }
}
