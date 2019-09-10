using System.Threading;
using System.Threading.Tasks;
using ElenaHelperBot.Services;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.Enums;

namespace ElenaHelperBot.Handlers
{
    class StartCommand : CommandBase
    {
        private IInlineMarkupService _inlineMarkupService;

        public StartCommand(IInlineMarkupService inlineMarkupService)
        {
            _inlineMarkupService = inlineMarkupService;
        }

        public override async Task HandleAsync(
            IUpdateContext context,
            UpdateDelegate next,
            string[] args,
            CancellationToken cancellationToken
        )
        {
            string text = "Привет! Я искусственный интеллект 🤖," +
                " который помогает разобраться с тем, что такое *Дизайн человека*" +
                " и как получить чтение своего *бодиграфа*.";

            await context.Bot.Client.SendTextMessageAsync(
                context.Update.Message.Chat.Id,
                text,
                ParseMode.Markdown,
                cancellationToken: cancellationToken
            );

            await Task.Delay(500);

            text = "Что вас интересует?";

            var inlineKeyboard = _inlineMarkupService.GetMarkup();

            await context.Bot.Client.SendTextMessageAsync(
                context.Update.Message.Chat.Id,
                text,
                ParseMode.Markdown,
                cancellationToken: cancellationToken,
                replyMarkup: inlineKeyboard
            );

            await next(context, cancellationToken);
        }
    }
}
