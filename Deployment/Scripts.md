# Scripts


## SQL & API

### Scaffolding models into API Code (Database-first)
```powershell
dotnet ef dbcontext scaffold "Data Source=crudtososerverdb.database.windows.net;Initial Catalog=bikesdb;User ID=projectadmin;Password=Something_!?; Encrypt=True;" Microsoft.EntityFrameworkCore.SqlServer -o Model
```

