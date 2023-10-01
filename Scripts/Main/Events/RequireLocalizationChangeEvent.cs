using sm_application.Events;
using sm_application.Localizations;

namespace sm_application.Scripts.Main.Events
{
    public class RequireLocalizationChangeEvent : BaseEvent
    {
        public Locales CurrentLocale;
    }
}