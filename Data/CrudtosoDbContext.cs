using Microsoft.EntityFrameworkCore;
// NOTE: In the nuget console, run the following command: 
// dotnet ef dbcontext scaffold "Data Source=crudtososerverdb.database.windows.net;Initial Catalog=bikesdb; Authentication=Active Directory Default; Encrypt=True;" Microsoft.EntityFrameworkCore.SqlServer -o Model
namespace Crudtoso_api.Data
{
    public class CrudtosoDbContext : DbContext
    {
    }
}
