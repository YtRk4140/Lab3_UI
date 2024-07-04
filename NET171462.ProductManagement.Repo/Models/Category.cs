using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NET171462.ProductManagement.Repo.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(40)]
        public string CategoryName { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
