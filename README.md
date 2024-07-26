# Clarity Email Sender
## Description

Clarity Email Sender is a simple C# email sending platform comprised of the following:
- Frontend: A WPF application used to take in a sample email inputs (Recipient, Subject, Message)
- Backend: An Email API used for receiving email data, passing it to an EmailSender.dll, and storing emails in an SQL database
- Additional Libraries: An EmailSender.dll file which processes email requests from the API using SendGrid


## Installation
Download as a ZIP file, or clone the repo

```bash
git clone https://github.com/Dkillington/clarity-email-sender
```

### Configure Server Host Addresses
#### In Frontend/EmailSenderWPF/EmailSenderWPF.sln:
Configure ServerURI in App.config
```c#
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="ServerURI" value="http://{YourHostNameHere}" />
	</appSettings>
</configuration>
```
#### In Backend/EmailAPI/EmailAPI.sln:
Configure @EmailAPI_HostAddress in EmailAPI.http
```c#
@EmailAPI_HostAddress = http://{YourHostNameHere}

GET {{EmailAPI_HostAddress}}/weatherforecast/
Accept: application/json
```

### Configure appsettings.json
#### In Backend/EmailAPI/EmailAPI.sln:
Change "DefaultConnection" to a valid SQL server connection string
```c#
  // SQL Server Connection String
  "ConnectionStrings": { "DefaultConnection": "{YourConnectionStringHere}" },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
```


Provide a valid SendGridAPI key and SendGridEmail
```c#
  // Custom settings for SendGrid functionality
  "AddedCredentials": {
    "SendGridAPIKey": {YourAPIKeyHere}, // API Key
    "SendGridEmail": {YourSendGridEmailHere} // Email used for SendGrid.com
  }
```

### Create Migrations
#### In Backend/EmailAPI/EmailAPI.sln:
Open Package Manager Console:
- Add initial migration
```bash
Add-Migration InitialCreate
```
- Update database
```bash
Update-Database
```


## Usage
### Run Programs in the Following Order:
- Run EmailAPI 
- Run EmailSenderWPF

### Use EmailSenderWPF to Send Email
- Click 'Start' button
- Enter recipient email, subject line, and message to send
- Click 'Send Email' button