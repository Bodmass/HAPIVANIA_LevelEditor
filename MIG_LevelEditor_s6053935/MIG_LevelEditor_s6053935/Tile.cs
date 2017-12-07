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
        Brush tilecolour = Brushes.Magenta;
        Rectangle TileRect;



        public Tile(Canvas canvas, String tileText, int top, int left)
        {
            TileRect = new Rectangle();
            TileRect.Width = tilesize; TileRect.Height = tilesize;
            TileRect.Fill = tilecolour;
            TextBlock text = new TextBlock();
            text.Width = tilesize; text.Height = tilesize;
            text.Text = tileText;

            Canvas.SetTop(TileRect, top);
            Canvas.SetLeft(TileRect, left);
            Canvas.SetTop(text, top);
            Canvas.SetLeft(text, left);
            canvas.Children.Add(TileRect);
            canvas.Children.Add(text);

        }


        public void MouseOver()
        {
            TileRect.Fill = Brushes.Blue;

            if(TileRect.IsMouseOver)
            {
                
            }
            else
            {
                TileRect.Fill = Brushes.Magenta;
            }
        }

        public void SelectTile()
        {
        }

    }

}
