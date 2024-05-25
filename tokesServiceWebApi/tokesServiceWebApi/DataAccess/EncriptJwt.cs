//IMPORTACIONAES NECESARIAS PARA LAS REFERENCIAS
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using tokesServiceWebApi.Models;



namespace tokesServiceWebApi.DataAccess
{
    public class EncriptJwt
    {
        private readonly IConfiguration _configuration;
        //Creamos un constructor para recibir la instancia de nuestra clase y configuracion, basicamente para acceder al app.settingJson
        public EncriptJwt(IConfiguration configuration)
        {
            //Tomara el valor que esta recibiendo por el para metro de arriba el del constructor

            _configuration = configuration;

        }

        //CREAMOS UN METODO QUE ENCRIPTARA LA CONTRASEÑA
        public String EcriptPassword(String password)

        {
            using (SHA256 sha256 = SHA256.Create())
            {
                //Esto nos permitira convertir nuestro texto de password en bytes
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Iteramos cada elemento que encontramos en el array de bytes y lo vamos a convertir en un string 
                //Convertir de array de bytes a un string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));
                }

                //Retornamos nuestro texto ya encriptado
                return builder.ToString();
            }

        }

        //AHORA GENERAR NUESTRO JsonWebTokens
        public String GenerateJwt(User modeel)
        {
            //Crea la informacion del usuario para el token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modeel.IdUser.ToString()),
                new Claim(ClaimTypes.Email, modeel.Email!),
            };

            //CREAMOS NUESTRAS LLAVES DE SEGURIDAD PARA GENERAR NUESTRO JWT para algunas credenciales
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            //CREAMOS EL DETALLE DEL TOKEN

            var jwtConfig = new JwtSecurityToken(
            //Cada uno de los parametros que necesite

            claims: userClaims,
            expires: DateTime.UtcNow.AddMinutes(40), // Le diremos cuanto tiempo debe durar ese token en mi caso le puse 40Minutos
            signingCredentials: credentials//Enviaremos las credenciales que hemos configurado en la var credentials
            );

            //Vamos a retornar el token 
            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
