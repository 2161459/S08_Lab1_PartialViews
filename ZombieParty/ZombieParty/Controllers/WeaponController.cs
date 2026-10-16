using Microsoft.AspNetCore.Mvc;
using ZombieParty.Models;
using ZombieParty.Models.Data;
using ZombieParty.ViewModels;

namespace ZombieParty.Controllers
{
    public class WeaponController : Controller
    {
        private ZombiePartyDbContext _baseDonnees { get; set; }

        public WeaponController(ZombiePartyDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }

        public IActionResult Index()
        {
            List<Weapon> weapons = _baseDonnees.Weapons.ToList();
            return View(weapons);
        }

        //public IActionResult Create()
        //{
        //    return View("Upsert", new Weapon()); //Puisque tu as renommé Create.cshtml en Upsert.cshtml, ton action Create dans WeaponController.cs doit maintenant retourner cette vue :
        //}

        //GET
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
                // create
                return View(new Weapon()); // Envoie un objet vide, car la vue attend un objet Weapon
            else
                //update
                return View(_baseDonnees.Weapons.Find(id));
        }

        //int? id : permet de recevoir un identifiant qui peut être null.
        //id == null || id == 0 : si aucun ID n'est fourni, on crée une nouvelle arme.
        //_baseDonnees.Weapons.Find(id) : si un ID existe, on récupère l'arme dans la base de données pour la modifier.

        //[HttpPost]
        //public IActionResult /*Create*/Upsert(Weapon weapon)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Ajouter à la BD
        //        _baseDonnees.Weapons.Add(weapon);
        //        TempData["Success"] = $"{weapon.Name} weapon added";

        //        _baseDonnees.SaveChanges();

        //        return this.RedirectToAction("Index");
        //    }

        //    return this.View(weapon);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                // Create
                if (weapon.WeaponId == 0)
                {
                    // Ajouter à la BD
                    _baseDonnees.Weapons.Add(weapon);
                    TempData["Success"] = $"{weapon.Name} weapon added";
                }
                else
                {
                    // Update
                    _baseDonnees.Weapons.Update(weapon);
                    TempData["success"] = $"{weapon.Name} weapon updated";
                }
                _baseDonnees.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(weapon);
        }

    }
}
