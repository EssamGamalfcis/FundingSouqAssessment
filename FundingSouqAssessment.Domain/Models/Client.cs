using System.ComponentModel.DataAnnotations; 

namespace FundingSouqAssessment.Models
{
    public class CreateClientDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        [Required]
        [StringLength(11)]
        public string PersonalId { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        public string Gender { get; set; }
        public string ProfilePhotoUrl { get; set; } = "";
        public List<CreateAccountDto> Accounts { get; set; } = new List<CreateAccountDto>();
    }
}
