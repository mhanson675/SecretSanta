using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Data;
using SecretSanta.Models;
using SecretSanta.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Controllers
{
    public class SecretSantaController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly IMailer mailer;
        private readonly SecretSantaHat santaHat;

        public SecretSantaController(IWebHostEnvironment env, ApplicationDbContext context, IMailer mailer)
        {
            this.env = env;
            this.mailer = mailer;
            santaHat = new SecretSantaHat(context.Participants.ToList());
        }

        public async Task<IActionResult> PullNames()
        {
            var pairs = santaHat.PullNames();

            var body = GetMessageBody();

            var model = new VerificationViewModel();

            foreach (var pair in pairs)
            {
                var message = new SantaMessage(pair.Key, pair.Value, body, "25", "2 Dec");
                await mailer.SendEmailAsync(message);
                //TODO: Add email sending verification
                model.AddPaired(pair.Key, pair.Value);
                model.AddStatus(pair.Key, true);
            }

            return View(model);
        }

        private string GetMessageBody()
        {
            string body = "";
            string webRoot = env.WebRootPath;
            string path = Path.Combine(webRoot, "SantaMessage.html");

            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }

            return body;
        }
    }
}