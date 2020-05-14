using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace frontend_csharp.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<ProductData> Products { get; set; }
    }
}