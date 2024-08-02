using Microsoft.AspNetCore.Identity;

namespace FundingSouqAssessment.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    { 
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public ICollection<Client> Clients { get; set; } = new List<Client>();
        public ICollection<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>(); 
    }
}
