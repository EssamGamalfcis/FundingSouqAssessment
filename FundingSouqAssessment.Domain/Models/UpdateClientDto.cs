using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundingSouqAssessment.Models
{
    public class UpdateClientDto
    {
        [Required]
        public long Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(60)]
        public string FirstName { get; set; }

        [StringLength(60)]
        public string LastName { get; set; }

        [StringLength(11)]
        public string PersonalId { get; set; }

        [Phone]
        public string MobileNumber { get; set; }

        public string Gender { get; set; }

        public List<CreateAccountDto> Accounts { get; set; } = new List<CreateAccountDto>();
    }
}
