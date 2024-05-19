namespace tokesMagistralService.WebApi.Models
{
    public class ServiceVehicle
    {
        public int Id { get; set; }
        public string CarType { get; set; }
        public string Mark { get; set; }
        
        public DateTime DateOfEntry { get; set; } //Fecha de ingreso
        public string LicensePlate { get; set; } //Matricula
        public bool Status { get; set; } //Estatus Activo / Inactivo
        public string Observations { get; set; } //Obervaciones
        public string TechnicalComments { get; set; } //Comentarios tecnicos
        public int IdCliente { get; set; } // Cambiado a int para reflejar una clave foránea
        public Client Client { get; set; } // Propiedad de navegación para la relación con Cliente
    }
        //public string CarModel { get; set; } //Model
        //public string MechanicalRevisionBy { get; set; }
        //public string LastModification { get; set; }
        //public string YearEntry { get; set; }
        //public string MonthEntry { get; set; }
        //public string DayEntry { get; set; }
}
