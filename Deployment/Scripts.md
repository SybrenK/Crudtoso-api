# Scripts


## SQL & API

### Scaffolding models into API Code (Database-first)
```powershell
dotnet ef dbcontext scaffold "Data Source=crudtososerverdb.database.windows.net;Initial Catalog=bikesdb;User ID=projectadmin;Password=Something_!?; Encrypt=True;" Microsoft.EntityFrameworkCore.SqlServer -o Model
```

## CDN
```bash
PS /home/said_elkacimi> az provider show --namespace Microsoft.CDN --query "registrationState"                                                                                             
"NotRegistered"
PS /home/said_elkacimi> az provider register --namespace Microsoft.CDN                                                                                                                     
Registering is still on-going. You can monitor using 'az provider show -n Microsoft.CDN'
```
