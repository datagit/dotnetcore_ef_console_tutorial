using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef
{
  [Table("Category")]
  public class Category
  {
    [Key]
    public int CategoryId { set; get; }

    [StringLength(100)]
    public string Name { set; get; }
    [Column(TypeName = "ntext")]
    public string Description { set; get; }

    // Collection Navigation
    // no se khong tao field trong table
    // virtual -> support Lazy load by package: Microsoft.EntityFrameworkCore.Proxies
    public virtual List<Product> Products { get; set; }
  }
}