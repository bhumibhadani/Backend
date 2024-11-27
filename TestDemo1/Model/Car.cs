namespace TestDemo1.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string? Brand { get; set; } // Audi, Jaguar, etc.
        public string? Class { get; set; } // A-Class, B-Class, etc.
        public string? ModelName { get; set; }
        public string? ModelCode { get; set; } // Alphanumeric (max 10)
        public string? Description { get; set; }
        public string? Features { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public bool Active { get; set; }
        public int SortOrder { get; set; }
        public string? Images { get; set; } // Paths to uploaded images
        public string? FileName { get; set; } 
    }
}
