using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationExamples
{
  [Table("Article")]
  public class Article
  {
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(50)]
    public string Name {get; set;}

    [StringLength(50)]
    public string description {get; set;}

    // make Foreign key on table Product
    // Foreign Key
    // Reference Navigation
    public int? TagId { get; set; } // TagId can be null

    [ForeignKey("TagId")]
    public Tag Tag { get; set; } // FK(Article.TagId) -> PK(Tag.Id)
  }
}