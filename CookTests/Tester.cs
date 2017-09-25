﻿using System;
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
            CookFormManager cookFormManager = new CookFormManager();
            cookFormManager.Test();
        }

        [Test]
        public void DbTest()
        {
            DbService dbService = new DbService();
            var cooker = dbService.GetCooker(1);
        }
    }
}