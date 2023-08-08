using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models
{
    ///// <summary>
    ///// Платформа
    ///// </summary>
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Cost { get; set; }
    }
}