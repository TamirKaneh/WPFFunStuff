using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallsAnimation.Models
{
    public class BallModel : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _velocityX;
        private double _velocityY;
        private double _radius;

        public event PropertyChangedEventHandler PropertyChanged;

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                NotifyPropertyChanged(nameof(X));
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                NotifyPropertyChanged(nameof(Y));
            }
        }

        public double VelocityX
        {
            get => _velocityX;
            set
            {
                _velocityX = value;
                NotifyPropertyChanged(nameof(VelocityX));
            }
        }

        public double VelocityY
        {
            get => _velocityY;
            set
            {
                _velocityY = value;
                NotifyPropertyChanged(nameof(VelocityY));
            }
        }

        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                NotifyPropertyChanged(nameof(Radius));
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
