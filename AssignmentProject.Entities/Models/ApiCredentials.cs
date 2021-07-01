using System.ComponentModel.DataAnnotations;

namespace AssignmentProject.Entities.Models
{
    public class ApiCredentials
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
