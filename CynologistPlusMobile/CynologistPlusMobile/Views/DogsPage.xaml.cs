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
    public partial class DogsPage : ContentPage
    {
        DogsViewModel dogsViewModel { get; set; }

        public DogsPage()
        {
            dogsViewModel = new DogsViewModel();
            this.BindingContext = dogsViewModel;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await dogsViewModel.GetDogs();
            base.OnAppearing();
        }
    }
}