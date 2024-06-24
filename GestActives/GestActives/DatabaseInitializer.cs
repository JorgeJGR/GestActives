using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Data.SqlClient;

namespace GestActives
{
    public static class DatabaseInitializer
    {
        public static void CreateDatabase()
        {
            using (var context = new GestActivesContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}