using System.Windows;
using System.Windows.Controls;

namespace EmailSenderWPF.View.UserControls
{
    /// <summary>
    /// </summary>
    public partial class InputField : UserControl
    {
        public InputField()
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

        // Clear user's text if button is clicked
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            InputBlock.Clear(); // Clear text
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
