using AutoMapper;
using FundingSouqAssessment.Application.Interfaces;
using FundingSouqAssessment.Domain.Entities;
using FundingSouqAssessment.Infrastructure.Data;
using FundingSouqAssessment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundingSouqAssessment.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ClientService(AppDbContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }

        public async Task<IEnumerable<Client>> GetClientsAsync(string search, int pageNumber, int pageSize)
        {
            var clients = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                clients = clients.Where(c => c.FirstName.Contains(search) || c.LastName.Contains(search) || c.Email.Contains(search));
            }

            return await clients.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(long id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task CreateClientAsync(CreateClientDto clientDto, string photoUrl)
        {
            clientDto.ProfilePhotoUrl = photoUrl;
            var client = _mapper.Map<Client>(clientDto); 
            if (!client.Accounts.Any())
            {
                throw new InvalidOperationException("At least one account is required.");
            }

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(UpdateClientDto clientDto)
        {
            var client = await _context.Clients
                .Include(c => c.Accounts)   
                .FirstOrDefaultAsync(c => c.Id == clientDto.Id);

            if (client != null)
            {
                _mapper.Map(clientDto, client); 
                client.Accounts.Clear();
                foreach (var accountDto in clientDto.Accounts)
                {
                    var account = _mapper.Map<Account>(accountDto);
                    client.Accounts.Add(account);
                }

                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteClientAsync(long id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
