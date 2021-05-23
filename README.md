## Cài đặt MS SQL Server trên Docker
```
docker-compose up -d
```
container name: sqlserver-xtlab

port: 1433

user: sa

password: Password123

## Phục hồi dữ liệu mẫu
```
docker exec sqlserver-xtlab /var/opt/mssql/backup/restore.sh
```
Tên CSDL: xtlab

Cấu trúc dữ liệu mẫu:
https://xuanthulab.net/chay-sql-online-cong-cu-hoc-cau-lenh-sql.html

ME
```bash
mkdir dotnetcore_ef_console_tutorial
cd dotnetcore_ef_console_tutorial
dotnet new console -o ./

#guide: https://xuanthulab.net/ef-core-gioi-thieu-entity-framework-va-cach-su-dung-phan-co-ban-voi-c-csharp.html

# Để sử dụng EF Core hãy thêm những package cần thiết vào, chạy các lệnh sau:
dotnet add package System.Data.SqlClient
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Logging
dotnet add package Microsoft.Extensions.Logging.Console

# structure folders
tree -I obj                                                                                                             git:(master|…6 
.
├── Program.cs
├── bk
│   ├── restore.sh
│   ├── xtlab.bak
│   └── xtlab.en.bak
├── docker-compose.yml
├── dotnetcore_ef_console_tutorial.csproj
└── readme.md
# run docker
docker compose up -d

# access container:
docker exec -it sqlserver-xtlab bash

# install packages
# guide: https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-dotnet-cli
dotnet restore

# make DbContext
touch Models/ProductDbContext.cs
touch Models/Product.cs

# EF Logger hiện thị SQL Query trên terminal
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Logging
dotnet add package Microsoft.Extensions.Logging.Console
```

```bash
# Migration
# migration: (code C#) -> (database)
# database=webdb

# make new a migration with name
# dotnet ef migrations add [migration-name]
dotnet ef migrations add V0

# list all migration
dotnet ef migrations list
# 20210523054431_V0 (Pending) => not updated to DB

dotnet ef migrations add V1
dotnet ef migrations remove

# update [migration-name] -> DB
dotnet ef database update [migration-name]

# drop database
dotnet ef database drop -f
```