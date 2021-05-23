using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnetcore_ef_console_tutorial.Entities
{
    [Table("Product")]
    [Index(nameof(CategoryId), Name = "IX_Product_CategoryId")]
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string TenSanPham { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
    }
}
