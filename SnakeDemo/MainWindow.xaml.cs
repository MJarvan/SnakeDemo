using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnakeDemo
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow:Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		//蛇
		List<Point> SnakePoint = new List<Point>();
        private static int SnakeSize = 10;
        private Key direction = Key.Right;

        //食物
        Point FoodPoint = new Point();
        private static bool IsFood = false;

        //系统
        Random random = new Random();
        System.Windows.Threading.DispatcherTimer timer;//定时器

        private void Window_Loaded(object sender,RoutedEventArgs e)
		{
            int SnakeX = random.Next(100,400);
            int SnakeY = random.Next(100,400);
            //预创建蛇
            SnakePoint.Add(new Point(SnakeX + 4 * SnakeSize,SnakeY));
            SnakePoint.Add(new Point(SnakeX + 3 * SnakeSize,SnakeY));
            SnakePoint.Add(new Point(SnakeX + 2 * SnakeSize,SnakeY));
            SnakePoint.Add(new Point(SnakeX + 1 * SnakeSize,SnakeY));
            SnakePoint.Add(new Point(SnakeX,SnakeY));

            RefershSnake();
            RefershFood();

            //定时器初始化
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        /// <summary>
        /// 刷新蛇
        /// </summary>
        private void RefershSnake()
        {
            canvas.Children.Clear();
            foreach(Point point in SnakePoint)
            {
                Rectangle rtg = new Rectangle();
                rtg.Fill = new SolidColorBrush(Colors.Black);
                rtg.Width = SnakeSize;
                rtg.Height = SnakeSize;
                canvas.Children.Add(rtg);
                Canvas.SetTop(rtg,point.Y);
                Canvas.SetLeft(rtg,point.X);

                if(point.X == canvas.Width || point.Y == canvas.Height || point.X == 0 || point.Y == 0)
                {
                    timer.Stop();
                    MessageBox.Show("nishule");
                }
            }
        }
        /// <summary>
        /// 刷新食物
        /// </summary>
        private void RefershFood()
        {
            if(!IsFood)
            {
                FoodPoint.X = random.Next(0,500);
                FoodPoint.Y = random.Next(0,500);

                int num = 0;

                foreach(Point point in SnakePoint)
                {
                    if(FoodPoint != point)
                    {
                        num++;
                    }

                }
                if(num == SnakePoint.Count)
                {
                    Rectangle rtg = new Rectangle();
                    rtg.Fill = new SolidColorBrush(Colors.Green);
                    rtg.Width = SnakeSize;
                    rtg.Height = SnakeSize;
                    canvas.Children.Add(rtg);
                    Canvas.SetTop(rtg,FoodPoint.Y);
                    Canvas.SetLeft(rtg,FoodPoint.X);
                }
                IsFood = true;
            }
            else
            {
                Rectangle rtg = new Rectangle();
                rtg.Fill = new SolidColorBrush(Colors.Green);
                rtg.Width = SnakeSize;
                rtg.Height = SnakeSize;
                canvas.Children.Add(rtg);
                Canvas.SetTop(rtg,FoodPoint.Y);
                Canvas.SetLeft(rtg,FoodPoint.X);
            }
        }

        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object sender,EventArgs e)
        {
            Point headpoint = SnakePoint[0];
            Point newpoint = new Point();

            //Y轴-1
            if(direction == Key.Up)
            {
                newpoint.X = headpoint.X;
                newpoint.Y = headpoint.Y - SnakeSize;
            }
            //Y轴+1
            else if(direction == Key.Down)
            {
                newpoint.X = headpoint.X;
                newpoint.Y = headpoint.Y + SnakeSize;
            }
            //X轴-1
            else if(direction == Key.Left)
            {
                newpoint.X = headpoint.X - SnakeSize;
                newpoint.Y = headpoint.Y;
            }
            //X轴+1
            else if(direction == Key.Right)
            {
                newpoint.X = headpoint.X + SnakeSize;
                newpoint.Y = headpoint.Y;
            }
            SnakePoint.Remove(SnakePoint[SnakePoint.Count - 1]);
            SnakePoint.Insert(0,newpoint);
            RefershSnake();
            RefershFood();
        }

        /// <summary>
        /// 旋转蛇头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender,KeyEventArgs e)
        {
            //蛇头向下的时候不能往上跑
            if(Keyboard.IsKeyDown(Key.Up) && direction != Key.Down)
            {
                direction = Key.Up;
            }
            //蛇头向上的时候不能往下跑
            else if(Keyboard.IsKeyDown(Key.Down) && direction != Key.Up)
            {
                direction = Key.Down;
            }
            //蛇头向右的时候不能往左跑
            else if(Keyboard.IsKeyDown(Key.Left) && direction != Key.Right)
            {
                direction = Key.Left;
            }
            //蛇头向左的时候不能往右跑
            else if(Keyboard.IsKeyDown(Key.Right) && direction != Key.Left)
            {
                direction = Key.Right;
            }
        }
    }
}
