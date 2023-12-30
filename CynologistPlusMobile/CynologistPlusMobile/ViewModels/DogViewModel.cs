using CynologistPlusMobile.Interfaces;
using CynologistPlusMobile.Model;
using CynologistPlusMobile.Resources;
using CynologistPlusMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CynologistPlusMobile.ViewModels
{
    /// <summary>
    /// ViewModel for displaying and managing individual dog details, including skills and related actions.
    /// Implements INotifyPropertyChanged to notify the UI of property changes.
    /// </summary>
    [QueryProperty(nameof(Id), nameof(Id))] // We get Id property from query path
    public class DogViewModel : INotifyPropertyChanged
    {
        private IDogService _dogService = DependencyService.Get<IDogService>();
        private DogSkill selectedDogSkill { get; set; } = null;

        public Dog CurrentDog { get; set; } = new Dog();
        // We use Observable collection instead of ordinary collection 
        // so that View can keep track of its changes
        public ObservableCollection<DogSkill> DogSkills { get; set; } = new ObservableCollection<DogSkill>();
        public bool IsMessageExists { get; set; } = false;
        public string MessageText { get; set; }
        public string EnteredSkillChangeValue { get; set; }

        public string MaxValue
        {
            get
            {
                if (SelectedDogSkill == null)
                    return "";
                // -1 means that dog skill value has no limit according to convention
                if (SelectedDogSkill.Skill.MaxValue == -1)
                    return Resource.Infinity;
                else
                    return SelectedDogSkill.Skill.MaxValue.ToString();
            }
            set { }
        }

        public string Id
        {
            set
            {
                // When we get ID (from query) we need to load data from server
                int dogId = Convert.ToInt32(value);
                LoadDog(dogId);
                LoadDogSkills(dogId);
            }
            get 
            {
                return CurrentDog.Id.ToString();
            }
        }

        public DogSkill SelectedDogSkill
        {
            get { return selectedDogSkill; }
            set
            {
                this.selectedDogSkill = value;
                // Notification to the listeners of property changing
                OnPropertyChanged(nameof(SelectedDogSkill));
                OnPropertyChanged(nameof(MaxValue));
                OnPropertyChanged(nameof(SelectedDogSkill.Skill));
            }
        }

        public async void LoadDog(int id)
        {
            try
            {
                CurrentDog = await _dogService.GetDog(id);
                OnPropertyChanged(nameof(CurrentDog));
            }
            catch
            {
                Debug.WriteLine("Failed to load Dog");
                // Initializing empty dog if we cant get info from server
                CurrentDog = new Dog();
                CurrentDog.Id = 0;
                IsMessageExists = true;
                MessageText = Resource.ServerError;
                ChangePropertyMessage();
            }
        }

        public async void LoadDogSkills(int dogId)
        {
            try
            {
                IEnumerable<DogSkill> dogSkills = await _dogService.GetDogSkills(dogId);
                DogSkills.Clear();

                // We need to copy values to observable collection
                foreach (DogSkill dogSkill in dogSkills)
                    DogSkills.Add(dogSkill);
            }
            catch
            {
                Debug.WriteLine("Failed to load DogSkills");
                IsMessageExists = true;
                MessageText = Resource.ServerError;
                ChangePropertyMessage();
            }
        }

        public async void ChangeDogSkill()
        {
            if(SelectedDogSkill == null)
            {
                IsMessageExists = true;
                MessageText = Resource.ChooseSkillError;
                ChangePropertyMessage();
            }
            else
            {
                double previousValue = SelectedDogSkill.Value;
                try
                {
                    // Business logic validation
                    double newValue = Convert.ToDouble(EnteredSkillChangeValue);
                    if (newValue > SelectedDogSkill.Skill.MaxValue
                        || newValue < 0)
                    {
                        IsMessageExists = true;
                        MessageText = Resource.ValueError;
                        ChangePropertyMessage();
                        return;
                    }
                    SelectedDogSkill.Value = newValue;
                    bool result = await _dogService.ChangeDogSkill(SelectedDogSkill);
                    if(result == true) {
                        IsMessageExists = true;
                        MessageText = Resource.SkillChangeSuccess;
                        ChangePropertyMessage();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    SelectedDogSkill.Value = previousValue;
                    IsMessageExists = true;
                    MessageText = Resource.SkillChangeError;
                    ChangePropertyMessage();
                }
                
            }
        }

        // Implementing INotifyProperfyChanged to notify the UI of property changes
        private void ChangePropertyMessage()
        {
            OnPropertyChanged(nameof(IsMessageExists));
            OnPropertyChanged(nameof(MessageText));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}