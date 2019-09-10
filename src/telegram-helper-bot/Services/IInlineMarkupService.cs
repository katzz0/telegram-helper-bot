using ElenaHelperBot.Constants;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElenaHelperBot.Services
{
    public interface IInlineMarkupService
    {
        bool IsInlineButtonsUpdateQuery(InlineButtonValue callbackValue = InlineButtonValue.Default);
        string GetButtonText(InlineButtonValue callbackValue = InlineButtonValue.Default);
        InlineKeyboardMarkup GetMarkup(InlineButtonValue callbackValue = InlineButtonValue.Default);
    }
}
