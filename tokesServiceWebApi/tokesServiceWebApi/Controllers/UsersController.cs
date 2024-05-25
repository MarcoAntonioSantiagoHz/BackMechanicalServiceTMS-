//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using tokesServiceWebApi.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authentication.JwtBearer;


//namespace tokesServiceWebApi.Controllers
//{
//    public class UsersController : Controller
//    {
//        private readonly TokesBdContext _context;

//        public UsersController(TokesBdContext context)
//        {
//            _context = context;
//        }

//        // GET: Users
//        [HttpGet]
//        [Route("GetAll/users")]
//        public async Task<IActionResult> Index()
//        {
//            var users = await _context.Users.ToListAsync();
//            return Ok(users);
//        }

//    }



//}
//}





using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tokesServiceWebApi.Models;
using tokesServiceWebApi.DataAccess;
using tokesServiceWebApi.DTOs;


namespace tokesServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TokesBdContext _dbContext;
        private readonly EncriptJwt _encript;

        public UsersController(TokesBdContext dbContext, EncriptJwt encript)
        {
            _dbContext = dbContext;
            _encript = encript;
        }

        // Método para obtener todos los usuarios creados
        [HttpGet]
        [Route("ListAllUsers")]
        public async Task<IActionResult> ListUsers()
        {
            var ListUsers = await _dbContext.Users.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = ListUsers });
        }

        // Método para crear un usuario
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUsers(UserDto user)
        {
            var modelUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Status = user.Status,
                Phone = user.Phone,
                RoleUser = user.RoleUser,
                Address = user.Address,
                Email = user.Email,
                Password = _encript.EcriptPassword(user.Password)
            };

            await _dbContext.Users.AddAsync(modelUser);
            await _dbContext.SaveChangesAsync();

            if (modelUser.IdUser != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }



        // Método para editar un usuario
        [HttpPut]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.IdUser);
            if (existingUser == null)
            {
                return NotFound(new { isSuccess = false, message = "User not found" });
            }

            // Actualizamos todos los campos
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Status = user.Status;
            existingUser.Phone = user.Phone;
            existingUser.RoleUser = user.RoleUser;
            existingUser.Address = user.Address;
            existingUser.Email = user.Email;

            // Actualizamos la contraseña encriptada
            existingUser.Password = _encript.EcriptPassword(user.Password);

            _dbContext.Users.Update(existingUser);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
        }


        // Método para eliminar un usuario

        // Método para eliminar un usuario
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _dbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound(new { isSuccess = false, message = "User not found" });
            }

            _dbContext.Users.Remove(existingUser);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
        }

    }
}
