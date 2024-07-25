using EmailAPI.Data;
using EmailAPI.Models.DTOs;
using EmailAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EmailSender;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

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
            var allEmails = await dbContext.emails.ToListAsync(); // Grab all emails asynchronously

            if(allEmails == null)
            {
                return NotFound(); // Return 404
            }

            return Ok(allEmails); // Return all emails with a code 200
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetEmail(int id)
        {
            var foundEmail = await dbContext.emails.FindAsync(id); // Grab specific email asynchronously

            if (foundEmail == null)
            {
                return NotFound(); // Return 404
            }

            return Ok(foundEmail); // Return given email (Status: 200)
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateEmail(CreateEmailDto emailDto)
        {
            if (emailDto == null)
            {
                return NotFound();
            }

            // Copy over emailDto values to newly created email
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


            var apiKey = _configuration["AddedCredentials:SendGridAPIKey"];
            createdEmail.Sender = _configuration["AddedCredentials:SendGridEmail"];


            // Send Email
            await EmailSender.SendFunctionality.Execute(apiKey, createdEmail.Sender, createdEmail.Recipient, createdEmail.Subject, createdEmail.Body);








            // Add
            await dbContext.emails.AddAsync(createdEmail);

            // Save
            await dbContext.SaveChangesAsync();

            
            return Ok(createdEmail);
        }

        // Update
        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmail(CreateEmailDto emailDto, int id)
        {
            // Find
            var foundEmail = await dbContext.emails.FindAsync(id); // Grab specific email asynchronously

            if (foundEmail == null)
            {
                return NotFound(); // Return 404
            }

            // Update all values
            foundEmail.Sender = emailDto.Sender;
            foundEmail.Recipient = emailDto.Recipient;
            foundEmail.Subject = emailDto.Subject;
            foundEmail.Body = emailDto.Body;

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
            var foundEmail = await dbContext.emails.FindAsync(id); // Grab specific email asynchronously

            if (foundEmail == null)
            {
                return NotFound(); // Return 404
            }

            // Remove
            dbContext.emails.Remove(foundEmail);

            // Save
            await dbContext.SaveChangesAsync();

            return Ok(foundEmail);
        }
    }
}
