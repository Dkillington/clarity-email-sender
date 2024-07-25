using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for EmailPage.xaml
    /// </summary>
    public partial class EmailPage : UserControl
    {
        public EmailPage()
        {
            InitializeComponent();
        }

        // For when email is sent
        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> foundErrors = FindErrors();
            if (foundErrors.Count > 0)
            {
                ShowErrors(foundErrors);
            }
            else
            {
                CreateEmail();
            }

            List<string> FindErrors()
            {
                List<string> foundErrors = new List<string>();

                ValidateEmail();


                if (IsEmpty(subjectInput))
                {
                    Add("Subject line cannot be blank!");
                }

                

                return foundErrors;


                bool IsEmpty(InputField item)
                {
                    if (string.IsNullOrWhiteSpace(item.InputBlock.Text))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }



                void Add(string errorText)
                {
                    foundErrors.Add(errorText);
                }




                void ValidateEmail()
                {
                    if (IsEmpty(recipientInput))
                    {
                        Add("Recipient email cannot be blank!");
                    }


                    if(!recipientInput.InputBlock.Text.Contains("@") || !recipientInput.InputBlock.Text.Contains("."))
                    {
                        Add("Recipient email is not valid. Example: This@that.com");
                    }

                }
            }


            async void CreateEmail()
            {
                // Create email from text fields
                var newEmail = new SendableEmail()
                {
                    Sender = "Dkillian00@yahoo.com",
                    Recipient = recipientInput.InputBlock.Text,
                    Subject = subjectInput.InputBlock.Text,
                    Body = messageInput.InputBlock.Text
                };


                // Communicating with API
                var client = new HttpClient();
                client.BaseAddress = new Uri(AppSettingsHelper.GetAppSetting("ServerURI"));

                var json = JsonSerializer.Serialize(newEmail);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                await client.PostAsync("/api/email", content);
            }



        }
        private static void ShowErrors(List<string> allErrors)
        {
            MessageBox.Show(ReturnErrorMessage(), "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);

            string ReturnErrorMessage()
            {
                string errorMessage = "";

                int indexCount = 0;
                foreach (string error in allErrors)
                {
                    indexCount++;
                    errorMessage += error;

                    if (!ErrorIsLastInList())
                    {
                        errorMessage += ", ";
                    }

                    bool ErrorIsLastInList()
                    {
                        if (indexCount == allErrors.Count)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return errorMessage;
            }
        }

        private void SendEmail()
        {

        }
    }
}
