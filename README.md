# aspnetcore-todoapp ![Build](https://github.com/larsbergqvist/aspnetcore-todoapp/actions/workflows/dotnet.yml/badge.svg)

# Create the database for todo lists
Modify TodosConnection in Infrastructure\appsettings.json (or use user-secret)
```
cd Infrastructure
dotnet ef database update
```
# Create database for application users
```
cd RazorPagesApp
dotnet ef database update
```
