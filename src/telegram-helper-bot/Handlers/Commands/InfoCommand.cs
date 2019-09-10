using System.Threading;
using System.Threading.Tasks;
using ElenaHelperBot.Services;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.Enums;

namespace ElenaHelperBot.Handlers
{
    class InfoCommand : CommandBase
    {
        private IInlineMarkupService _inlineMarkupService;

        public InfoCommand(IInlineMarkupService inlineMarkupService)
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
            var text = "Что вас интересует?";
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
