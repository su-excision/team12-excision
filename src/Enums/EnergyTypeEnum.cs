using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Enumerated type for the different energy types
    /// of Pokémon Trading Cards.
    /// </summary>
    public enum EnergyType
    {
        /// <summary>
        /// Energy Type is undefined
        /// </summary>
        [Display(Name = "All")]
        All = 0,

        /// <summary>
        /// Normal Energy
        /// </summary>
        [Display(Name = "Normal")]
        Normal = 1,

        /// <summary>
        /// Bug Energy
        /// </summary>
        [Display(Name = "Bug")]
        Bug = 2,

        /// <summary>
        /// Grass Energy
        /// </summary>
        [Display(Name = "Grass")]
        Grass = 3,

        /// <summary>
        /// Electric Energy
        /// </summary>
        [Display(Name = "Electric")]
        Electric = 4,

        /// <summary>
        /// Darkness Energy
        /// </summary>
        [Display(Name = "Darkness")]
        Darkness = 5,

        /// <summary>
        /// Fairy Energy
        /// </summary>
        [Display(Name = "Fairy")]
        Fairy = 6,

        /// <summary>
        /// Fire Energy
        /// </summary>
        [Display(Name = "Fire")]
        Fire = 7,

        /// <summary>
        /// Psychic Energy
        /// </summary>
        [Display(Name = "Psychic")]
        Psychic = 8,

        /// <summary>
        /// Ice Energy
        /// </summary>
        [Display(Name = "Ice")]
        Ice = 9,

        /// <summary>
        /// Ground Energy
        /// </summary>
        [Display(Name = "Ground")]
        Ground = 10,

        /// <summary>
        /// Poison Energy
        /// </summary>
        [Display(Name = "Poison")]
        Poison = 11,

        /// <summary>
        /// Rock Energy
        /// </summary>
        [Display(Name = "Rock")]
        Rock = 12,

        /// <summary>
        /// Ghost Energy
        /// </summary>
        [Display(Name = "Ghost")]
        Ghost = 13,

        /// <summary>
        /// Steel Energy
        /// </summary>
        [Display(Name = "Steel")]
        Steel = 14,

        /// <summary>
        /// Dragon Energy
        /// </summary>
        [Display(Name = "Dragon")]
        Dragon = 15,

        /// <summary>
        /// Water Energy
        /// </summary>
        [Display(Name = "Water")]
        Water = 16,

        /// <summary>
        /// Fighting Energy
        /// </summary>
        [Display(Name = "Fighting")]
        Fighting = 17,

        /// <summary>
        /// Flying Energy
        /// </summary>
        [Display(Name = "Flying")]
        Flying = 18,

        /// <summary>
        /// Colorless Energy
        /// </summary>
        [Display(Name = "Colorless")]
        Colorless = 19,
    }
}
