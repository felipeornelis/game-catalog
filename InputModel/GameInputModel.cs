using System;
using System.ComponentModel.DataAnnotations;

namespace GameCatalog.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The game's name must contain between 1 and 100 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game's producer name must contain between 3 and 100 characters")]
        public string Producer { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "The game's price must be in the range of R$1 and R$1,000")]
        public double Price { get; set; }
    }
}