using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef
{
  [Table("Product")]
  public class Product
  {
    [Key]
    public int ProductId { get; set; }

    [Required]
    [StringLength(50)]
    [Column("TenSanPham", TypeName = "ntext")]
    public string Name { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    // make Foreign key on table Product
    // Foreign Key
    // Reference Navigation
    public int? CategoryId { get; set; } // CategoryId can be null
    // CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]) ON DELETE NO ACTION
    // public Category Category { get; set; }
    [ForeignKey("CategoryId")]

    // virtual -> support Lazy load by package: Microsoft.EntityFrameworkCore.Proxies
    public virtual Category Category { get; set; } // FK(Product.CategoryId) -> PK(Category.CategoryId)

    // public int CategoryId { get; set; } // CategoryId can be not null
    // CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]) ON DELETE NO ACTION

    public void PrintInfo() => Console.WriteLine($"id-{ProductId} - {Name} - {Price}, - {CategoryId}");
  }
  /*
    https://www.devart.com/dotconnect/mysql/docs/datatypemapping.html
    Table("TableName")
    [KEY] -> Primary Key (PK)
    [Required] -> not null
    [StringLength(50)] -> string -> nvarchar
    [Column("TenSanPham", TypeName = "ntext")]
    [Column(TypeName = "money")]
    [ForeignKey("CategoryId")]

    Reference Navigation -> Foreign Key(1 -> many)
    Collection Navigation -> (khong tao ra Foreign Key)

    InverseProperty
  */
}