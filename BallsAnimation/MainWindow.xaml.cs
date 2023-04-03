using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BallsAnimation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            AddBall();
        }


        //private Ellipse ball;
        private Random random = new Random();
        
       private List<Ellipse> Collection = new List<Ellipse>();

        private void AddBall()
        {
            var ball = new Ellipse();
            ball.Width = 50;
            ball.Height = 50;
            ball.Fill = new SolidColorBrush(generateRandomColor());
            Canvas.SetLeft(ball, 0);
            Canvas.SetTop(ball, 0);
            ball.Tag = new Point();
            AddBall(ball);
            //return ball;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBall();
        }

        private void AddBalls(int numOfballs)
        {
            int i;

            for (i=0;i< numOfballs; i++)
            {
                var ball = new Ellipse();
                ball.Width = 50;
                ball.Height = 50;
                ball.Fill = new SolidColorBrush(generateRandomColor());
                Canvas.SetLeft(ball, i*10);
                Canvas.SetTop(ball, i * 10);
                ball.Tag = new Point();
                AddBall(ball);
            }
            numOfBalls.Text = Collection.Count.ToString();
            Multi = i;


        }
        int Multi = 0;

        private void AddBall(UIElement ball)
        {
            MyCanvas.Children.Add(ball);
            Collection.Add((Ellipse)ball);
            numOfBalls.Text = Collection.Count.ToString();
        }
        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int i = Collection.Count - Multi;
            
            Log.Text += "Ball " + i + "\n";
            var ball = Collection[i-1];
            var clickPoint = e.GetPosition(MyCanvas);
            Point ballPoint = new Point(Canvas.GetLeft(ball) + ball.Width / 2, Canvas.GetTop(ball) + ball.Height / 2);
           
            if (Multi > 0)
                Multi--;


            double distance = Math.Sqrt(Math.Pow(clickPoint.X - ballPoint.X, 2) + Math.Pow(clickPoint.Y - ballPoint.Y, 2));
            if (distance < ball.Width / 2)
            {
                // Ball was clicked, start moving it
                double newX = Canvas.GetLeft(ball) + ball.Width / 2;
                double newY = Canvas.GetTop(ball) + ball.Height / 2;
                double deltaX = clickPoint.X - newX;
                double deltaY = clickPoint.Y - newY; 
                CompositionTarget.Rendering -= CompositionTargetRendering(ball, newX, newY, deltaX, deltaY);
                CompositionTarget.Rendering += CompositionTargetRendering(ball, newX, newY, deltaX, deltaY);

            }
            
        }

        private EventHandler CompositionTargetRendering(Ellipse ball, double newX, double newY, double deltaX,  double deltaY)
        {
            return (s, args) =>
            {
                newX += deltaX / 10;
                newY += deltaY / 10;

                if (newX - ball.Width / 2 < 0) // Left edge
                {
                    newX = ball.Width / 2;
                    deltaX = -deltaX;
                }
                if (newX + ball.Width / 2 > MyCanvas.ActualWidth) // Right edge
                {
                    newX = (MyCanvas.ActualWidth - ball.Width / 2);
                    deltaX = -deltaX;
                }

                if (newY - ball.Height / 2 < 0) // Top edge
                {
                    newY = ball.Height / 2;
                    deltaY = -deltaY;
                }
                if (newY + ball.Height / 2 > MyCanvas.ActualHeight) // Bottom edge
                {
                    newY = MyCanvas.ActualHeight - ball.Height / 2;
                    deltaY = -deltaY;
                }
                Canvas.SetLeft(ball, newX - ball.Width / 2);
                Canvas.SetTop(ball, newY - ball.Height / 2);
            };
        }

        private Color generateRandomColor()
        {
            // Create a new instance of the Random class
            Random rand = new Random();

            // Generate random values for each color component (red, green, and blue)
            byte red = (byte)rand.Next(256); // values range from 0-255
            byte green = (byte)rand.Next(256);
            byte blue = (byte)rand.Next(256);

            return Color.FromRgb(red, green, blue); 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddBalls(10);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddBalls(100);
        }


        //private void CompositionTarget_Rendering(object sender, EventArgs e)
        //{
        //    // Update the position of each ball based on its velocity
        //    foreach (Ball ball in balls)
        //    {
        //        ball.PositionX += ball.VelocityX;
        //        ball.PositionY += ball.VelocityY;

        //        // Check for collisions with walls
        //        if (ball.PositionX < 0 || ball.PositionX > canvas.ActualWidth - ball.Width)
        //        {
        //            ball.VelocityX *= -1;
        //        }
        //        if (ball.PositionY < 0 || ball.PositionY > canvas.ActualHeight - ball.Height)
        //        {
        //            ball.VelocityY *= -1;
        //        }

        //        // Check for collisions with other balls
        //        foreach (Ball otherBall in balls)
        //        {
        //            if (ball != otherBall) // Make sure we're not comparing a ball with itself
        //            {
        //                double dx = ball.PositionX - otherBall.PositionX;
        //                double dy = ball.PositionY - otherBall.PositionY;
        //                double distance = Math.Sqrt(dx * dx + dy * dy);

        //                if (distance < ball.Radius + otherBall.Radius)
        //                {
        //                    // Handle the collision by bouncing the balls off each other
        //                    double angle = Math.Atan2(dy, dx);
        //                    double sin = Math.Sin(angle);
        //                    double cos = Math.Cos(angle);

        //                    // Rotate ball1's velocity
        //                    double vx1 = ball.VelocityX * cos + ball.VelocityY * sin;
        //                    double vy1 = ball.VelocityY * cos - ball.VelocityX * sin;

        //                    // Rotate ball2's velocity
        //                    double vx2 = otherBall.VelocityX * cos + otherBall.VelocityY * sin;
        //                    double vy2 = otherBall.VelocityY * cos - otherBall.VelocityX * sin;

        //                    // Swap x velocities
        //                    double temp = vx1;
        //                    vx1 = vx2;
        //                    vx2 = temp;

        //                    // Rotate velocities back
        //                    ball.VelocityX = vx1 * cos - vy1 * sin;
        //                    ball.VelocityY = vy1 * cos + vx1 * sin;
        //                    otherBall.VelocityX = vx2 * cos - vy2 * sin;
        //                    otherBall.VelocityY = vy2 * cos + vx2 * sin;
        //                }
        //            }
        //        }
        //    }
        //}

    }
}
