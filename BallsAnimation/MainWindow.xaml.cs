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
            MyCanvas.Children.Add(ball);
            Collection.Add(ball);
            //return ball;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBall();
        }

        private void AddBalls(int numOfballs)
        {
            for(int i=0;i< numOfballs; i++)
            {
                var ball = new Ellipse();
                ball.Width = 50;
                ball.Height = 50;
                ball.Fill = new SolidColorBrush(generateRandomColor());
                Canvas.SetLeft(ball, i*10);
                Canvas.SetTop(ball, i * 10);
                ball.Tag = new Point();
                MyCanvas.Children.Add(ball);
                Collection.Add(ball);
            }
            
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var ball in Collection)
            {
                var clickPoint = e.GetPosition(MyCanvas);
                Point ballPoint = new Point(Canvas.GetLeft(ball) + ball.Width / 2, Canvas.GetTop(ball) + ball.Height / 2);

                double distance = Math.Sqrt(Math.Pow(clickPoint.X - ballPoint.X, 2) + Math.Pow(clickPoint.Y - ballPoint.Y, 2));
                if (distance < ball.Width / 2)
                {
                    // Ball was clicked, start moving it
                    double newX = Canvas.GetLeft(ball) + ball.Width / 2;
                    double newY = Canvas.GetTop(ball) + ball.Height / 2;
                    double deltaX = clickPoint.X - newX;
                    double deltaY = clickPoint.Y - newY;
                    CompositionTarget.Rendering += (s, args) =>
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
            }
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
    }
}
