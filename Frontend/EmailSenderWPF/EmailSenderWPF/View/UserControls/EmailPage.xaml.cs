using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using EmailSenderWPF.Scripts;

namespace EmailSenderWPF.View.UserControls
{
    // All functionality for the 'Create Email' page
    public partial class EmailPage : UserControl
    {
        public EmailPage()
        {
            InitializeComponent();
        }

        // When 'Send Email' button is clicked
        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NoErrorsWithFrontendData())
            {
                SendEmail();
            }

            // Validate frontend data
            bool NoErrorsWithFrontendData()
            {
                List<string> foundErrors = FindErrors();
                if (foundErrors.Count > 0)
                {
                    ShowErrors(foundErrors);

                    return false;
                }
                else
                {
                    return true;
                }



                // Return all frontend data validation errors
                List<string> FindErrors()
                {
                    List<string> foundErrors = new List<string>();
                    ValidateEmail();
                    ValidateSubject();
                    ValidateMessage();
                    return foundErrors;



                    void ValidateEmail()
                    {
                        bool recipientIsBlank = IsEmpty(recipientInput);

                        if (recipientIsBlank)
                        {
                            Add("Recipient email cannot be blank!");
                        }

                        // Recipient isn't blank, but it lacks a '@' or a '.'
                        if (!recipientIsBlank && (!recipientInput.InputBlock.Text.Contains("@") || !recipientInput.InputBlock.Text.Contains(".")))
                        {
                            Add("Recipient email is not valid. Example: This@that.com");
                        }

                    }
                    void ValidateSubject()
                    {
                        if (IsEmpty(subjectInput))
                        {
                            Add("Subject line cannot be blank!");
                        }
                    }
                    void ValidateMessage()
                    {
                        if (string.IsNullOrWhiteSpace(messageInput.InputBlock.Text))
                        {
                            Add("You must write a message!");
                        }
                    }

                    // Check if field is empty
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
                    // Add an error message to the list
                    void Add(string errorText)
                    {
                        foundErrors.Add(errorText);
                    }
                }

                // Show all errors (If any are found)
                static void ShowErrors(List<string> allErrors)
                {
                    // Generate popup
                    MessageBox.Show(GenerateErrorMessage(), "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);

                    string GenerateErrorMessage()
                    {
                        string errorMessage = "";

                        int indexCount = 0;
                        foreach (string error in allErrors)
                        {
                            indexCount++;
                            errorMessage += error;

                            if (!ErrorIsLastInList())
                            {
                                errorMessage += ",\n\n";
                            }


                            // Check if current error is the last error found
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
            }


            async void SendEmail()
            {
                // Create email from text fields
                var newEmail = new SendableEmail()
                {
                    Sender = "FakeEmail@NotReal.com",
                    Recipient = recipientInput.InputBlock.Text,
                    Subject = subjectInput.InputBlock.Text,
                    Body = messageInput.InputBlock.Text
                };


                // Communicate with Email API
                var client = new HttpClient();
                client.BaseAddress = new Uri(GetServerAddress());

                // Serialize email
                var json = JsonSerializer.Serialize(newEmail);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send email
                await client.PostAsync("/api/email", content);

                // Gets the current ServerURI from App.config
                string GetServerAddress()
                {
                    string? address = ConfigurationManager.AppSettings["ServerURI"];

                    // Returns a local host if address is null
                    if (address == null)
                    {
                        return "http://localhost:5178";
                    }
                    else
                    {
                        return address;
                    }
                }
            }


        }
    }
}
