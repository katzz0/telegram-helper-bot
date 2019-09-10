using ElenaHelperBot.Constants;
using Telegram.Bot.Types.ReplyMarkups;

namespace ElenaHelperBot.Services
{
    public class InlineMarkupService : IInlineMarkupService
    {
        private const string ABOUT_DESIGN = "Что такое дизайн человека?";
        private const string FULL_READING = "Полное (углубленное) чтение дизайна";
        private const string BASIC_READING = "Базовое чтение дизайна";
        private const string DESIGN_FOR_CHILDREN = "Дизайн для детей";
        private const string LOVE_COMPOSITE = "Любовный композит";
        private const string OTHER = "Другое";
        private const string BACK = "Назад";
        private const string I_WANT_TO_ORDER = "Хочу заказать";
        private const string BUSINESS_PARTNER_COMPOSITE = "Бизнес-партнёрский композит";
        private const string COMPOSITE_WITH_A_BABY = "Композит с ребенком";
        private const string FAMILY_COMPOSITE = "Семейный композит";
        private const string I_HAVE_PAIED = "Я оплатил(а)";

        public string GetButtonText(InlineButtonValue callbackValue = InlineButtonValue.Default)
        {
            switch (callbackValue)
            {
                case InlineButtonValue.AboutDesign:
                    return ABOUT_DESIGN;

                case InlineButtonValue.FullReading:
                    return FULL_READING;

                case InlineButtonValue.BasicReading:
                    return BASIC_READING;

                case InlineButtonValue.DesignForChildren:
                    return DESIGN_FOR_CHILDREN;

                case InlineButtonValue.LoveComposite:
                    return LOVE_COMPOSITE;

                case InlineButtonValue.BusinessPartnerComposite:
                    return BUSINESS_PARTNER_COMPOSITE;

                case InlineButtonValue.CompositeWithABaby:
                    return COMPOSITE_WITH_A_BABY;

                case InlineButtonValue.FamilyComposite:
                    return FAMILY_COMPOSITE;

                case InlineButtonValue.IWantToOrder:
                    return I_WANT_TO_ORDER;

                case InlineButtonValue.IHavePaid:
                    return I_HAVE_PAIED;

                default:
                    return "";
            }
        }

        public bool IsInlineButtonsUpdateQuery(InlineButtonValue callbackValue = InlineButtonValue.Default)
        {
            switch (callbackValue)
            {
                case InlineButtonValue.Back:
                    return true;

                case InlineButtonValue.Other:
                    return true;

                case InlineButtonValue.AboutDesign:
                case InlineButtonValue.FullReading:
                case InlineButtonValue.BasicReading:
                case InlineButtonValue.DesignForChildren:
                case InlineButtonValue.LoveComposite:
                case InlineButtonValue.BusinessPartnerComposite:
                case InlineButtonValue.CompositeWithABaby:
                case InlineButtonValue.FamilyComposite:
                    return false;

                case InlineButtonValue.IWantToOrder:
                    return true;

                case InlineButtonValue.IHavePaid:
                    return false;

                default:
                    return true;
            }
        }

        public InlineKeyboardMarkup GetMarkup(InlineButtonValue callbackValue = InlineButtonValue.Default)
        {
            switch (callbackValue)
            {
                case InlineButtonValue.Back:
                    return GetDefaultMarkup();

                case InlineButtonValue.Other:
                    return GetDetailedDesignMarkup();

                case InlineButtonValue.AboutDesign:
                case InlineButtonValue.FullReading:
                case InlineButtonValue.BasicReading:
                case InlineButtonValue.DesignForChildren:
                case InlineButtonValue.LoveComposite:
                case InlineButtonValue.BusinessPartnerComposite:
                case InlineButtonValue.CompositeWithABaby:
                case InlineButtonValue.FamilyComposite:
                    return GetPreOrderMarkup();

                case InlineButtonValue.IWantToOrder:
                    return GetPostOrderMarkup();

                default:
                    return GetDefaultMarkup();
            }
        }

        private InlineKeyboardMarkup GetDefaultMarkup()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(ABOUT_DESIGN, InlineButtonValue.AboutDesign.ToString()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(FULL_READING, InlineButtonValue.FullReading.ToString()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(BASIC_READING, InlineButtonValue.BasicReading.ToString()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DESIGN_FOR_CHILDREN, InlineButtonValue.DesignForChildren.ToString()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(LOVE_COMPOSITE, InlineButtonValue.LoveComposite.ToString()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(OTHER, InlineButtonValue.Other.ToString()),
                },
            });
        }

        private InlineKeyboardMarkup GetDetailedDesignMarkup()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(BUSINESS_PARTNER_COMPOSITE, InlineButtonValue.BusinessPartnerComposite.ToString()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(COMPOSITE_WITH_A_BABY, InlineButtonValue.CompositeWithABaby.ToString()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(FAMILY_COMPOSITE, InlineButtonValue.FamilyComposite.ToString()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(BACK, InlineButtonValue.Back.ToString()),
                },
            });
        }

        private InlineKeyboardMarkup GetPreOrderMarkup()
        {
            return new InlineKeyboardMarkup(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(I_WANT_TO_ORDER, InlineButtonValue.IWantToOrder.ToString()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(BACK, InlineButtonValue.Back.ToString()),
                },
            });
        }

        private InlineKeyboardMarkup GetPostOrderMarkup()
        {
            return new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData(I_HAVE_PAIED, InlineButtonValue.IHavePaid.ToString()),
            });
        }
    }
}
