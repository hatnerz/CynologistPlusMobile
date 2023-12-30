using CynologistPlusMobile.Interfaces;
using CynologistPlusMobile.Resources;
using CynologistPlusMobile.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CynologistPlusMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Resource.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            DependencyService.Register<IDogService, DogService>();
            DependencyService.Register<IAuthService, AuthService>();
            Preferences.Set("ApiUrl", "http://192.168.0.103:5181");
            MainPage = new AppShell();
            Shell.Current.GoToAsync("//AuthPage");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
