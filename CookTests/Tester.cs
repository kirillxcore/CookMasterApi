using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookFormMaster;
using CookRepository;
using NUnit.Framework;

namespace CookTests
{
    public class Tester
    {
        [Test]
        public void Test()
        {
        }

        [Test]
        public void DbTest()
        {
            DbService dbService = new DbService();
            var dishes = dbService.GetDishes();
        }
    }
}
