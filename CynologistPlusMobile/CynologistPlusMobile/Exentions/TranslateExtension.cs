using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Resources;
using System.Globalization;
using System.Reflection;
using CynologistPlusMobile.Interfaces;

namespace CynologistPlusMobile.Exentions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo cultureInfo;
        const string resourceId = "CynologistPlusMobile.Resources.Resource";

        public TranslateExtension()
        {
            cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager resourceManager = new ResourceManager(resourceId,
                        typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = resourceManager.GetString(Text, cultureInfo);

            if (translation == null)
            {
                translation = Text;
            }
            return translation;
        }
    }
}
