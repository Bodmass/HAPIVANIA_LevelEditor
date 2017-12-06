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
using System.Windows.Shapes;

namespace MIG_LevelEditor_s6053935
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        int width;
        int height;
        public Editor()
        {

            int.TryParse((string)((MainWindow)Application.Current.MainWindow).SetWidth.Text, out width);
            int.TryParse((string)((MainWindow)Application.Current.MainWindow).SetHeight.Text, out height);
            InitializeComponent();
            DisplayGrid();

        }

        private void HideDropdownGrid()
        {
            //Grid_Settings.Visibility = System.Windows.Visibility.Hidden;
            //Grid_Tiles.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Tiles_Click(object sender, RoutedEventArgs e)
        {
            //HideDropdownGrid();
            //Grid_Tiles.Visibility = System.Windows.Visibility.Visible;
        }


        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            //HideDropdownGrid();
            //Grid_Settings.Visibility = System.Windows.Visibility.Visible;
        }

        private void DisplayGrid()
        {
            int size = 32;
            int top = 0;
            int left = 0;

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = size; rect.Height = size;
                    rect.Fill = Brushes.Magenta;

                    TextBlock text = new TextBlock();
                    text.Width = size; text.Height = size;
                    text.Text = (row*width+col).ToString();

                    Canvas.SetTop(rect, top);
                    Canvas.SetLeft(rect, left);
                    Canvas.SetTop(text, top);
                    Canvas.SetLeft(text, left);
                    canvasman.Children.Add(rect);
                    canvasman.Children.Add(text);

                    left += (size + 1);
                }

                left = 0;
                top += (size + 1);
            }

            canvasman.Width = (width * 32) + width;
            canvasman.Height = (height * 32) + height;
        }
    }


 }
