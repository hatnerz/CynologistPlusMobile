using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CynologistPlusMobile.Model
{
    public class DogSkill : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public int DogId { get; set; }

        public virtual Dog Dog { get; set; }

        public virtual Skill Skill { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
