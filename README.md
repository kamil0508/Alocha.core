# Alocha.core

## Project patterns:
- MVC
- Reporitory
- Dependency Injection

### Features:
 - Service layer (thanks to which the controller actions are responsible mainly for generating views)
 - Inversion of control (Extensions > IoCContainer.cs)
 - Database build using Entity Framework (code first)
 - Fluent Api
 - Linq
 - Background task using IHostedService (Services > WorkerServices.cs)
 - Integration with Twilio Api to sending sms(Helpers > SmsSender.cs)
 - Sending e-mail via smtp protocol (Helpers > EmailSender.cs)
 - Registration and login system with data encryption (ASP.NET Identity)
 - User roles with different permissions ("Administrator", "Customer")
 - Generating pdf documents in iTextSharp (Helpers > PdfDocument.cs)
 - Asynchronous validation forms (client-side) (Microsoft.jQuery.Unobtrusive.Validation)
 - User data management (adding phone number and change password) 
 - Views built in bootstrap
 - Razor
 - Java Script
  - Ajax request support (e.g asynchronous calculate date)

### How to run:
 - Clone this project to your computer
 - Use the update-database command in the Visual Studio console 
 - If you want turn on e-mails & sms sending you must configure EmailSender and SmsSender class.
 - You can log in to the administrator account (Email: Administrator@poczta.pl, Hasło: Start123!)
 

 
