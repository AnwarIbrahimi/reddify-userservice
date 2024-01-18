using System.ComponentModel.DataAnnotations;

namespace UserService.DTO
{
    public class UserReadDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }

        public string Uid { get; set; }
    }
}
