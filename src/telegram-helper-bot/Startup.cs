using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Framework;
using ElenaHelperBot.Options;
using ElenaHelperBot.Handlers;
using ElenaHelperBot.Services;
using Telegram.Bot.Framework.Abstractions;
using Microsoft.AspNetCore.Http;

namespace ElenaHelperBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ElenaHelperBot>()
                .Configure<BotOptions<ElenaHelperBot>>(Configuration.GetSection("Bot"))
                .Configure<CustomBotOptions<ElenaHelperBot>>(Configuration.GetSection("Bot"))
                .AddScoped<StartCommand>()
                .AddScoped<InfoCommand>()
                .AddScoped<UpdateLogger>()
                .AddScoped<ExceptionHandler>()
                .AddScoped<CallbackQueryHandler>();

            services.AddScoped<IInlineMarkupService, InlineMarkupService>();
            services.AddScoped<IDescriptionTextService, DescriptionTextService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.EnsureWebhookUnset<ElenaHelperBot>();

                // get bot updates from Telegram via long-polling approach during development
                // this will disable Telegram webhooks
                app.UseTelegramBotLongPolling<ElenaHelperBot>(ConfigureBot(), startAfter: TimeSpan.FromSeconds(2));
            }
            else
            {
                // use Telegram bot webhook middleware in higher environments
                app.UseTelegramBotWebhook<ElenaHelperBot>(ConfigureBot());
                // and make sure webhook is enabled
                app.EnsureWebhookSet<ElenaHelperBot>();
            }

            app.Run(async context => { await context.Response.WriteAsync("Hello World!"); });
        }

        private IBotBuilder ConfigureBot()
        {
            return new BotBuilder()
                .Use<ExceptionHandler>()
                .Use<UpdateLogger>()

                // .Use<CustomUpdateLogger>()
                .UseWhen(When.NewMessage, msgBranch => msgBranch
                    .UseWhen(When.NewTextMessage, txtBranch => txtBranch
                            .UseWhen(When.NewCommand, cmdBranch => cmdBranch
                                .UseCommand<StartCommand>("start")
                                .UseCommand<InfoCommand>("info")
                            )
                    )
                )
                .UseWhen<CallbackQueryHandler>(When.CallbackQuery);
        }
    }
}
