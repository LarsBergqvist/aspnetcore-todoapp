# aspnetcore-todoapp ![Build](https://github.com/larsbergqvist/aspnetcore-todoapp/actions/workflows/dotnet.yml/badge.svg)
Uses SqlLite, EntityFramework and AspNetCore.Identity

# Install dotnet ef
```
dotnet tool install --global dotnet-ef
```
# Create the database for todo lists
Modify TodosConnection in Infrastructure\appsettings.json (or use user-secret)
```
cd Infrastructure
dotnet ef migrations add InitialCreate
dotnet ef database update
```
# Create database for application users
```
cd RazorPagesApp
dotnet ef migrations add InitialCreate --context ApplicationDbContext
dotnet ef database update --context ApplicationDbContext
```
