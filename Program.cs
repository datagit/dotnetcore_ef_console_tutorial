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
      using (var dbcontext = new ShopContext())
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

      using (var context = new ShopContext())
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

    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      //await DeleteDatabase();
      await CreateDatabase();

    }
  }
}
