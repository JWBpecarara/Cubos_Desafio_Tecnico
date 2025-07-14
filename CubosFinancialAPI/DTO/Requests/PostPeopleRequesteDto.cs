using System.ComponentModel.DataAnnotations;

namespace CubosFinancialAPI.DTO.Requests
{
    public class PostPeopleRequesteDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Document { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Password { get; set; }
    }
}
