using Discord;
using Discord.Commands;
using EzylBot.Services;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace EzylBot.Modules
{
    public class React : ModuleBase<SocketCommandContext>
    {
        private static readonly string _baseurlpurr = "https://purrbot.site/api/img/sfw/";
        private static readonly string _baseurlneko = "https://nekos.life/api/v2/img/";
        private readonly Color _color = new Color(169, 9, 208);

        private async Task SendGif(string gifurl,string message,string api)
        {
            Requethttp req = new Requethttp();
            string giflink = "";
            switch (api)
            {
                case "neko":
                    giflink = await req.GetLinkImageNeko(gifurl);
                    break;
                case "purr":
                    giflink = await req.GetLinkImagePurr(gifurl);
                    break;
            }
            var EmbedBuilder = new EmbedBuilder()
                .WithDescription(message)
                .WithImageUrl(giflink)
                .WithColor(_color)
                .WithCurrentTimestamp()
                .WithFooter(footer =>
                {
                    footer
                    .WithText($"Powered by {api} Api");
                });
            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }

        [Command("bite")]
        [Summary("Bite someone or get bite by the bot")]
        public async Task BiteReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "bite/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} has bitten {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"You want to be bite {Context.User.Mention} ? Okay...";
                }
            }
            else
            {
                message = $"{Context.User.Mention} has bitten {user.Mention}";
            }
            await SendGif(url, message,"purr");
        }

        [Command("comfy")]
        [Summary("Only comfy !!!!")]
        public async Task ComfyReact(IGuildUser user = null)
        {
            string url = _baseurlpurr + "comfy/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.Message.ReferencedMessage.Author.Mention} look ! {Context.User.Mention} is really comfy !!!";
                }
                else
                {
                    message = $"{Context.User.Mention} is really comfy....";
                }
            }
            else
            {
                message = $"{user.Mention} look ! {Context.User.Mention} is really comfy !!!";
            }
            await SendGif(url, message, "purr");
        }

        [Command("smug")]
        [Summary("am very smug")]
        public async Task SmugReac(IGuildUser user = null)
        {
            string url = _baseurlneko + "smug";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.Message.ReferencedMessage.Author.Mention}, {Context.User.Mention} look very smug now... OwO";
                }
                else
                {
                    message = $"{Context.User.Mention} look very smug";
                }
            }
            else
            {
                message = $"{user.Mention},{Context.User.Mention} look very smug now... OwO";
            }
            await SendGif(url, message, "neko");
        }

        [Command("foxgirl")]
        [Summary("just foxgirl picture")]
        public async Task FoxPic(IGuildUser user = null)
        {
            string url = _baseurlneko + "fox_girl";
            string message = "OwO Fox girl";
            await SendGif(url, message, "neko");
        }

        [Command("catgirl")]
        [Summary("just nekogirl picture")]
        public async Task NekoPic(IGuildUser user = null)
        {
            string url = _baseurlneko + "ngif";
            string message = "UwU Cat girl";
            await SendGif(url, message, "neko");
        }

        [Command("cuddle")]
        [Summary("Cuddle someone or be cuddle by the bot")]
        public async Task CuddleReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "cuddle/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} has cuddled {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"You want some cuddle {Context.User.Mention} ? Okay... ";
                }
            }
            else
            {
                message = $"{Context.User.Mention} has cuddled {user.Mention}";
            }
            
            await SendGif(url, message, "purr");
        }

        [Command("cry")]
        [Summary("crying stuff")]
        public async Task CryReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "cry/gif";
            string message = "";
            if (user == null)
            {

                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} is crying because of {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"{Context.User.Mention} is crying...";
                }
            }
            else
            {
                message = $"{Context.User.Mention} is crying because of {user.Mention}";
            }
            await SendGif(url, message, "purr");
        }

        [Command("dance")]
        [Summary("dance gif !")]
        public async Task DanceReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "dance/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} dance with {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"{Context.User.Mention} is dancing !";
                }
            }
            else
            {
                message = $"{Context.User.Mention} dance with {user.Mention} !";
            }
            await SendGif(url, message, "purr");
        }

        [Command("feed")]
        [Summary("I feed you or you feed someone")]
        public async Task FeedReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "feed/gif";
            string message = "";
            if (user == null)
            {

                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} has fed {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"Okay... I will feed you, {Context.User.Mention}";
                }
            }
            else
            {
                message = $"{Context.User.Mention} has fed {user.Mention}";
            }
            await SendGif(url, message, "purr");
        }

        [Command("hug")]
        [Summary("I hug you or you can hug someone")]
        public async Task HugReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "hug/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} has hugged {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"You... You need an hug ? {Context.User.Mention} ? Okay... There you go";
                }
            }
            else
            {
                message = $"{Context.User.Mention} has hugged {user.Mention}";
            }
            await SendGif(url, message, "purr");
        }

        [Command("kiss")]
        [Summary("You realy want that ? Hentai-kun!")]
        public async Task KissReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "kiss/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} has kissed {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"You... You really want that ? {Context.User.Mention} ? Okay...";
                }
            }
            else
            {
                message = $"{Context.User.Mention} has kissed {user.Mention}";
            }
            await SendGif(url, message, "purr");
        }

        [Command("lick")]
        [Summary("Uuhh ???")]
        public async Task LickReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "lick/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} has licked {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"You... What ? {Context.User.Mention} ? Are ... Are you okay ?";
                }
            }
            else
            {
                message = $"{Context.User.Mention} has licked {user.Mention}";
            }
            await SendGif(url, message, "purr");
        }

        [Command("pat")]
        [Summary("Give a headpat at someone or the bot will give it to you")]
        public async Task PatReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "pat/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} has headpated {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"You... You need an headpat, {Context.User.Mention} ?\n Okay... There there, you did well <3";
                }
            }
            else
            {
                message = $"{Context.User.Mention} has headpated {user.Mention}";
            }
            await SendGif(url, message, "purr");
        }

        [Command("poke")]
        [Summary("Poke or be poked")]
        public async Task PokeReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "poke/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} has pocked {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"You... Are you here {Context.User.Mention} ? Hi hi hi... ";
                }
            }
            else
            {
                message = $"{Context.User.Mention} has pocked {user.Mention}";
            }
            await SendGif(url, message, "purr");
        }

        [Command("slap")]
        [Summary("Slap someone or be slapped")]
        public async Task SlapReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "slap/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"You did something bad {Context.Message.ReferencedMessage.Author.Mention} don't you ?\n {Context.User.Mention} has slapped you...";
                }
                else
                {
                    message = $"You... You did a bad thing don't you, {Context.User.Mention} ?";
                }
            }
            else
            {
                message = $"You did something bad {user.Mention} don't you ?\n {Context.User.Mention} has slapped you...";
            }
            await SendGif(url, message, "purr");
        }

        [Command("tickle")]
        [Summary("Tickle someone or be tickeled")]
        public async Task TickleReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "tickle/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} has tickled {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"Hey..., {Context.User.Mention}... Tickle tickle tickle !!";
                }
            }
            else
            {
                message = $"{Context.User.Mention} has tickled {user.Mention}";
            }
            await SendGif(url, message, "purr");
        }

        [Command("blush")]
        [Summary("blush because of someone with a gif")]
        public async Task BlushReac(IGuildUser user = null)
        {
            string url = _baseurlpurr + "blush/gif";
            string message = "";
            if (user == null)
            {
                if (Context.Message.ReferencedMessage != null)
                {
                    message = $"{Context.User.Mention} is blushing because of {Context.Message.ReferencedMessage.Author.Mention}";
                }
                else
                {
                    message = $"{Context.User.Mention} is blushing...";
                }
            }
            else
            {
                message = $"{Context.User.Mention} made {user.Mention} blush";
            }
            await SendGif(url, message, "purr");
        }
    }
}
