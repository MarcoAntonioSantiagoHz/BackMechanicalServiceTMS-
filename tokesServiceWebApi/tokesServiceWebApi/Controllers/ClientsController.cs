//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using tokesServiceWebApi.DataAccess;
//using tokesServiceWebApi.DTOs;
//using tokesServiceWebApi.Models;

//namespace tokesServiceWebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ClientsController : ControllerBase
//    {
//        private readonly TokesBdContext _dbContext;

//        public ClientsController(TokesBdContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        // Método para obtener todos los clientes
//        [HttpGet]
//        [Route("ListClients")]
//        public async Task<IActionResult> ListClients()
//        {
//            var listClients = await _dbContext.Clients.ToListAsync();
//            return StatusCode(StatusCodes.Status200OK, new { value = listClients });
//        }



//        // Método para crear un cliente
//        [HttpPost]
//        [Route("CreateClient")]
//        public async Task<IActionResult> CreateClients(ClientDto client)
//        {
//            var modelClient = new Client
//            {
//                NameClient = client.NameClient,
//                EmailClient = client.EmailClient,
//                PhoneClient = client.PhoneClient,
//                AddressClient = client.AddressClient,
//                LastModification = client.LastModification
//            };

//            await _dbContext.Clients.AddAsync(modelClient);
//            await _dbContext.SaveChangesAsync();

//            if (modelClient.IdClient != 0)
//                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
//            else
//                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
//        }



//        // Método para eliminar un cliente por id
//        [HttpDelete]
//        [Route("DeleteClient")]
//        public async Task<IActionResult> DeleteClient(int id)
//        {
//            var existingClient = await _dbContext.Clients.FindAsync(id);
//            if (existingClient == null)
//            {
//                return NotFound(new { isSuccess = false, message = "User not found" });
//            }

//            _dbContext.Clients.Remove(existingClient);
//            await _dbContext.SaveChangesAsync();

//            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
//        }




//    }
//}








using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tokesServiceWebApi.DataAccess;
using tokesServiceWebApi.DTOs;
using tokesServiceWebApi.Models;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace tokesServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly TokesBdContext _dbContext;

        public ClientsController(TokesBdContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        // Método para obtener todos los clientes con sus vehículos
        [HttpGet]
        [Route("ListClients")]
        public async Task<IActionResult> ListClients()
        {
            // Configuración para manejar referencias circulares y formatear el JSON
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true, // Formato indentado para mayor legibilidad
                MaxDepth = 100 // Ajusta según sea necesario para la profundidad máxima permitida
            };

            var listClients = await _dbContext.Clients
                                              .Include(c => c.Vehicles) // Incluye los vehículos relacionados
                                              .ToListAsync();

            // Serializa el resultado con las opciones configuradas
            var serializedResult = JsonSerializer.Serialize(new { value = listClients }, options);

            return Ok(serializedResult);
        }





        // Método para crear un cliente
        [HttpPost]
        [Route("CreateClient")]
        public async Task<IActionResult> CreateClients(ClientDto client)
        {
            // Validar la entrada del cliente
            if (client == null)
            {
                return BadRequest(new { isSuccess = false, message = "Client data is required" });
            }

            var modelClient = new Client
            {
                NameClient = client.NameClient,
                EmailClient = client.EmailClient,
                PhoneClient = client.PhoneClient,
                AddressClient = client.AddressClient,
                LastModification = client.LastModification
            };

            try
            {
                await _dbContext.Clients.AddAsync(modelClient);
                await _dbContext.SaveChangesAsync();

                return Ok(new { isSuccess = true });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = false, message = ex.Message });
            }
        }

        // Método para eliminar un cliente por id
        [HttpDelete]
        [Route("DeleteClient")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var existingClient = await _dbContext.Clients.FindAsync(id);
            if (existingClient == null)
            {
                return NotFound(new { isSuccess = false, message = "Client not found" });
            }

            _dbContext.Clients.Remove(existingClient);
            await _dbContext.SaveChangesAsync();

            return Ok(new { isSuccess = true });
        }

        // Método para editar un cliente
        [HttpPut]
        [Route("EditClient")]
        public async Task<IActionResult> EditClient(ClientEditDto client)
        {
            if (client == null || client.IdClient == 0)
            {
                return BadRequest(new { isSuccess = false, message = "Valid client data is required" });
            }

            var existingClient = await _dbContext.Clients.FindAsync(client.IdClient);
            if (existingClient == null)
            {
                return NotFound(new { isSuccess = false, message = "Client not found" });
            }

            // Actualizamos todos los campos
            existingClient.NameClient = client.NameClient;
            existingClient.EmailClient = client.EmailClient;
            existingClient.PhoneClient = client.PhoneClient;
            existingClient.AddressClient = client.AddressClient;
            existingClient.LastModification = client.LastModification;

            try
            {
                _dbContext.Clients.Update(existingClient);
                await _dbContext.SaveChangesAsync();

                return Ok(new { isSuccess = true });
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = false, message = ex.Message });
            }
        }
    }
}
