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

    public void PrintInfo() => Console.WriteLine($"id-{ProductId} - {Name} - {Price}");
  }
  /*
    https://www.devart.com/dotconnect/mysql/docs/datatypemapping.html
    Table("TableName")
    [KEY] -> Primary Key (PK)
    [Required] -> not null
    [StringLength(50)] -> string -> nvarchar
    [Column("TenSanPham", TypeName = "ntext")]
    [Column(TypeName = "money")]
  */
}