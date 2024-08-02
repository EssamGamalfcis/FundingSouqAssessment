using FundingSouqAssessment.Application.Interfaces;
using FundingSouqAssessment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundingSouqAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients([FromQuery] string search, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var clients = await _clientService.GetClientsAsync(search, pageNumber, pageSize);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(long id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto clientDto, [FromForm] IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                var photoUrl = string.Empty;

                if (photo != null && photo.Length > 0)
                {
                    photoUrl = await UploadPhotoAsync(photo);
                }

                await _clientService.CreateClientAsync(clientDto, photoUrl);
                return Ok("Client created successfully.");
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientDto clientDto)
        {
            if (ModelState.IsValid)
            {
                await _clientService.UpdateClientAsync(clientDto);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(long id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }
        private async Task<string> UploadPhotoAsync(IFormFile photo)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", photo.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            } 
            return $"/images/{photo.FileName}";
        }

    }
}
