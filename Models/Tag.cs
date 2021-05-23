using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationExamples
{
  [Table("Tag")]
  public class Tag
  {
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(50)]
    public string name {get; set;}

    // Collection Navigation
    // no se khong tao field trong table
    public List<Article> Products { get; set; }

  }
}