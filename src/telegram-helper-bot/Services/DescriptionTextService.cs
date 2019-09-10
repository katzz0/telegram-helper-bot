using ElenaHelperBot.Constants;

namespace ElenaHelperBot.Services
{
    public class DescriptionTextService : IDescriptionTextService
    {
        private const string DESIGN = "Описание дизайна человека";
        private const string BASIC_READING = "Описание базового чтения";
        private const string FULL_READING = "Описание углублённого чтения";
        private const string DESIGN_FOR_CHILDREN = "Описание дизайна для детей";
        private const string LOVE_COMPOSITE = "Оисаение любовного композита";
        private const string BUSINESS_PARTNER_COMPOSITE = "Описание бизнес-партнёрского композита";
        private const string COMPOSITE_WITH_A_BABY = "Описание композита с ребенком";
        private const string FAMILY_COMPOSITE = "Описание семейного композита";
        private const string PAYMENT_INFO = "Информация об оплате";

        public string GetDescriptionText(InlineButtonValue callbackValue = InlineButtonValue.Default)
        {
            switch (callbackValue)
            {
                case InlineButtonValue.AboutDesign:
                    return DESIGN;

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
                    return PAYMENT_INFO;

                default:
                    return "";
            }
        }
    }
}
