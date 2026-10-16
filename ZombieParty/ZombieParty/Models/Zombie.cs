using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZombieParty.ViewModels;

namespace ZombieParty.Models
{
    public class Zombie
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        [StringLength(255)]
        public string ShortDesc { get; set; }

        [Range(1, 20, ErrorMessage = "{0} requires a value between {1} and {2}.")]
        public int Point { get; set; }

        // FACULTATIF on peut formellement identifier le champ lien
        // sinon le champ de foreignKey sera auto généré dans la BD
        [Display(Name = "Zombie Type")]
        [ForeignKey("ZombieType")]
        public int ZombieTypeId { get; set; }
        //[ValidateNever]
        //public ZombieType? ZombieType { get; set; }

        //[ValidateNever] 
        //public List<HuntingLog> HuntingLogs { get; set; }

        [ValidateNever]
        public virtual ZombieType? ZombieType { get; set; } //J'ai ajouter Virtual virtual permet à Entity Framework Core d'utiliser le Lazy Loading avec les proxies.

        //Tu ajoutes virtual uniquement devant les propriétés de navigation, pas devant les propriétés normales comme Id, Name, Point ou ZombieTypeId.

        [ValidateNever]
        public virtual List<HuntingLog> HuntingLogs { get; set; } //J'ai ajouter Virtual virtual permet à Entity Framework Core d'utiliser le Lazy Loading avec les proxies.


    }
}
