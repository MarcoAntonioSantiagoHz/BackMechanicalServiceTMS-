using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//Importamos elacceso a datos de nuestras clases
using tokesServiceWebApi.Models;
using tokesServiceWebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using tokesServiceWebApi.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace tokesServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AcessController : ControllerBase
    {



        private readonly TokesBdContext _dbContext;
        private readonly EncriptJwt _encript;
        public AcessController(TokesBdContext dbContext, EncriptJwt encript)
        {
            _dbContext = dbContext;
            _encript = encript;
        }


        //CREAMOS EL ENDPOT / SOLICUTUD PARA PODER REGISTRARNOS Y LOGGEARNOS


        //PRIMERO EL POST REGISTRAR USUARIO
        [HttpPost]
        [Route("RegistrUser")]
        public async Task<IActionResult> Register(UserDto user)
        {
            //
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

            //Contexto de nuestra BD 
            await _dbContext.Users.AddAsync(modelUser);//Agrega de manera asyncrona el modelo que hemos creado modelUser
            await _dbContext.SaveChangesAsync(); //Que pueda guardar los cambios

            //VALIDAMOS SI NUESTRO USUARIO DEL MODELO A SIDO CREADO
            if (modelUser.IdUser != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true }); //Si es correcta regresara un TRUE 200OK
            else//EN CASO DE QUE NO SE CREE EL USUARIO MOSTRARA UN FALSO NO SE PUDO CREAR
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }




        //AQUI VAMOS A LOGEARNOS
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            var usuarioEncontrado = await _dbContext.Users
                                                    .Where(u =>
                                                        u.Email == user.Email &&
                                                        u.Password == _encript.EcriptPassword(user.Password)
                                                      ).FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _encript.GenerateJwt(usuarioEncontrado) });
        }
    }

}

