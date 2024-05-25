namespace tokesServiceWebApi.DTOs
{
    public class VehicleEdit
    {
        public int IdVehicle { get; set; }

        public string? CarType { get; set; }

        public string? Mark { get; set; }

        public DateTime? DateEntry { get; set; }

        public string? LicensePlate { get; set; }

        public bool? Status { get; set; }

        public string? Observations { get; set; }

        public string? LastModification { get; set; }

        public string? TechnicalComments { get; set; }

        public string? MechanicalRevisionBy { get; set; }

    }
}
