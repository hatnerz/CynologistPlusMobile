using CynologistPlusMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CynologistPlusMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public AuthViewModel authViewModel;

        public AuthPage()
        {
            authViewModel = new AuthViewModel();
            this.BindingContext = authViewModel;
            InitializeComponent();
        }

        void OnLoginClicked(object sender, EventArgs e)
        {
            authViewModel.Login();
        }
    }
}