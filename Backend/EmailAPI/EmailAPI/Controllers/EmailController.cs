using EmailAPI.Data;
using EmailAPI.Models.DTOs;
using EmailAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailAPI.Controllers
{
    [Route("api/email")]
    [ApiController]

    public class EmailController : ControllerBase
    {
        public readonly ApplicationDbContext dbContext;
        private readonly IConfiguration _configuration;

        public EmailController(ApplicationDbContext givenContext, IConfiguration configuration)
        {
            dbContext = givenContext;
            _configuration = configuration;
        }


        // Read
        [HttpGet]
        public async Task<IActionResult> GetAllEmails()
        {
            var allEmails = await dbContext.Emails.ToListAsync(); // Grab all emails asynchronously

            if(allEmails == null)
            {
                return NotFound(); // Return 404
            }

            return Ok(allEmails); // Return all emails with a code 200
        }

        // Read (Specific email)
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEmail(int id)
        {
            var foundEmail = await dbContext.Emails.FindAsync(id); 

            if (foundEmail == null)
            {
                return NotFound();
            }

            return Ok(foundEmail); 
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateEmail(CreateEmailDto emailDto)
        {
            if(EmailIsNotValid())
            {
                return NotFound();
            }

            // Copy over given values to newly created email
            var createdEmail = new Email()
            {
                Sender = emailDto.Sender,
                Recipient = emailDto.Recipient,
                Subject = emailDto.Subject,
                Body = emailDto.Body,
            };

            if(createdEmail == null)
            {
                return NotFound();
            }

            // Send Email
            var apiKey = _configuration["AddedCredentials:SendGridAPIKey"]; // Grab API key from appsettings.json
            createdEmail.Sender = _configuration["AddedCredentials:SendGridEmail"]; // Grab SendGrid email from appsettings.json
            SendGrid.Response response = await EmailSender.SendFunctionality.Execute(apiKey, "John Doe", createdEmail.Sender, "Test Recipient", createdEmail.Recipient, createdEmail.Subject, createdEmail.Body);

            AssignStatusToEmail();

            // Add
            await dbContext.Emails.AddAsync(createdEmail);

            // Save
            await dbContext.SaveChangesAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(createdEmail);
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return Accepted(createdEmail);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Unauthorized(createdEmail);
            }
            else
            {
                return BadRequest(createdEmail);
            }

            // Check for invalid email details
            bool EmailIsNotValid()
            {
                if (emailDto == null || IsInvalid(emailDto.Sender) || IsInvalid(emailDto.Recipient) || IsInvalid(emailDto.Subject) || IsInvalid(emailDto.Body))
                {
                    return true;
                }

                return false;


                bool IsInvalid(string text)
                {
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            // Make sure email gets correct response status (Sent or Failed)
            void AssignStatusToEmail()
            {
                if (StatusCodeOk())
                {
                    createdEmail.Status = EmailStatus.Sent;
                }
                else
                {
                    createdEmail.Status = EmailStatus.Failed;
                }

                // Verify returned status code is OK (Email sent)
                bool StatusCodeOk()
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        // Update
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmail(UpdateEmailDto emailDto, int id)
        {
            // Find
            var foundEmail = await dbContext.Emails.FindAsync(id); // Grab specific email asynchronously

            if (foundEmail == null)
            {
                return NotFound(); // Return 404
            }

            // Update all values
            foundEmail.Sender = emailDto.Sender;
            foundEmail.Recipient = emailDto.Recipient;
            foundEmail.Subject = emailDto.Subject;
            foundEmail.Body = emailDto.Body;
            foundEmail.Status = emailDto.Status;

            // Save
            await dbContext.SaveChangesAsync(); 

            return Ok(foundEmail);
        }

        // Update
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            // Find
            var foundEmail = await dbContext.Emails.FindAsync(id); // Grab specific email asynchronously

            if (foundEmail == null)
            {
                return NotFound(); // Return 404
            }

            // Remove
            dbContext.Emails.Remove(foundEmail);

            // Save
            await dbContext.SaveChangesAsync();

            return Ok(foundEmail);
        }
    }
}
