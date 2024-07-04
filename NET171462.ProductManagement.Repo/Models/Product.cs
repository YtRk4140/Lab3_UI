using SE171462.ProductManagement.Repo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NET171462.ProductManagement.Repo.Models
{
    public partial class Product : ISoftDelete
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; } = null!;

        public int CategoryId { get; set; }

        public short UnitsInStock { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public virtual Category Category { get; set; }

        public bool IsDeleted { get; set; }
        //public DateTimeOffset? DeletedAt { get; set; }
    }
}
