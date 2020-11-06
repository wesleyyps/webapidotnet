using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class User : UserDTO
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}