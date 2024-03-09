using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using EzylBot.Services.Metier;
using Newtonsoft.Json;

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

        [Command("urban")]
        [Summary("Get the urban def of any word you want")]
        public async Task Urban([Remainder]string word = null)
        {
            UrbanList list;
            EmbedBuilder embed = new EmbedBuilder();
            EmbedAuthorBuilder author = new EmbedAuthorBuilder()
                .WithName(Context.Client.CurrentUser.Username)
                .WithIconUrl(Context.Client.CurrentUser.GetAvatarUrl());
            EmbedFooterBuilder embedFooter = new EmbedFooterBuilder().WithText("I worked 40 min on that because of that fucking API");
            if (word == null)
            {
                await ReplyAsync("What the fuck do you wanna know ?????? I can't search stuff like that");
            }
            else
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.urbandictionary.com/v0/define?term=" + word);
                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    string result = await content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<UrbanList>(result);
                    Random random = new Random();
                    Urban urban= list.List[random.Next(1,list.List.Count)];
                    string def= urban.Definition.Replace("[","").Replace("]","");
                    string ex = urban.Example.Replace("[", "").Replace("]", "");
                    embed
                        .WithTitle(urban.Word)
                        .WithUrl(urban.Permalink)
                        .WithDescription(def)
                        .AddField("Example :",ex)
                        .WithAuthor(author)
                        .WithCurrentTimestamp()
                        .WithColor(_color)
                        .WithFooter(embedFooter);
                    Embed embedF = embed.Build();
                    await ReplyAsync("UrbanDictionary result :", embed: embedF);
                }
                else
                {
                    await ReplyAsync("Urban dictionary didn't worked, sorry");
                }
            }
        }


    }
}
