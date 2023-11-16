using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Enumerated type for the different categories
    /// of Pokémon Trading Cards.
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// Card Type is undefined
        /// </summary>
        [Display(Name = "")]
        Undefined = 0,

        /// <summary>
        /// General Category for Pokémon Type Cards
        /// </summary>
        [Display(Name = "Pokémon - General")]
        Pokemon = 100,

        /// <summary>
        /// General Category for Trainer Type Cards
        /// </summary>
        [Display(Name = "Trainer - General")]
        Trainer = 200,

        /// <summary>
        /// General Category for Energy Type Cards
        /// </summary>
        [Display(Name = "Energy - General")]
        Energy = 300,

        /// <summary>
        /// Specific Category for Basic Energy Cards
        /// </summary>
        [Display(Name = "Basic Energy Card")]
        EnergyBasic = 301,

        /// <summary>
        /// Specific Category for Special Energy Cards
        /// </summary>
        [Display(Name = "Special Energy Card")]
        EnergySpecial = 302
    }
}
