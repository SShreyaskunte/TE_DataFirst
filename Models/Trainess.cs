using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFirstCoreMVC.Models
{
    public partial class Trainess
    {
        [Key]
        public int Tid { get; set; }

        [Required(ErrorMessage = "Enter the Trainee Name")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Name must consist of minimum 4 characters")]
        [RegularExpression(@"^([A-Za-z]+)$")]
        public string? Tname { get; set; }

        [Range(21, 45, ErrorMessage = "Not Eligible")]
        public int? Tage { get; set; }
    }
}
