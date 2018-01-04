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
        List<string> locations = new List<string>();
        ImageBrush SelectedTile;
        string SelectedTileName;
        
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

        Point mouseDownPos_Left; // The point where the mouse button was clicked down.

        private void Setup()
        {
            SelectedTile = new ImageBrush(new BitmapImage(new Uri(String.Format("file:///{0}/" + "Cave/Cave_00.png", currentAssemblyParentPath))));
            SelectedTileName = "Cave/Cave_00.png";
        }

        private void Deselect_All()
        {
            //Set off screen
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

            // Track the mouse.
            mouseDown = true;
            mouseDownPos = e.GetPosition(theGrid);
            theGrid.CaptureMouse();

            // Initial placement of the drag selection box.         
            Canvas.SetLeft(selectionBox, mouseDownPos.X);
            Canvas.SetTop(selectionBox, mouseDownPos.Y);
            selectionBox.Width = 0;
            selectionBox.Height = 0;

            // Make the selection box visible.
            selectionBox.Visibility = Visibility.Visible;

        }

        private void Grid_CopyTile(object sender, MouseButtonEventArgs e)
        {
            // Capture and track the mouse.
            mouseDown = true;
            mouseDownPos = e.GetPosition(theGrid);
            theGrid.CaptureMouse();
      
            Canvas.SetLeft(selectionBox, mouseDownPos.X);
            Canvas.SetTop(selectionBox, mouseDownPos.Y);
            selectionBox.Width = 0;
            selectionBox.Height = 0;

            Rect rect1 = new Rect(Canvas.GetLeft(selectionBox), Canvas.GetTop(selectionBox), selectionBox.Width, selectionBox.Height);

            foreach (Rect rects in tileRects)
            {

                if (rect1.IntersectsWith(rects))
                {
                    tileList[tileRects.IndexOf(rects)].SelectTile2();
                    SelectedTile = tileList[tileRects.IndexOf(rects)].CopyTile();
                    SelectedTileName = tileList[tileRects.IndexOf(rects)].getImageName();
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

            Rect rect1 = new Rect(Canvas.GetLeft(selectionBox), Canvas.GetTop(selectionBox), selectionBox.Width, selectionBox.Height);



            foreach (Rect rects in tileRects)
            {

                if (rect1.IntersectsWith(rects))
                {
                    tileList[tileRects.IndexOf(rects)].SelectTile(SelectedTile, SelectedTileName);
                }
            }

            // Release the mouse capture and stop tracking it.
            mouseDown = false;
            theGrid.ReleaseMouseCapture();

            // Hide the drag selection box.
            selectionBox.Visibility = Visibility.Collapsed;

            Point mouseUpPos = e.GetPosition(theGrid);

        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {

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
            //Check when the Tiles on the Left have been clicked
            mouseDownPos_Left = a.GetPosition(TheOtherGrid);
            TheOtherGrid.CaptureMouse();

            Deselect_All();

            foreach (Rect rects3 in tileRects2)
            {

                if (rects3.Contains(mouseDownPos_Left))
                {
                    tileList2[tileRects2.IndexOf(rects3)].SelectTile2();
                    SelectedTile = tileList2[tileRects2.IndexOf(rects3)].CopyTile();
                    SelectedTileName = tileList2[tileRects2.IndexOf(rects3)].getImageName();
                }
                else
                {
                    tileList2[tileRects2.IndexOf(rects3)].DeselectTile();
                }
            }



        }


        private void Grid_MouseUp_Left(object sender, MouseButtonEventArgs a)
        {
            // Release the mouse capture
            TheOtherGrid.ReleaseMouseCapture();

            // Hide the drag selection box.
            selectionBox_Left.Visibility = Visibility.Collapsed;

            Point mouseUpPos_Left = a.GetPosition(TheOtherGrid);
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
                locations.Clear();
                string filename = dlg.FileName;
                TilesetName.Text = "Tileset: " + filename;

                XmlDocument doc = new XmlDocument();
                doc.Load(filename);

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
                    string LocationString = locations[i];
                    tileList2[i].SelectTile(EditorListBrush, LocationString);
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

                    left += (size);
                }

                left = 0;
                top += (size);
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

        private void NewLevel(object sender, RoutedEventArgs e)
        {
            SelectedTile = new ImageBrush(new BitmapImage(new Uri(String.Format("file:///{0}/" + "Cave/Cave_00.png", currentAssemblyParentPath))));
            SelectedTileName = "Cave/Cave_00.png";

            foreach (Rect rects in tileRects)
            {
                tileList[tileRects.IndexOf(rects)].SelectTile(SelectedTile, SelectedTileName);
                tileList[tileRects.IndexOf(rects)].DeselectTile();
            }

            foreach (Rect rects in tileRects2)
            {
                tileList2[tileRects2.IndexOf(rects)].SelectTile(SelectedTile, SelectedTileName);
                tileList2[tileRects2.IndexOf(rects)].DeselectTile();
            }

            locations.Clear();
            TilesetName.Text = "Tileset: [Nothing Loaded]";

        }

        private void LoadLevel(object sender, RoutedEventArgs e)
        {
            Deselect_All();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".xml";
            dlg.Title = "Load Level";
            dlg.Filter = "Level (XML)|*.xml";



            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;

                XmlDocument doc = new XmlDocument();
                doc.Load(filename);


                List<int> tilex = new List<int>();
                List<int> tiley = new List<int>();
                List<string> tilel = new List<string>();
                int index = 0;

                foreach (XmlAttribute xa in doc.SelectNodes("level//tiles//tile/@x"))
                {
                    
                    int location = int.Parse(xa.Value);
                    tilex.Add(location/32);
                    index++;
                }

                foreach (XmlAttribute xa in doc.SelectNodes("level//tiles//tile/@y"))
                {
                    int location = (int.Parse(xa.Value) / 32);
                    tiley.Add(location);
                }

                foreach (XmlAttribute xa in doc.SelectNodes("level//tiles//tile/@look"))
                {
                    string location = xa.Value.ToString();
                    tilel.Add(location);
                }

                foreach (XmlAttribute xb in doc.SelectNodes("level//sprites//sprite/@location"))
                {
                    bool notadded = true;
                    string location = xb.Value.ToString();
                    foreach (string s in locations)
                    {
                        if (s == location)
                        {
                            notadded = false;
                            break;
                        }
                    }

                    if (notadded)
                    {
                        Trace.WriteLine(location);
                        locations.Add(location);
                    }
                }

                for (int i = 0; i < locations.Count; i++)
                {
                    ImageBrush EditorListBrush;
                    EditorListBrush = new ImageBrush(new BitmapImage(new Uri(String.Format("file:///{0}/" + locations[i], currentAssemblyParentPath))));
                    string LocationString = locations[i];
                    tileList2[i].SelectTile(EditorListBrush, LocationString);
                }


                ImageBrush RememberTile = SelectedTile;
                String RememberTileName = SelectedTileName;


                SelectedTile = new ImageBrush(new BitmapImage(new Uri(String.Format("file:///{0}/" + "Cave/Cave_00.png", currentAssemblyParentPath))));
                SelectedTileName = "Cave/Cave_00.png";

                foreach (Rect rects in tileRects)
                {
                    tileList[tileRects.IndexOf(rects)].SelectTile(SelectedTile, SelectedTileName);
                    tileList[tileRects.IndexOf(rects)].DeselectTile();
                }

                
                foreach (Tile t in tileList)
                {
                    for (int i = 0; i < index; i++)
                    {
                        if((t.getX()/32) == tilex[i] && (t.getY()/32) == tiley[i])
                        {
                            SelectedTile = new ImageBrush(new BitmapImage(new Uri(String.Format("file:///{0}/" + tilel[i], currentAssemblyParentPath))));
                            SelectedTileName = tilel[i];
                            t.SelectTile(SelectedTile, SelectedTileName);
                            
                        }
                    }
                }

                SelectedTile = RememberTile;
                SelectedTileName = RememberTileName;

            }
        }

        private void SaveLevel(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);


            XmlNode levelNode = doc.CreateElement("level");
            doc.AppendChild(levelNode);

            XmlNode spritesNode = doc.CreateElement("sprites");
            levelNode.AppendChild(spritesNode);

            for (int i = 0; i < locations.Count(); i++)
            {
                XmlNode spriteNode = doc.CreateElement("sprite");
                XmlAttribute spriteAttribute = doc.CreateAttribute("location");
                spriteAttribute.Value = locations[i];
                spriteNode.Attributes.Append(spriteAttribute);
                spritesNode.AppendChild(spriteNode);
            }

            XmlNode tilesNode = doc.CreateElement("tiles");
            levelNode.AppendChild(spritesNode);

            for (int i = 0; i < tileList.Count(); i++)
            {
                bool hastile = false;

                Trace.WriteLine(tileList[i].getImageName());

                if (tileList[i].getImageName() != tileList2[0].getImageName())
                {
                    hastile = true;
                }


                if (hastile)
                {
                    XmlNode tileNode = doc.CreateElement("tile");
                    XmlAttribute tileAttribute = doc.CreateAttribute("x");
                    tileAttribute.Value = tileList[i].getX().ToString();
                    tileNode.Attributes.Append(tileAttribute);

                    XmlAttribute tileAttribute2 = doc.CreateAttribute("y");
                    tileAttribute2.Value = tileList[i].getY().ToString();
                    tileNode.Attributes.Append(tileAttribute2);

                    XmlAttribute tileAttribute3 = doc.CreateAttribute("look");
                    tileAttribute3.Value = tileList[i].getImageName();
                    tileNode.Attributes.Append(tileAttribute3);

                    tilesNode.AppendChild(tileNode);
                }
            }

            levelNode.AppendChild(tilesNode);


            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Title = "Save Level";
            dlg.FileName = "Untitled Level";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (.xml)|*.xml";
            

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string levelname = dlg.FileName;
                doc.Save(levelname);
                this.Title = "MIG_LevelEditor_s6053935 : '" + levelname + "'";
            }
        }
    }


 }
