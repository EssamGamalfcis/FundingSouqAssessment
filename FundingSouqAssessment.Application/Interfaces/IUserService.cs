using FundingSouqAssessment.Models; 
using FundingSouqAssessment.Domain.Entities;
using System.Threading.Tasks;

namespace FundingSouqAssessment.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> RegisterAsync(RegisterModel model);
        Task<string> LoginAsync(LoginModel model);
    }
}
