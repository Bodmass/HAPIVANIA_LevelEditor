using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
using System.Xml;


namespace MIG_LevelEditor_s6053935
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        int width;
        int height;
        int tg_width;
        int tg_height;
        int tiles = 0;
        List <Tile>tileList = new List<Tile>();
        List<Tile> tileList2 = new List<Tile>();
        List<Rect> tileRects = new List<Rect>();
        List<Rect> tileRects2 = new List<Rect>();
        List<ImageBrush> edtiorTiles = new List<ImageBrush>();
        ImageBrush SelectedTile;
        
        string currentAssemblyParentPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        public Editor()
        {

            int.TryParse((string)((MainWindow)Application.Current.MainWindow).SetWidth.Text, out width);
            int.TryParse((string)((MainWindow)Application.Current.MainWindow).SetHeight.Text, out height);
            InitializeComponent();
            Setup();
            DisplayGrid();
            DisplayGrid2();

        }

        bool mouseDown = false; // Set to 'true' when mouse is held down.
        Point mouseDownPos; // The point where the mouse button was clicked down.

        bool mouseDown_Left = false; // Set to 'true' when mouse is held down.
        Point mouseDownPos_Left; // The point where the mouse button was clicked down.

        private void Setup()
        {
            SelectedTile = new ImageBrush(new BitmapImage(new Uri(String.Format("file:///{0}/" + "Cave/Cave_01.png", currentAssemblyParentPath))));
        }

        private void Deselect_All()
        {
            Canvas.SetLeft(selectionBox, 9999);
            Canvas.SetTop(selectionBox, 9999);
            selectionBox.Width = 0;
            selectionBox.Height = 0;
            mouseDown = false;
            theGrid.ReleaseMouseCapture();
            selectionBox.Visibility = Visibility.Collapsed;
            foreach (Rect rects in tileRects)
            {
                tileList[tileRects.IndexOf(rects)].DeselectTile();

            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Deselect_All();

            // Capture and track the mouse.
            mouseDown = true;
            mouseDownPos = e.GetPosition(theGrid);
            theGrid.CaptureMouse();

            // Initial placement of the drag selection box.         
            Canvas.SetLeft(selectionBox, mouseDownPos.X);
            Canvas.SetTop(selectionBox, mouseDownPos.Y);
            selectionBox.Width = 0;
            selectionBox.Height = 0;

            // Make the drag selection box visible.
            selectionBox.Visibility = Visibility.Visible;

            Rect rect1 = new Rect(Canvas.GetLeft(selectionBox), Canvas.GetTop(selectionBox), selectionBox.Width, selectionBox.Height);



            foreach (Rect rects in tileRects)
            {

                if (rect1.IntersectsWith(rects))
                {
                    tileList[tileRects.IndexOf(rects)].SelectTile(SelectedTile);
                }
                else
                {
                    tileList[tileRects.IndexOf(rects)].DeselectTile();
                }
            }



        }

        private void Grid_CopyTile(object sender, MouseButtonEventArgs e)
        {
            // Capture and track the mouse.
            mouseDown = true;
            mouseDownPos = e.GetPosition(theGrid);
            theGrid.CaptureMouse();

            // Initial placement of the drag selection box.         
            Canvas.SetLeft(selectionBox, mouseDownPos.X);
            Canvas.SetTop(selectionBox, mouseDownPos.Y);
            selectionBox.Width = 0;
            selectionBox.Height = 0;

            // Make the drag selection box visible.
            //selectionBox.Visibility = Visibility.Visible;

            Rect rect1 = new Rect(Canvas.GetLeft(selectionBox), Canvas.GetTop(selectionBox), selectionBox.Width, selectionBox.Height);



            foreach (Rect rects in tileRects)
            {

                if (rect1.IntersectsWith(rects))
                {
                    tileList[tileRects.IndexOf(rects)].SelectTile2();
                    SelectedTile = tileList[tileRects.IndexOf(rects)].CopyTile();
                }
                else
                {
                    tileList[tileRects.IndexOf(rects)].DeselectTile();
                }
            }

            mouseDown = false;

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Release the mouse capture and stop tracking it.
            mouseDown = false;
            theGrid.ReleaseMouseCapture();

            // Hide the drag selection box.
            selectionBox.Visibility = Visibility.Collapsed;

            Point mouseUpPos = e.GetPosition(theGrid);

            // TODO: 
            //
            // The mouse has been released, check to see if any of the items 
            // in the other canvas are contained within mouseDownPos and 
            // mouseUpPos, for any that are, select them!
            //
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {

                Rect rect1 = new Rect(Canvas.GetLeft(selectionBox), Canvas.GetTop(selectionBox), selectionBox.Width, selectionBox.Height);



                foreach (Rect rects in tileRects)
                {

                    if (rect1.IntersectsWith(rects))
                    {
                        tileList[tileRects.IndexOf(rects)].SelectTile(SelectedTile);
                    }
                    else
                    {
                        tileList[tileRects.IndexOf(rects)].DeselectTile();
                    }
                }

                // When the mouse is held down, reposition the drag selection box.

                Point mousePos = e.GetPosition(theGrid);

                if (mouseDownPos.X < mousePos.X)
                {
                    Canvas.SetLeft(selectionBox, mouseDownPos.X);
                    selectionBox.Width = mousePos.X - mouseDownPos.X;
                }
                else
                {
                    Canvas.SetLeft(selectionBox, mousePos.X);
                    selectionBox.Width = mouseDownPos.X - mousePos.X;
                }

                if (mouseDownPos.Y < mousePos.Y)
                {
                    Canvas.SetTop(selectionBox, mouseDownPos.Y);
                    selectionBox.Height = mousePos.Y - mouseDownPos.Y;
                }
                else
                {
                    Canvas.SetTop(selectionBox, mousePos.Y);
                    selectionBox.Height = mouseDownPos.Y - mousePos.Y;
                }
            }
        }


        private void Grid_MouseDown_Left(object sender, MouseButtonEventArgs a)
        {
            // Capture and track the mouse.
            mouseDown_Left = true;
            mouseDownPos_Left = a.GetPosition(TheOtherGrid);
            TheOtherGrid.CaptureMouse();

            // Initial placement of the drag selection box.         
            Canvas.SetLeft(selectionBox_Left, mouseDownPos_Left.X);
            Canvas.SetTop(selectionBox_Left, mouseDownPos_Left.Y);
            selectionBox_Left.Width = 0;
            selectionBox_Left.Height = 0;

            // Make the drag selection box visible.
            selectionBox_Left.Visibility = Visibility.Visible;

            Rect rect2 = new Rect(Canvas.GetLeft(selectionBox_Left), Canvas.GetTop(selectionBox_Left), selectionBox_Left.Width, selectionBox_Left.Height);


            Deselect_All();

            foreach (Rect rects3 in tileRects2)
            {

                if (rect2.IntersectsWith(rects3))
                {
                    tileList2[tileRects2.IndexOf(rects3)].SelectTile2();
                    SelectedTile = tileList2[tileRects2.IndexOf(rects3)].CopyTile();
                }
                else
                {
                    tileList2[tileRects2.IndexOf(rects3)].DeselectTile();
                }
            }



        }


        private void Grid_MouseUp_Left(object sender, MouseButtonEventArgs a)
        {
            // Release the mouse capture and stop tracking it.
            mouseDown_Left = false;
            TheOtherGrid.ReleaseMouseCapture();

            // Hide the drag selection box.
            selectionBox_Left.Visibility = Visibility.Collapsed;

            Point mouseUpPos_Left = a.GetPosition(TheOtherGrid);
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
            Deselect_All();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Title = "Load Tileset";
            //dlg.Filter = "All Files (Images)|*.png;*.jpg;*.jpeg|JPEG Files (*.jpeg, *jpg)|*.jpeg;*.jpg|PNG Files (*.png)|*.png";
            dlg.Filter = "Tileset (XML)|*.xml";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                TilesetName.Text = "Tileset: " + filename;

                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                List<string> locations = new List<string>();
                foreach (XmlAttribute xa in doc.SelectNodes("tileset//sprites//sprite/@doc"))
                {
                    string location = xa.Value.ToString();
                    Trace.WriteLine(location);
                    locations.Add(location);
                }

                for (int i = 0; i < locations.Count; i++)
                {
                    ImageBrush EditorListBrush;
                    EditorListBrush = new ImageBrush(new BitmapImage(new Uri(String.Format("file:///{0}/" + locations[i], currentAssemblyParentPath))));
                    tileList2[i].SelectTile(EditorListBrush);
                }
                
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

            foreach (Tile tile in tileList)
            {
                tileRects.Add(new Rect(Canvas.GetLeft(tile.getRect()), Canvas.GetTop(tile.getRect()), tile.getRect().Width, tile.getRect().Height));
            }
        }

        private void DisplayGrid2()
        {
            tg_width = 7;
            tg_height = 15;
            int size = 32;
            int top = 0;
            int left = 0;

            for (int row = 0; row < tg_height; row++)
            {
                for (int col = 0; col < tg_width; col++)
                {

                    tileList2.Add(new Tile(tilegetter, "", top, left));

                    left += (size + 1);
                }

                left = 0;
                top += (size + 1);
                tiles++;
            }



            canvasman.Width = (width * 32) + width;
            canvasman.Height = (height * 32) + height;

            foreach (Tile tile2 in tileList2)
            {
                tileRects2.Add(new Rect(Canvas.GetLeft(tile2.getRect()), Canvas.GetTop(tile2.getRect()), tile2.getRect().Width, tile2.getRect().Height));
            }
        }



        private void UpdateGrid(object sender, MouseEventArgs e)
        {

        }

    }


 }
