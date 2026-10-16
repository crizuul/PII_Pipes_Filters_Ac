using System;
using System.Drawing;
using Ucu.Poo.Discord;

namespace Ucu.Poo.PipesAndFilters.Filters
{
    /// <summary>
    /// Filtro que envía la imagen a un canal de Discord.
    /// </summary>
    public class FilterDiscord : IFilter
    {
        private DiscordClient discord;
        private ulong channelId;
        public FilterDiscord(string token, string channelId)
        {
            string botToken = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
            string channelText = Environment.GetEnvironmentVariable("CHANNEL_ID");
            if (string.IsNullOrWhiteSpace(botToken) || string.IsNullOrWhiteSpace(channelText))
            {
                Console.WriteLine("Faltan las variables de entorno 'DISCORD_BOT_TOKEN' y 'CHANNEL_ID'.");
                return;
            }

            this.channelId = ulong.Parse(channelText);
            this.discord = new DiscordClient();

            discord.Login(botToken);
        }

        public IPicture Filter(IPicture image)
        {
            PictureProvider provider = new PictureProvider();
            provider.SavePicture(image, "tmp.jpg");
            discord.SendImage(channelId, "tmp.jpg", "¡Nueva imagen filtrada!");
            return image;
        }
    }
}