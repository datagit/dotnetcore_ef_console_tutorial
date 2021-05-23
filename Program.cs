using System;
using System.Threading.Tasks;
using MigrationExamples;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace dotnetcore_ef_console_tutorial
{
  class Program
  {
    public static void CreateDatabase()
    {
      using var dbcontext = new WebContext();
      String databasename = dbcontext.Database.GetDbConnection().Database;// mydata

      Console.WriteLine("Tạo " + databasename);

      bool result = dbcontext.Database.EnsureCreated();
      string resultstring = result ? "tạo  thành  công" : "đã có trước đó";
      Console.WriteLine($"CSDL {databasename} : {resultstring}");
    }
    public static void DeleteDatabase()
    {

      using (var context = new WebContext())
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
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      // CreateDatabase();
      // DeleteDatabase();
    }
  }
}
