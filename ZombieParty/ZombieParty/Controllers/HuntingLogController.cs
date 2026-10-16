using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using ZombieParty.Models;
using ZombieParty.Models.Data;

namespace ZombieParty.Controllers
{
    public class HuntingLogController : Controller
    {
        private readonly ZombiePartyDbContext _baseDonnees;

        public HuntingLogController(ZombiePartyDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees; //_baseDonnees permet d'accéder à ta base de données.
        } 

        // GET : HuntingLog/Index
        public IActionResult Index()
        {
            List<HuntingLog> huntingLogs =
                _baseDonnees.HuntingLogs.ToList(); //.HuntingLogs.ToList() récupère tous les journaux.

            return View(huntingLogs); //envoie la liste à la vue Index.
        }
        // GET : HuntingLog/Upsert    Pour Hunter
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                // Create
                return View(new HuntingLog());
            }
            else
            {
                // Update
                HuntingLog? huntingLog = _baseDonnees.HuntingLogs.Find(id);

                if (huntingLog == null)
                {
                    return NotFound();
                }

                return View(huntingLog);
            }

        }
        //Ce code permet de créer et de modifier les champs Title et Description.Si HuntingLog possède d'autres champs, il faudra aussi les traiter dans le POST.
        // POST : HuntingLog/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(HuntingLog huntingLog)
        {
            if (ModelState.IsValid)
            {
                if (huntingLog.Id == 0)
                {
                    // CREATE
                    _baseDonnees.HuntingLogs.Add(huntingLog);

                    TempData["Success"] = "Hunting Log added";
                }
                else
                {
                    // UPDATE
                    HuntingLog? existingLog =
                        _baseDonnees.HuntingLogs.Find(huntingLog.Id);

                    if (existingLog == null)
                    {
                        return NotFound();
                    }

                    existingLog.Title = huntingLog.Title;
                    existingLog.Description = huntingLog.Description;

                    TempData["Success"] = "Hunting Log updated";
                }

                _baseDonnees.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(huntingLog);
        }
    }
}
