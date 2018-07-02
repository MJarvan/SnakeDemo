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
        private static int SnakeWidth = 10;
        private static int SnakeHeight = 10;

        System.Windows.Threading.DispatcherTimer timer;//定时器

        private void Window_Loaded(object sender,RoutedEventArgs e)
		{
            //预创建蛇
            SnakePoint.Add(new Point(100,100));
            SnakePoint.Add(new Point(100,100 + 1 * SnakeWidth));
            SnakePoint.Add(new Point(100,100 + 2 * SnakeWidth));
            SnakePoint.Add(new Point(100,100 + 3 * SnakeWidth));
            SnakePoint.Add(new Point(100,100 + 4 * SnakeWidth));

            foreach(Point point in SnakePoint)
            {
                Rectangle rtg = new Rectangle();
                rtg.Fill = new SolidColorBrush(Colors.Black);
                rtg.Width = SnakeWidth;
                rtg.Height = SnakeHeight;
                canvas.Children.Add(rtg);
                Canvas.SetTop(rtg,point.X);
                Canvas.SetLeft(rtg,point.Y);

            }

            //定时器初始化
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void OnTimedEvent(object sender,EventArgs e)
        {
            
        }
    }
}
