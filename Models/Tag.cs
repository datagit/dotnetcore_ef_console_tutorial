using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef
{
  [Table("Tag")]
  public class Tag
  {
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(50)]
    public string name {get; set;}
    
  }
}