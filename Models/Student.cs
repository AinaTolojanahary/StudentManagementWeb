using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementWeb.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le prénom est requis.")]
        [StringLength(50, ErrorMessage = "Le prénom ne peut pas dépasser 50 caractères.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "Email invalide.")]
        [StringLength(100, ErrorMessage = "L'email ne peut pas dépasser 100 caractères.")]
        public required string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}