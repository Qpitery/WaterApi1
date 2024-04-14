namespace WaterApi1.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string? TypeProduct { get; set; }

        public string? BottleVolume { get; set; }

        public int? Lot { get; set; }

        public string? Partnership { get; set; }

        public string? Name { get; set; }

        public string? Contact { get; set; }

        public string? Email { get; set; }

        public string? City { get; set; }

        public byte[]? Logo { get; set; }

        public int UserId { get; set; }
    }
}
