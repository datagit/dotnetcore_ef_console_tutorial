using System;
using System.Threading.Tasks;
using ef;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace dotnetcore_ef_console_tutorial
{
  class Program
  {
    // Tạo Database mydata (tên mydata từ thông tin kết nối)
    // Gồm tất cả các bảng định nghĩa bởi các thuộc tính kiểu DbSet
    public static void CreateDatabase()
    {
      using var dbcontext = new ShopContext();
      String databasename = dbcontext.Database.GetDbConnection().Database;// mydata

      Console.WriteLine("Tạo " + databasename);

      bool result = dbcontext.Database.EnsureCreated();
      string resultstring = result ? "tạo  thành  công" : "đã có trước đó";
      Console.WriteLine($"CSDL {databasename} : {resultstring}");
    }
    public static void DeleteDatabase()
    {

      using (var context = new ShopContext())
      {
        String databasename = context.Database.GetDbConnection().Database;
        Console.Write($"Có chắc chắn xóa {databasename} (y) ? ");
        // string input = Console.ReadLine();

        // Hỏi lại cho chắc
        // if (input.ToLower() == "y")
        {
          bool deleted = context.Database.EnsureDeleted();
          string deletionInfo = deleted ? "đã xóa" : "không xóa được";
          Console.WriteLine($"{databasename} {deletionInfo}");
        }
      }
    }

    public static void InsertData()
    {

      using (var context = new ShopContext())
      {
        // Category c1 = new Category() {
        //   Name = "cate2",
        //   Description = "desc2",
        // };
        // Category c2 = new Category() {
        //   Name = "cate3",
        //   Description = "desc3",
        // };

        // context.categories.Add(c1);
        // context.categories.Add(c2);

        var c1 = (from c in context.categories where c.CategoryId == 1 select c).FirstOrDefault<Category>();
        var c2 = (from c in context.categories where c.CategoryId == 2 select c).FirstOrDefault<Category>();

        context.Add(new Product()
        {
          Name = "Phone",
          Price = 100,
          CategoryId = 1
        });
        context.Add(new Product()
        {
          Name = "SAmsumg",
          Price = 200,
          Category = c1
        });
        context.Add(new Product()
        {
          Name = "quan ao",
          Price = 300,
          Category = c1
        });
        context.Add(new Product()
        {
          Name = "giay dep",
          Price = 400,
          Category = c2
        });
        context.Add(new Product()
        {
          Name = "laptop",
          Price = 500,
          Category = c2
        });

        context.SaveChanges();
      }
    }

    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      // DeleteDatabase();
      // CreateDatabase();
      // InsertData();

      // using var dbcontext = new ShopContext();
      // var product = (from p in dbcontext.products where p.ProductId == 3 select p).FirstOrDefault();

      // //manual load Category from Entry
      // var e = dbcontext.Entry(product);
      // e.Reference(p => p.Category).Load();

      // product.PrintInfo();
      // if (product.Category != null) {
      //   Console.WriteLine("HAS: e.Reference(p => p.Category).Load();");
      //   Console.WriteLine($"Category info {product.Category.Name}, {product.Category.Description}");
      // } else {
      //   Console.WriteLine("Category == null");
      // }

      using var dbcontext = new ShopContext();
      var category = (from c in dbcontext.categories where c.CategoryId == 2 select c).FirstOrDefault();

      // //manual load Products from Entry
      var e = dbcontext.Entry(category);
      e.Collection(c => c.Products).Load();

      if (category.Products != null)
      {
        Console.WriteLine("e.Collection(c => c.Products).Load();");
        Console.WriteLine("Product is not null");
        category.Products.ForEach(p => p.PrintInfo());
      }
      else
      {
        Console.WriteLine("Product is null");
      }

    }
  }
}
