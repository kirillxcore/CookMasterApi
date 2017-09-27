using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CookMasterApiModel;
using CookRepository.Models;
using Dapper;

namespace CookRepository
{
    public class DbService
    {
        private const string DatabaseConnectionString =
            "Server=10.195.0.121;Database=CookMaster;User ID=sa;Password=qwe123;";

        public List<DishItem> GetDishes()
        {
            using (IDbConnection db = new SqlConnection(DatabaseConnectionString))
            {
                var query = db.Query("SELECT * FROM Dishes");
                return query.Select(x => new DishItem()
                {
                    Id = x.id.ToString(),
                    Name = x.title,
                    ImageUrl = x.image,
                    IsVegan = false,
                    CategoryId = x.category_id,
                    Description = x.description
                }).ToList();

            }
        }

        public Cooker GetCooker(string login, string password)
        {
            using (IDbConnection db = new SqlConnection(DatabaseConnectionString))
            {
                var query = db.Query($"SELECT * FROM Cooks WHERE login='{login}' AND password='{password}'");
                return query.Select(x => new Cooker
                {
                    Id = x.id,
                    Name = x.name,
                    Login = x.login,
                    Password = x.password,
                    Token = x.token
                }).FirstOrDefault();

            }
        }

        public Cooker GetCookerbyId(int id)
        {
            using (IDbConnection db = new SqlConnection(DatabaseConnectionString))
            {
                var query = db.Query($"SELECT * FROM Cooks WHERE id={id}");
                return query.Select(x => new Cooker
                {
                    Id = x.id,
                    Name = x.name,
                    Login = x.login,
                    Password = x.password,
                    Token = x.token
                }).FirstOrDefault();

            }
        }

        public List<DishCategory> GetCategories()
        {
            using (IDbConnection db = new SqlConnection(DatabaseConnectionString))
            {
                var query = db.Query("SELECT * FROM Categories");
                return query.Select(x => new DishCategory
                {
                    Id = x.id,
                    Title = x.title
                }).ToList();
            }
        }

        public void CreateMenu(string formUrl, string formId, List<Tuple<int, int>> relationsBetweenIds, int cookerId, DateTime date)
        {
            using (IDbConnection db = new SqlConnection(DatabaseConnectionString))
            {
                string sql = $@"
INSERT INTO [Menus] (form_url,form_response_id,cook_id, date) VALUES ('{formUrl}','{formId}',{cookerId}, DATEFROMPARTS({date.Year},{date.Month},{date.Day}));
SELECT CAST(SCOPE_IDENTITY() as int)";

                var id = db.Query<int>(sql).Single();

                foreach (var rel in relationsBetweenIds)
                {
                    string sql2 = $@"INSERT INTO [DishForm] (menu_id,dish_id,form_id) VALUES ({id},{rel.Item1},{rel.Item2})";
                    db.Execute(sql2);
                }
            }
        }

        public List<Menu> GetMenus(int cookerId, DateTime day)
        {
            using (IDbConnection db = new SqlConnection(DatabaseConnectionString))
            {
                var query = db.Query($"SELECT * FROM Menus m where m.date = DATEFROMPARTS({day.Year},{day.Month},{day.Day}) and m.cook_id = {cookerId}");
                return query.Select(x => new Menu
                {
                    Id = (int) x.id,
                    FormId = x.form_response_id
                }).ToList();
            }
        }

        public Dictionary<int, int> GetDishesRelationsByMenu(int menuId)
        {
            using (IDbConnection db = new SqlConnection(DatabaseConnectionString))
            {
                var query = db.Query($"SELECT * FROM DishForm where menu_id = {menuId}");
                return query.ToDictionary(x => (int) x.form_id, x => (int) x.dish_id);
            }
        }
    }
}
