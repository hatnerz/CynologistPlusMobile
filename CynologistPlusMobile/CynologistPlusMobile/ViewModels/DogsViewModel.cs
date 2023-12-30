using CynologistPlusMobile.Interfaces;
using CynologistPlusMobile.Model;
using CynologistPlusMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CynologistPlusMobile.ViewModels
{
    public class DogsViewModel
    {
        public ObservableCollection<Dog> Dogs { get; set; }
        private Dog selectedDog { get; set; }

        private IDogService _dogService = DependencyService.Get<IDogService>();

        public DogsViewModel()
        {
            Dogs = new ObservableCollection<Dog>();
            _dogService = DependencyService.Get<IDogService>();
        }

        public async Task GetDogs()
        {

            try
            {
                IEnumerable<Dog> dogs = await _dogService.GetAllDogs();
                Dogs.Clear();

                // Adding loaded from server data to obserable collection
                foreach (Dog dog in dogs)
                    Dogs.Add(dog);
            }
            catch
            {
                Debug.WriteLine("Failed to load dog list");
                Dogs = null;
            }
        }

        public Dog SelectedDog
        {
            get { return selectedDog; }
            set
            {
                if (selectedDog != value)
                {
                    Dog tempDog = new Dog()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Breed = value.Breed
                    };
                    selectedDog = null;
                    Shell.Current.GoToAsync($"{nameof(DogPage)}?{nameof(DogViewModel.Id)}={tempDog.Id}");
                }
            }
        }
    }
}
