using CMS4.Models;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS4.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        private CMS4Entities db = new CMS4Entities();
        public ActionResult SendEmail(string id)
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = db.AspNetUsers.Find(currUserId);
            var toUser = db.AspNetUsers.Find(id);
            EmailFormModel model = new EmailFormModel()
            {
                FromEmail = currUser.Email,
                FromName = currUser.UserName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})Message:</p><p>{2}</p>";
                var message = new MailMessage();
                //message.To.Add(new MailAddress("recipient@gmail.com"));  // replace with valid value 
                message.To.Add(new MailAddress(model.ToEmail));  // replace with valid value 
                //message.From = new MailAddress("sender@outlook.com");  // replace with valid value
                message.From = new MailAddress(model.FromEmail);  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
                if (model.Upload != null && model.Upload.ContentLength > 0)
                {
                    message.Attachments.Add(new Attachment(model.Upload.InputStream,
                        Path.GetFileName(model.Upload.FileName)));
                }
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = model.FromEmail,  // replace with valid value
                        //Password = "Password"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    //smtp.Host = "smtp-mail.outlook.com";
                    smtp.Host = "smtp.monash.edu.au";
                    //smtp.Port = 587;
                    //smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Send");
                }
            }
            return View(model);
        }
        public ActionResult Send()
        {
            return View();
        }
    }
}