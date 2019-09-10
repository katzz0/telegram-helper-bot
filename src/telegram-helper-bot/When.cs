using System.Linq;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.Enums;

namespace ElenaHelperBot
{
    public static class When
    {
        public static bool NewMessage(IUpdateContext context) =>
            context.Update.Message != null;

        public static bool NewTextMessage(IUpdateContext context) =>
            context.Update.Message?.Text != null;

        public static bool NewCommand(IUpdateContext context) =>
            context.Update.Message?.Entities?.FirstOrDefault()?.Type == MessageEntityType.BotCommand;

        public static bool CallbackQuery(IUpdateContext context) =>
            context.Update.CallbackQuery != null;

        public static bool NotifierMessage(IUpdateContext context) =>
            context.Update.Message?.From.Id == null;
    }
}