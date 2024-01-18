using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class Users
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }

        public string Uid { get; set; }
    }
}
