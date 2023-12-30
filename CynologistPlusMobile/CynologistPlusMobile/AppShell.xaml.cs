using System;
using System.Collections.Generic;
using CynologistPlusMobile.ViewModels;
using CynologistPlusMobile.Views;
using Xamarin.Forms;

namespace CynologistPlusMobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DogsPage), typeof(DogsPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(DogPage), typeof(DogPage));
            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync("//AuthPage");
        }
    }
}
