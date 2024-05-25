namespace tokesServiceWebApi.DTOs
{
    public class UserDto
    {

        //PROPIEDADES DE LAS CUAL NOS PERMITIRA CREAR UN NUEVO USUARIO PARA EL METODO POST
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? RoleUser { get; set; }

        public bool? Status { get; set; }

    }
}
