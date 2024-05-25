using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tokesServiceWebApi.DataAccess;
using tokesServiceWebApi.DTOs;
using tokesServiceWebApi.Models;

namespace tokesServiceWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehciclesControllers : Controller
    {
        private readonly TokesBdContext _dbContext;
        private readonly EncriptJwt _encript;

        public VehciclesControllers(TokesBdContext dbContext, EncriptJwt encript)
        {
            _dbContext = dbContext;

            _encript = encript;
        }

        // Método para obtener todos los clientes
        [HttpGet]
        [Route("ListVehicles")]
        public async Task<IActionResult> ListVehicles()
        {
            var listVehicles = await _dbContext.Vehicles.ToListAsync();
            return Ok(new { value = listVehicles });
        }

        // Método para crear un vehiculo
        [HttpPost]
        [Route("CreateVehicle")]
        public async Task<IActionResult> CreateVehicle(VehicleDto vehicleDto)
        {
            var modelVehicle = new Vehicle
            {
                CarType = vehicleDto.CarType,
                Mark = vehicleDto.Mark,
                DateEntry = vehicleDto.DateEntry,
                LicensePlate = vehicleDto.LicensePlate,
                Status = vehicleDto.Status,


                Observations = vehicleDto.Observations,

                LastModification = vehicleDto.LastModification,

                TechnicalComments = vehicleDto.TechnicalComments,

                MechanicalRevisionBy = vehicleDto.MechanicalRevisionBy,

                IdClient = vehicleDto.IdClient
            };

            await _dbContext.Vehicles.AddAsync(modelVehicle);
            await _dbContext.SaveChangesAsync();

            if (modelVehicle.IdVehicle != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        // Método para editar un vehiculo
        [HttpPut]
        [Route("EditVehicle")]
        public async Task<IActionResult> EditVehicle(VehicleEdit vehicle)
        {
            var existingVehicle = await _dbContext.Vehicles.FindAsync(vehicle.IdVehicle);
            if (existingVehicle == null)
            {
                return NotFound(new { isSuccess = false, message = "Vehicle not found" });
            }

            // Actualizamos todos los campos
            existingVehicle.IdVehicle = vehicle.IdVehicle;
            existingVehicle.CarType = vehicle.CarType;
            existingVehicle.Mark = vehicle.Mark;
            existingVehicle.DateEntry = vehicle.DateEntry;
            existingVehicle.LicensePlate = vehicle.LicensePlate;
            existingVehicle.Status = vehicle.Status;
            existingVehicle.Observations = vehicle.Observations;
            existingVehicle.LastModification = vehicle.LastModification;
            existingVehicle.TechnicalComments = vehicle.TechnicalComments;
            existingVehicle.MechanicalRevisionBy = vehicle.MechanicalRevisionBy;
            //existingVehicle.IdClient = vehicle.IdClient;



            _dbContext.Vehicles.Update(existingVehicle);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
        }



        // Método para eliminar un VEHICULO
        [HttpDelete]
        [Route("DeleteVehicle")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var existingVehicle = await _dbContext.Vehicles.FindAsync(id);
            if (existingVehicle == null)
            {
                return NotFound(new { isSuccess = false, message = "vehicle not found" });
            }

            _dbContext.Vehicles.Remove(existingVehicle);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
        }
    }
}
