namespace tokesServiceWebApi.DTOs
{
    public class LoginDto
    {

        //AGREGAMOS LAS CREDENCIALES DE LAS CUAL NUESTRO USUARIO PODRA ACCEDER A NUESTRA API  QUE SON CORREO Y PASWORD
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
