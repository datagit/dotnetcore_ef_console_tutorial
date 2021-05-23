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
    public static async Task CreateDatabase()
    {
      using (var dbcontext = new ProductDbContext())
      {
        String databasename = dbcontext.Database.GetDbConnection().Database;// mydata

        Console.WriteLine("Tạo " + databasename);

        bool result = await dbcontext.Database.EnsureCreatedAsync();
        string resultstring = result ? "tạo  thành  công" : "đã có trước đó";
        Console.WriteLine($"CSDL {databasename} : {resultstring}");
      }
    }
    public static async Task DeleteDatabase()
    {

      using (var context = new ProductDbContext())
      {
        String databasename = context.Database.GetDbConnection().Database;
        Console.Write($"Có chắc chắn xóa {databasename} (y) ? ");
        string input = Console.ReadLine();

        // Hỏi lại cho chắc
        if (input.ToLower() == "y")
        {
          bool deleted = await context.Database.EnsureDeletedAsync();
          string deletionInfo = deleted ? "đã xóa" : "không xóa được";
          Console.WriteLine($"{databasename} {deletionInfo}");
        }
      }

    }
    // Thực hiện chèn hai dòng dữ liệu vào bảng Product
    // Dùng AddAsync trong DbSet và trong DbContext
    public static async Task InsertProduct()
    {
      using (var context = new ProductDbContext())
      {
        // Thêm sản phẩm 1
        await context.products.AddAsync(new Product
        {
          ProductName = "Sản phẩm 1",
          Provider = "Công ty 1"
        });
        // Thêm sản phẩm 2
        await context.AddAsync(new Product()
        {
          ProductName = "Sản phẩm 2",
          Provider = "Công ty 1"
        });

        // Thực hiện cập nhật thay đổi trong DbContext lên Server
        int rows = await context.SaveChangesAsync();
        Console.WriteLine($"Đã lưu {rows} sản phẩm");

      }
    }
    public static async Task InsertMany()
    {
      using (var context = new ProductDbContext())
      {
        var p1 = new Product() { ProductName = "Sản phẩm 3", Provider = "CTY A" };
        var p2 = new Product() { ProductName = "Sản phẩm 4", Provider = "CTY A" };
        var p3 = new Product() { ProductName = "Sản phẩm 5", Provider = "CTY B" };

        await context.AddRangeAsync(new object[] { p1, p2, p3 });

        int rows = await context.SaveChangesAsync();
        Console.WriteLine($"Đã lưu {rows} sản phẩm");
      }
    }
    public static async Task ReadProducts()
    {
      using var context = new ProductDbContext();
      // context.SetLogging();
      // Lấy danh sách các sản phẩm trong bảng
      var products = await context.products.ToListAsync();

      Console.WriteLine("Tất cả sản phẩm");
      foreach (var product in products)
      {
        Console.WriteLine($"{product.ProductId,2} {product.ProductName,10} - {product.Provider}");
      }
      Console.WriteLine();
      // Console.WriteLine();

      // Dùng LINQ để truy vấn đến DbSet products (bảng product)
      // Lấy các sản phẩm cung cấp bởi CTY A
      // products = await (from p in context.products
      //                   where  p.Provider == "CTY A"
      //                   select p
      //                  )
      //                 .ToListAsync();

      // Nếu không dùng bất đồng bộ chỗ này, có thể dùng
      // var products2 = from p in context.products where (p.Provider == "CTY A") select p;

      // Console.WriteLine("Sản phẩm CTY A");
      // foreach (var product in products2)
      // {
      //   Console.WriteLine($"{product.ProductId,2} {product.ProductName,10} - {product.Provider}");
      // }
    }
    // Đổi tên sản phẩm có ProductID thành  tên mới
    public static async Task RenameProduct(int id, string newName)
    {
      using (var context = new ProductDbContext())
      {

        // Lấy  Product có  ID sản phẩm  chỉ  ra
        var product = await (from p in context.products where (p.ProductId == id) select p).FirstOrDefaultAsync();

        // Đổi tên và cập nhật
        if (product != null)
        {
          // product -> DbContext
          // DbContext theo gioi doi tuong product(nan biet su tahy doi cua doi tuong product)
          EntityEntry<Product> entry = context.Entry<Product>(product);
          // gio chung ta khong the dung context.SaveChangesAsync() de update object
          //entry.State = EntityState.Detached;

          product.ProductName = newName;
          Console.WriteLine($"ID={product.ProductId} có tên mới = {product.ProductName}");
          await context.SaveChangesAsync();  //Thi hành cập nhật
        }
      }
    }

    // Xóa sản phẩm có ProductID = id
    public static async Task DeleteProduct(int id)
    {
      using (var context = new ProductDbContext())
      {
        // context.SetLogging();
        var product = await (from p in context.products where (p.ProductId == id) select p).FirstOrDefaultAsync();

        if (product != null)
        {
          context.Remove(product);
          Console.WriteLine($"Xóa {product.ProductId}");
          await context.SaveChangesAsync();
        }
      }
    }

    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      //   await DeleteDatabase();
      //   await CreateDatabase();
      //   await InsertProduct();
      //   await InsertMany();
      // await ReadProducts();
      await RenameProduct(2, "Cong ty 2#");
      // await ReadProducts();
      // await DeleteProduct(1);

    }
  }
}
