using System.ComponentModel.DataAnnotations;

namespace UserService.DTO
{
    public class UserCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
