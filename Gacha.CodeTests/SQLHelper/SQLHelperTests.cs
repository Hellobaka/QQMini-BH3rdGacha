using Microsoft.VisualStudio.TestTools.UnitTesting;
using BH3rdGacha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH3rdGacha.Tests
{
    [TestClass()]
    public class SQLHelperTests
    {
        [TestMethod()]
        public void CreateDBTest()
        {
            try
            {
                SQLHelper.Init(@"E:\QQMini机器人开发\QQMiniPro\Data\me.luohuaming.bh3gacha\data.db");
                SQLHelper.CreateDB();
                Console.WriteLine("S");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message+e.StackTrace);
                Assert.Fail();
            }
        }
    }
}