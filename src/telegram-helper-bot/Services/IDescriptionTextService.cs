using ElenaHelperBot.Constants;

namespace ElenaHelperBot.Services
{
    public interface IDescriptionTextService
    {
        string GetDescriptionText(InlineButtonValue callbackValue = InlineButtonValue.Default);
    }
}
