rmdir .\Cargodi.DataAccess\Migrations -Recurse
mkdir .\Cargodi.DataAccess\Migrations
dotnet ef database drop -p  .\Cargodi.DataAccess\ -s  .\Cargodi.WebApi\ -f
dotnet ef migrations add "Migga" -p  .\Cargodi.DataAccess\ -s  .\Cargodi.WebApi\ -o "Migrations"
dotnet ef database update -p  .\Cargodi.DataAccess\ -s  .\Cargodi.WebApi\