using System.ComponentModel.DataAnnotations;

namespace frontend_csharp.Models
{
    public class ProductData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public UserData User { get; set; }
    }
}