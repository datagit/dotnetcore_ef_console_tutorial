using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MigrationExamples
{
  public class ArticleTag
  {
    [Key]
    public int Id { get; set; } // -> PK

    public int? TagId { get; set; } // -> FK Tag
    [ForeignKey("TagId")]
    public Tag Tag { get; set; }

    public int? ArticleId { get; set; } // -> FK Article
    [ForeignKey("ArticleId")]
    public Article Article { get; set; }
  }
}