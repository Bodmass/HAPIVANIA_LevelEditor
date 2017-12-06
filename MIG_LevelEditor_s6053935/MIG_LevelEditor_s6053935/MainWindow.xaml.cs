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
using System.Text.RegularExpressions;

namespace MIG_LevelEditor_s6053935
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int width;
        int height;
        public MainWindow()
        {
            InitializeComponent();
            int.TryParse((string)SetWidth.Text, out width);
            int.TryParse((string)SetHeight.Text, out height);
            OverallSize.Text = "Level Size: " + (width * 32).ToString() + " x " + (height * 32).ToString();


        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            MakeLevelGrid.Visibility = Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MakeLevelGrid.Visibility = Visibility.Hidden;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Editor win = new Editor();
            win.Show();
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse((string)SetWidth.Text, out width);
            UpdateText();
        }

        private void TextChanged2(object sender, TextChangedEventArgs e)
        {
            int.TryParse((string)SetHeight.Text, out height);
            UpdateText();
        }

        private void UpdateText()
        {
            if (OverallSize != null)
            {
                OverallSize.Text = "Level Size: " + (width * 32).ToString() + " x " + (height * 32).ToString();
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }


}
