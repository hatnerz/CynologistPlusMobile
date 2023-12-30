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
    public partial class DogPage : ContentPage
    {
        DogViewModel dogViewModel;
        public DogPage()
        {
            dogViewModel = new DogViewModel();
            this.BindingContext = dogViewModel;
            InitializeComponent();
        }

        private void ButtonChangeValueClick(object sender, EventArgs e)
        {
            dogViewModel.ChangeDogSkill();
        }
    }
}