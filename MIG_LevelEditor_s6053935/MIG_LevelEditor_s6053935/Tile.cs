using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Reflection;

namespace MIG_LevelEditor_s6053935
{
    class Tile
    {
        int tilesize = 32;
        Brush tilecolour = Brushes.Black;
        Rectangle TileRect;
        string currentAssemblyParentPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        ImageBrush tileImage;
        string tileImageName;
        Rectangle Selectangle;
        int remember_top;
        int remember_left;


        public Tile(Canvas canvas, String tileText, int top, int left)
        {

            remember_top = top;
            remember_left = left;


            TileRect = new Rectangle();
            TileRect.Width = tilesize; TileRect.Height = tilesize;
            TileRect.Fill = tilecolour;
            TileRect.Stroke = Brushes.DarkSlateGray;
            // TextBlock text = new TextBlock();
            // text.Width = tilesize; text.Height = tilesize;
            //text.Foreground = Brushes.GhostWhite;
            //text.Text = tileText;

            Canvas.SetTop(TileRect, top);
            Canvas.SetLeft(TileRect, left);
            //Canvas.SetTop(text, top);
           // Canvas.SetLeft(text, left);
            canvas.Children.Add(TileRect);
            //canvas.Children.Add(text);

            Selectangle = new Rectangle();
            Selectangle.Width = tilesize; Selectangle.Height = tilesize;
            Canvas.SetTop(Selectangle, top);
            Canvas.SetLeft(Selectangle, left);
            Selectangle.StrokeThickness = 1;
            Selectangle.Stroke = Brushes.White;
            canvas.Children.Add(Selectangle);
            Selectangle.Visibility = Visibility.Collapsed;

            tileImage = new ImageBrush(new BitmapImage(new Uri(String.Format("file:///{0}/" + "Cave/Cave_00.png", currentAssemblyParentPath))));
            tileImageName = "Cave/Cave_00.png";
            TileRect.Fill = tileImage;

        }

        public void SelectTile(ImageBrush img, string str)
        {
            TileRect.Fill = img;
            tileImage = img;
            tileImageName = str;
            Selectangle.Visibility = Visibility.Visible;

        }

        public void SelectTile2()
        {
            Selectangle.Visibility = Visibility.Visible;

        }

        public void DeselectTile()
        {
            Selectangle.Visibility = Visibility.Collapsed;

        }

        public ImageBrush CopyTile()
        {
            return tileImage;
        }

        public Rectangle getRect()
        {
            return TileRect;
        }

        public int getX()
        {
            return remember_left;
        }

        public int getY()
        {
            return remember_top;
        }

        public string getImageName()
        {
            return tileImageName;
        }

        public void setImageName(string str)
        {
            tileImageName = str;
        }
    }

}
