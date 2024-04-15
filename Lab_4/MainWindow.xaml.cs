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

namespace Lab_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int
            red_d_ang = 0,
            blue_d_ang = 0;
        private bool 
            Winner = true;
        private
            List<TextBox> tlist = new List<TextBox>();




        public MainWindow()
        {
            InitializeComponent();
            Gen();
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D:
                    double x = Canvas.GetLeft(Red) + (Red.Width - 10), y = Canvas.GetTop(Red) + 10;
                    Check_pulya(x, y);

                    break;
                case Key.Left:
                    double x_left = Canvas.GetLeft(Blue) + 10, y_left = Canvas.GetTop(Red) + 10;
                    Check_pulya_right(x_left, y_left);
                    break;
                case Key.W:
                    if(red_d_ang > -45) Red_d.RenderTransform = new RotateTransform(red_d_ang -= 15);
                    break;
                case Key.S:
                    if(red_d_ang < 0) Red_d.RenderTransform = new RotateTransform(red_d_ang += 15);
                    break;
                case Key.Up:
                    if (blue_d_ang < 45) Blue_d.RenderTransform = new RotateTransform(blue_d_ang += 15);
                    break;
                case Key.Down:
                    if (blue_d_ang > 0) Blue_d.RenderTransform = new RotateTransform(blue_d_ang -= 15);
                    break;
            }
        }

        private void Gen()
        {
            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    TextBox b = new TextBox
                    {
                        Name = "wall" + x.ToString() + y.ToString(),
                        Background = new SolidColorBrush(Colors.RosyBrown),
                        IsReadOnly = true,
                        Width = 40,
                        Height = 39
                    };
                    Canvas.SetLeft(b, 400 + 40 * x);
                    Canvas.SetTop(b, y * 39);

                    canvas.Children.Add(b);

                    tlist.Add(b);
                }
            }


        }

        private async void Check_pulya(double x, double y)
        {
            Ellipse el_l = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(-3.7,0.5),
                RenderTransform = new RotateTransform(red_d_ang)

            };
            Canvas.SetLeft(el_l, x);
            Canvas.SetTop(el_l, y);

            canvas.Children.Add(el_l);

            for (int i = 0; i < 3000; i++)
            {

                switch (red_d_ang) {
                    case 0:
                        Canvas.SetLeft(el_l, x += 5);
                        break;
                    case -15:
                        Canvas.SetLeft(el_l, x += 5);
                        Canvas.SetTop(el_l,y -= 2.5);
                        break;
                    case -30:
                        Canvas.SetLeft(el_l, x += 5);
                        Canvas.SetTop(el_l, y -= 3.5);
                        break;
                    case -45:
                        Canvas.SetLeft(el_l, x += 5);
                        Canvas.SetTop(el_l, y -= 5);
                        break;
                }
                await Task.Delay(1);

                foreach (TextBox t in tlist)
                {
                    if (
                        (x + 20 > Canvas.GetLeft(t) && (x-20 < Canvas.GetLeft(t) + 40 && x< Canvas.GetLeft(t) + 40)) &&
                        ((y > Canvas.GetTop(t)) && (y < (Canvas.GetTop(t) + 39))) ||

                        (x + 20 > Canvas.GetLeft(t) && (x - 20 < Canvas.GetLeft(t) + 40 && x < Canvas.GetLeft(t) + 40)) &&
                        (((y + 20) > Canvas.GetTop(t)) && ((y + 20) < (Canvas.GetTop(t) + 39))) ||

                        (x + 20 > Canvas.GetLeft(t) && (x - 20 < Canvas.GetLeft(t) + 40 && x < Canvas.GetLeft(t) + 40)) &&
                        (((y - 20) > Canvas.GetTop(t)) && ((y - 20) < (Canvas.GetTop(t) + 39)))
                    )
                    {
                        t.Visibility = Visibility.Collapsed;
                        el_l.Visibility = Visibility.Collapsed;
                        Canvas.SetLeft(t, 0); Canvas.SetTop(t, 0);
                        return;
                    }
                }

                if((x + 20 > Canvas.GetLeft(Blue)) &&
                  ((y > Canvas.GetTop(Blue)) && (y < (Canvas.GetTop(Blue) + 39))) &&
                  Winner)
                {
                    Winner = false;
                    el_l.Visibility = Visibility.Collapsed;
                    Blue.Visibility = Visibility.Collapsed;
                    Blue_d.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Red Win!");
                    Close();

                }

            }

        }

        private async void Check_pulya_right(double x, double y)
        {
            Ellipse el_l = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = new SolidColorBrush(Colors.Black),
                RenderTransformOrigin = new Point(3.7, 0.5),
                RenderTransform = new RotateTransform(blue_d_ang)
            };
            Canvas.SetLeft(el_l, x);
            Canvas.SetTop(el_l, y);

            canvas.Children.Add(el_l);

            for (int i = 0; i < 3000; i++)
            {
                await Task.Delay(1);

                switch (blue_d_ang)
                {
                    case 0:
                        Canvas.SetLeft(el_l, x -= 5);
                        break;
                    case 15:
                        Canvas.SetLeft(el_l, x -= 5);
                        Canvas.SetTop(el_l, y -= 2.5);
                        break;
                    case 30:
                        Canvas.SetLeft(el_l, x -= 5);
                        Canvas.SetTop(el_l, y -= 3.5);
                        break;
                    case 45:
                        Canvas.SetLeft(el_l, x -= 5);
                        Canvas.SetTop(el_l, y -= 5);
                        break;
                }

                foreach (TextBox t in tlist)
                {
                    if (
                        (x - 40 < Canvas.GetLeft(t)) &&
                        ((y > Canvas.GetTop(t)) && (y < (Canvas.GetTop(t) + 39))) ||

                       (x - 40 < Canvas.GetLeft(t)) &&
                        (((y + 20) > Canvas.GetTop(t)) && ((y + 20) < (Canvas.GetTop(t) + 39))) ||

                        (x - 40 < Canvas.GetLeft(t)) &&
                        (((y - 20) > Canvas.GetTop(t)) && ((y - 20) < (Canvas.GetTop(t) + 39)))
                    )
                    {
                        t.Visibility = Visibility.Collapsed;
                        el_l.Visibility = Visibility.Collapsed;
                        Canvas.SetLeft(t, 0); Canvas.SetTop(t, 0);
                        return;
                    }
                }
                if 
                (
                 (x - 20 < Canvas.GetLeft(Red) + Red.Width) &&
                 ((y > Canvas.GetTop(Red)) && (y < (Canvas.GetTop(Red) + 39)))&&
                 Winner)
                {
                    Winner = false;
                    el_l.Visibility = Visibility.Collapsed;
                    Red.Visibility = Visibility.Collapsed;
                    Red_d.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Blue Win!");
                    Close();
                }

            }

        }
    }
}
