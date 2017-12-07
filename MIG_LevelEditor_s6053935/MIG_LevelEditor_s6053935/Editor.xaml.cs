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
        int tiles = 0;
        List <Tile>tileList = new List<Tile>();
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


        private void btnImportTileset_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Title = "Load Tileset";
            dlg.Filter = "All Files (Images)|*.png;*.jpg;*.jpeg|JPEG Files (*.jpeg, *jpg)|*.jpeg;*.jpg|PNG Files (*.png)|*.png";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                TilesetName.Text = "Tileset: " + filename;

                Image tilesetImage = new Image();
                ImageBrush tilesetImage2 = new ImageBrush();
                //tilesetImage2.ImageSource = new BitmapImage(new Uri(filename));
                tilesetImage.Source = new BitmapImage(new Uri(filename));
                //TileCanvas.Background = tilesetImage2;

                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        if (tilesetImage != null)
                        {
                            Image img = new Image();
                            img.Source = new BitmapImage(new Uri(filename));
                            TileCanvas.Children.Add(img);
                        }
                        else
                        {
                            Rectangle rect = new Rectangle();
                            rect.Fill = Brushes.Magenta;
                            TileCanvas.Children.Add(rect);
                        }
                    }
                }
                /*
                Canvas.SetLeft(grid, (canvas.Width - grid.Width) / 2);
                Canvas.SetTop(grid, (canvas.Height - grid.Height) / 2);
                canvas.Children.Add(grid);
                */
            }



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

                    tileList.Add(new Tile(canvasman, (row * width + col).ToString(), top, left));
                    
                    left += (size + 1);
                }

                left = 0;
                top += (size + 1);
                tiles++;
            }

            

            canvasman.Width = (width * 32) + width;
            canvasman.Height = (height * 32) + height;
        }

        private void UpdateGrid(object sender, MouseEventArgs e)
        {
            tileList[0].MouseOver();

            for (int i = 0; i > tileList.Count(); i++)
            {
                

            }


        }

    }


 }
