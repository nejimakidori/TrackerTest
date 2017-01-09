using System;
using System.Windows;
using System.Windows.Input;

namespace TrackerTest
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private double _downedX = 0;
        private double _downedY = 0;

        // 参考：https://blogs.msdn.microsoft.com/shintak/2012/07/04/xaml-1c/
        private void ContentPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 矩形初期値設定
            Point p = e.GetPosition(this);
            _downedX = p.X;
            _downedY = p.Y;
            rectangle1.Margin = new Thickness(p.X, p.Y, 0, 0);
            rectangle1.Width = 0;
            rectangle1.Height = 0;
            rectangle1.Visibility = Visibility.Visible;
        }

        private void ContentPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // 矩形非表示
            rectangle1.Width = 0;
            rectangle1.Height = 0;
            rectangle1.Visibility = Visibility.Hidden;
        }

        private void ContentPanel_MouseMove(object sender, MouseEventArgs e)
        {
            // 移動中矩形サイズ
            if (rectangle1.Visibility == Visibility.Visible)
            {
                Point p = e.GetPosition(this);
                if (p.X - _downedX >= 0 && p.Y - _downedY >= 0)
                {
                    // マウスダウン座標から見て右下
                    rectangle1.Margin = new Thickness(_downedX, _downedY, 0, 0);
                    rectangle1.Width  = p.X - _downedX;
                    rectangle1.Height = p.Y - _downedY;
                }
                else if (p.X - _downedX < 0 && p.Y - _downedY >= 0)
                {
                    // マウスダウン座標から見て左下
                    rectangle1.Margin = new Thickness(p.X, _downedY, 0, 0);
                    rectangle1.Width  = _downedX - p.X;
                    rectangle1.Height = p.Y - _downedY;
                }
                else if (p.X - _downedX >= 0 && p.Y - _downedY < 0)
                {
                    // マウスダウン座標から見て右上
                    rectangle1.Margin = new Thickness(_downedX, p.Y, 0, 0);
                    rectangle1.Width  = p.X - _downedX;
                    rectangle1.Height = _downedY - p.Y;
                }
                else
                {
                    // マウスダウン座標から見て左上
                    rectangle1.Margin = new Thickness(p.X, p.Y, 0, 0);
                    rectangle1.Width  = Math.Abs(_downedX - p.X);
                    rectangle1.Height = Math.Abs(_downedY - p.Y);
                }
            }
        }
    }
}
