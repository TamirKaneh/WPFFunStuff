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
        private List<BallModel> bcollection = new List<BallModel>();

        public List<BallModel> Bcollection { get => bcollection; set => bcollection = value; }

        //command1
        //command multiballs
        //AddBall(numOfBalls);

    }
}
