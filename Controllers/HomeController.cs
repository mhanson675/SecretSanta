using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecretSanta.Data;
using SecretSanta.Models;
using SecretSanta.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SecretSanta.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<HomeController> _logger;
        private readonly IMailer mailer;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, IMailer mailer)
        {
            this.context = context;
            _logger = logger;
            this.mailer = mailer;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ParticipantsList()
        {
            return View(context.Participants);
        }

        public async Task<IActionResult> Details(int id)
        {
            Participant participant = await context.Participants
                .FirstOrDefaultAsync(p => p.Id == id);
            ParticipantViewModel model = ViewModelFactory.Details(participant);
            return View("ParticipantEditor", model);
        }

        public IActionResult Create()
        {
            return View("ParticipantEditor", ViewModelFactory.Create(new Participant()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Participant participant)
        {
            if (ModelState.IsValid)
            {
                participant.Id = default;

                context.Participants.Add(participant);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(ParticipantsList));
            }
            return View("ParticipantEditor", ViewModelFactory.Create(participant));
        }

        public async Task<IActionResult> Edit(int id)
        {
            Participant participant = await context.Participants.FindAsync(id);
            ParticipantViewModel model = ViewModelFactory.Edit(participant);
            return View("ParticipantEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Participant participant)
        {
            if (ModelState.IsValid)
            {
                context.Participants.Update(participant);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(ParticipantsList));
            }
            return View("ParticipantEditor", participant);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Participant participant = await context.Participants.FindAsync(id);
            ParticipantViewModel model = ViewModelFactory.Delete(participant);
            return View("ParticipantEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] Participant participant)
        {
            context.Participants.Remove(participant);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(ParticipantsList));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
