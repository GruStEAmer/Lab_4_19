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
            index_wall = 0;

        private bool 
            Winner = true;
        private
            List<TextBox> tlist = new List<TextBox>();



        public MainWindow()
        {
            InitializeComponent();
            Gen();
        }
        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D:
                    double x = 270, y = 500;
                    Check_pulya(x, y);

                    break;
                case Key.Left:
                    double x_left = 715, y_left = 500;
                    Check_pulya_left(x_left, y_left);
                    break;
                case Key.W:
                    MessageBox.Show("W");
                    break;
                case Key.S:
                    MessageBox.Show("S");
                    break;
                case Key.Up:
                    MessageBox.Show(" Up");
                    break;
                case Key.Down:
                    MessageBox.Show("Down");
                    break;
            }
        }

        private void Gen()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    TextBox b = new TextBox
                    {
                        Name = "wall" + index_wall.ToString(),
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
                Fill = new SolidColorBrush(Colors.Black)
            };
            Canvas.SetLeft(el_l, x);
            Canvas.SetTop(el_l, y);

            canvas.Children.Add(el_l);

            for (int i = 0; i < 3000; i++)
            {
                Canvas.SetLeft(el_l, x++);
                await Task.Delay(1);

                foreach (TextBox t in tlist)
                {
                    if (
                        (x + 20 > Canvas.GetLeft(t)) &&
                        ((y > Canvas.GetTop(t)) && (y < (Canvas.GetTop(t) + 39))) ||

                        (x + 20 > Canvas.GetLeft(t)) &&
                        (((y + 20) > Canvas.GetTop(t)) && ((y + 20) < (Canvas.GetTop(t) + 39))) ||

                        (x + 20 > Canvas.GetLeft(t)) &&
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
        private async void Check_pulya_left(double x, double y)
        {
            Ellipse el_l = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = new SolidColorBrush(Colors.Black)
            };
            Canvas.SetLeft(el_l, x);
            Canvas.SetTop(el_l, y);

            canvas.Children.Add(el_l);

            for (int i = 0; i < 3000; i++)
            {
                Canvas.SetLeft(el_l, x--);
                await Task.Delay(1);

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
