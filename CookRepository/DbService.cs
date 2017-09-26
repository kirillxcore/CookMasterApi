﻿using System;
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
                "Server=192.168.50.39;Database=CookMaster;User ID=sa;Password=qwe123;";

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
