using BallsAnimation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallsAnimation.ViewModels
{
    public abstract class BouncingBallslViewModel:ViewModelBase
    {
        private List<BallViewModel> bcollection = new List<BallViewModel>();

        public List<BallViewModel> Bcollection { get => bcollection; set => bcollection = value; }

        //command1
        //command multiballs
        //AddBall(numOfBalls);

    }
}
