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

namespace MIG_LevelEditor_s6053935
{
    class Tile
    {
        int tilesize = 32;
        bool isSelected = false;
        Brush tilecolour = Brushes.Magenta;
        Rectangle TileRect;
        ImageBrush tileImage;
        Rectangle Selectangle;
        //int remember_top = 0;
        //int remember_left = 0;


        public Tile(Canvas canvas, String tileText, int top, int left)
        {
           // int remember_top = top;
           // int remember_left = left;

            TileRect = new Rectangle();
            TileRect.Width = tilesize; TileRect.Height = tilesize;
            TileRect.Fill = tilecolour;
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
   
        }

        public void SelectTile(ImageBrush img)
        {
            isSelected = true;
            TileRect.Fill = img;
            tileImage = img;
            Selectangle.Visibility = Visibility.Visible;

        }

        public void SelectTile2()
        {
            isSelected = true;
            Selectangle.Visibility = Visibility.Visible;

        }

        public void DeselectTile()
        {
            isSelected = false;
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
    }

}
