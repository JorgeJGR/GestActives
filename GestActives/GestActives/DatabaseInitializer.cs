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
                //CreateTriggers(context);
            }
        }

        private static void CreateTriggers(GestActivesContext context)
        {
            var connection = context.Database.GetDbConnection();
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
        IF OBJECT_ID('SetCompanyDefaults', 'TR') IS NOT NULL 
            DROP TRIGGER SetCompanyDefaults;
    ";
                command.ExecuteNonQuery();

                command.CommandText = @"
        CREATE TRIGGER SetCompanyDefaults
        ON Company
        AFTER INSERT
        AS
        BEGIN
            SET NOCOUNT ON;

            UPDATE Company
            SET External = CASE WHEN inserted.DesCompany = 'Monbake' THEN 0 ELSE 1 END,
                IdCompany = CASE WHEN inserted.DesCompany = 'Monbake' THEN 1 ELSE Company.IdCompany END
            FROM inserted
            WHERE Company.IdCompany = inserted.IdCompany;
        END;
    ";
                command.ExecuteNonQuery();
            }
        }
    }
}


