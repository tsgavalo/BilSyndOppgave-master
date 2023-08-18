namespace BilSynd
{
    internal class Car
    {
        //Properties
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? LicensePlate { get; set; }
        public DateTime DateofRegistration { get; set; }
        public DateTime LastInspection { get; set; }
        public int ModelYear { get; set; } = 2000;
        public float EngineSize { get; set; } = 2f;
        public Person? Owner { get; set; }

        public string? ManufacturingDefects { get; set; }
    }
}
