using System.ComponentModel.DataAnnotations;

namespace UserService.DTO
{
    public class UserCreateDTO
    {
        [Required]
        public string Email { get; set; }

        public string Uid { get; set; }
    }
}
