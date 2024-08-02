using Microsoft.AspNetCore.Identity;

namespace FundingSouqAssessment.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Name { get; set; }
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>(); 
    }
}
