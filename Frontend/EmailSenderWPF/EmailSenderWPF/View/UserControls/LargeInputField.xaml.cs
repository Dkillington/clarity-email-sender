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

namespace EmailSenderWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for LargeInputField.xaml
    /// </summary>
    public partial class LargeInputField : UserControl
    {
        public LargeInputField()
        {
            InitializeComponent();
        }


        // Represents the binded text that sits over a field
        // Example: 'Enter Recipient Email Here'
        private string temporaryText;
        public string TemporaryText
        {
            get { return temporaryText; }
            set
            {
                temporaryText = value;
                TempText.Text = temporaryText;
            }
        }


        // Hide text if anything is typed into the field
        private void InputBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(InputBlock.Text))
            {
                ShowText();
            }
            else
            {
                HideText();
            }

            void HideText()
            {
                TempText.Visibility = Visibility.Hidden;
            }
            void ShowText()
            {
                TempText.Visibility = Visibility.Visible;
            }
        }
    }
}
