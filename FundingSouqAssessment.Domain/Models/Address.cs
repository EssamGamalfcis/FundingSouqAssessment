using System.ComponentModel.DataAnnotations;

namespace FundingSouqAssessment.Models
{
    public class CreateAccountDto
    {
        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Balance { get; set; }
    }
}
