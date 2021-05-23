using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef
{
  [Table("Article")]
  public class Article
  {
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(50)]
    public string title {get; set;}

    [StringLength(50)]
    public string description {get; set;}
  }
}