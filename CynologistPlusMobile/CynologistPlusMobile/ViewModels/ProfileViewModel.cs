using CynologistPlusMobile.Interfaces;
using CynologistPlusMobile.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CynologistPlusMobile.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private IAuthService _authService;

        public Cynologist CurrentCynologist { get; set; }

        TimeSpan timeOffset = new DateTimeOffset(DateTime.Now).Offset;

        public string TimeOffset
        {
            get
            {
                int offset = timeOffset.Hours;
                if (offset > 0)
                    return $"GTM+{offset}";
                if (offset == 0)
                    return "GTM 0";
                else
                    return $"GTM{offset}";
            }
        }

        public DateTime CurrentTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        public ProfileViewModel()
        {
            _authService = DependencyService.Get<IAuthService>();
            LoadCynologist();
        }

        public async void LoadCynologist()
        {
            int cynologistId = _authService.GetCynologistIdFromToken();
            CurrentCynologist = await _authService.GetCynologistById(cynologistId);
            OnPropertyChanged(nameof(CurrentCynologist));
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

// GetCynologistIdFromToken
