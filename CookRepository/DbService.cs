using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookData;
using Dapper;

namespace CookRepository
{
    public class DbService
    {
        private const string DatabaseConnectionString =
                "Server=78624758-8105-4602-a6ab-a7f5004d5221.sqlserver.sequelizer.com;Database=db7862475881054602a6aba7f5004d5221;User ID=qjhmazuiieodsobo;Password=MyQYq4CaamriviQPATSvVNbXdft32yoeD4KocMBT3pCXKM7jB3P7XPV8x2VjJJhJ;";

        public Cooker GetCooker(int id)
        {
            using (IDbConnection db = new SqlConnection(DatabaseConnectionString))
            {
                var cookerQuery = db.Query($"SELECT * FROM Cookers WHERE id={id}");
                var cooker = cookerQuery.Select(x => new Cooker
                {
                    Id = x.id,
                    Name = x.name
                }).First();

                var dishesQuery = db.Query($"SELECT * FROM Dishes WHERE cooker_id={id}");

                cooker.Dishes = dishesQuery.Select(x => new Dish
                {
                    Id = x.id,
                    Name = x.name
                }).ToList();

                return cooker;
            }
        }
    }
}
