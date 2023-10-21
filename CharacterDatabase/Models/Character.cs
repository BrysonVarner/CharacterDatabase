using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Azure.Core;
using Microsoft.AspNetCore.Identity;



namespace CharacterDatabase.Models
{
    public class Character
    {   
        public Character()
        {
            LastUpdated = DateTime.Now;
        }

        public long CharacterID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Species { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Gender { get; set; }

        [Required]
        [Range(5, 500)]
        public string Speed { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Class1 { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string? Subclass1 { get; set; }

        [Required]
        [Range(1, 50)]
        public int Class1Level { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? Class2 { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? Subclass2 { get; set; }

        [Range(1, 50)]
        public int? Class2Level { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? Class3 { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? Subclass3 { get; set; }

        [Range(1, 50)]
        public int? Class3Level { get; set; }

        [StringLength(20, MinimumLength = 2)]
        public string? Height { get; set; }

        [Range(1, 10000)]
        public string? Weight { get; set; }

        [Range(1, 10000)]
        public int? Age { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? HairColor { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? EyeColor { get; set; }

        [Required]
        [Range(1, 40)]
        public int Strength { get; set; }

        [Required]
        [Range(1, 40)]
        public int Dexterity { get; set; }

        [Required]
        [Range(1, 40)]
        public int Constitution { get; set; }

        [Required]
        [Range(1, 40)]
        public int Intelligence { get; set; }

        [Required]
        [Range(1, 40)]
        public int Wisdom { get; set; }

        [Required]
        [Range(1, 40)]
        public int Charisma { get; set; }       
        
        public string? CharacterNotes { get; set; }

        public string? UserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
