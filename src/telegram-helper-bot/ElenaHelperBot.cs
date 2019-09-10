using Microsoft.Extensions.Options;
using Telegram.Bot.Framework;

namespace ElenaHelperBot
{
    public class ElenaHelperBot : BotBase
    {
        public ElenaHelperBot(IOptions<BotOptions<ElenaHelperBot>> options)
            : base(options.Value)
        {
        }
    }
}
