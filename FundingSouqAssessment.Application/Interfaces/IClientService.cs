using FundingSouqAssessment.Domain.Entities;
using FundingSouqAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundingSouqAssessment.Application.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetClientsAsync(string search, int pageNumber, int pageSize);
        Task<Client> GetClientByIdAsync(long id);
        Task CreateClientAsync(CreateClientDto clientDto, string photoUrl);
        Task UpdateClientAsync(UpdateClientDto clientDto);
        Task DeleteClientAsync(long id);
    }
}
