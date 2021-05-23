using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MigrationExamples
{
  public class WebContext : DbContext
  {
    // Tạo ILoggerFactory
    public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
    {
      // Các tên category tham khảo đối với EF Logger:
      // DbLoggerCategory.Database.Command, DbLoggerCategory.Database.Connection, DbLoggerCategory.Database.Transaction, DbLoggerCategory.Infrastructure, DbLoggerCategory.Migration, DbLoggerCategory.Model, DbLoggerCategory.Query, DbLoggerCategory.Scaffolding, DbLoggerCategory.Update
      builder
             //  .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Warning)
             .AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information)
             .AddConsole();
    }
    );


    // Thuộc tính products kiểu DbSet<Product> cho biết CSDL có bảng mà
    // thông tin về bảng dữ liệu biểu diễn bởi model Product
    public DbSet<Article> articles { set; get; }
    public DbSet<Tag> tags { set; get; }

    public DbSet<ArticleTag> ArticleTags { get; set; }

    // Chuỗi kết nối tới CSDL (MS SQL Server)
    private const string connectionString = @"
                Data Source=localhost,1433;
                Initial Catalog=webdb;
                User ID=SA;Password=Password123";

    // Phương thức OnConfiguring gọi mỗi khi một đối tượng DbContext được tạo
    // Nạp chồng nó để thiết lập các cấu hình, như thiết lập chuỗi kết nối
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      optionsBuilder
        .UseLoggerFactory(loggerFactory)  // - Thiết lập sử Logger
        .UseSqlServer(connectionString);
    }

    // Phương thức này thi hành khi EnsureCreatedAsync chạy, tại đây gọi các Fluent API mong muốn
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Các Fluent API
      modelBuilder.Entity<ArticleTag>(entity =>
      {
        entity.HasIndex(articleTag => new { articleTag.ArticleId, articleTag.TagId })     // Đánh chỉ mục 2 fields (ArticleId, TagId)
          .IsUnique(true);               // Unique
      });


    }
  }
}