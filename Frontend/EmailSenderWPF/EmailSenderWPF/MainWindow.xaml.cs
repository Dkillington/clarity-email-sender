using EmailSenderWPF.View.UserControls;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;

namespace EmailSenderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen; // Set the application to the center of the screen

            InitializeComponent();

            Open(welcomePage); 
        }



        // For opening specifc pages
        public void Open(UserControl control)
        {
            HideAllPanels();
            control.Visibility = Visibility.Visible;
        }


        // Hide all pages/panels
        public void HideAllPanels()
        {
            welcomePage.Visibility = Visibility.Hidden;
            emailPage.Visibility = Visibility.Hidden;
        }

    }
}