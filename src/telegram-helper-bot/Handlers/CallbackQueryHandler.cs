using System;
using System.Threading;
using System.Threading.Tasks;
using ElenaHelperBot.Constants;
using ElenaHelperBot.Services;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ElenaHelperBot.Handlers
{
    public class CallbackQueryHandler : IUpdateHandler
    {
        private IDescriptionTextService _descriptionTextService;
        private IInlineMarkupService _inlineMarkupService;

        public CallbackQueryHandler(
            IInlineMarkupService inlineMarkupService,
            IDescriptionTextService descriptionTextService
        ) {
            _inlineMarkupService = inlineMarkupService;
            _descriptionTextService = descriptionTextService;
        }

        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            CallbackQuery cq = context.Update.CallbackQuery;

            await context.Bot.Client.AnswerCallbackQueryAsync(cq.Id, cq.Data);

            var callbackButtonValue = ParseCallbackValue(cq.Data);

            if (callbackButtonValue != InlineButtonValue.Default)
            {
                await ProcessCallbackQueryAsync(context.Bot.Client, cq.Message.Chat.Id, cq.Message.MessageId, callbackButtonValue);
            }

            await next(context, cancellationToken);
        }

        private InlineButtonValue ParseCallbackValue(string callbackData)
        {
            Enum.TryParse(callbackData, out InlineButtonValue callbackButtonValue);

            return callbackButtonValue;
        }

        private async Task<Message> ProcessCallbackQueryAsync(Telegram.Bot.ITelegramBotClient client, long chatId, int messageId, InlineButtonValue callbackButtonValue)
        {
            var descriptionText = _descriptionTextService.GetDescriptionText(callbackButtonValue);
            var queryButtonText = _inlineMarkupService.GetButtonText(callbackButtonValue);
            var inlineKeyboard = _inlineMarkupService.GetMarkup(callbackButtonValue);
            var defaultText = "Что вам интересно?";

            if (callbackButtonValue == InlineButtonValue.IWantToOrder)
            {
                await client.EditMessageReplyMarkupAsync(chatId, messageId, null);
                return await client.SendTextMessageAsync(chatId, descriptionText, replyMarkup: inlineKeyboard);
            }

            if (callbackButtonValue == InlineButtonValue.IHavePaid)
            {
                await client.EditMessageReplyMarkupAsync(chatId, messageId, null);
                await client.SendTextMessageAsync(chatId, "Спасибо! Теперь свяжитель с Еленой для того чтобы выбрать удобное для вас время.");

                await Task.Delay(2000);

                return await client.SendTextMessageAsync(chatId, "Отправьте команду /info если хотите увидеть доступные опции снова.");
            }

            var text = string.IsNullOrEmpty(descriptionText) ? defaultText : $"_{queryButtonText}_\n{descriptionText}";

            return await client.EditMessageTextAsync(
                chatId,
                messageId,
                text,
                ParseMode.Markdown,
                replyMarkup: inlineKeyboard
            );
        }
    }
}
