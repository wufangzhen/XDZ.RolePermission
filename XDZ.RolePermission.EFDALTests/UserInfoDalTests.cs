using Microsoft.VisualStudio.TestTools.UnitTesting;
using XDZ.RolePermission.EFDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDZ.RolePermission.Model;

namespace XDZ.RolePermission.EFDAL.Tests
{
    [TestClass()]
    public class UserInfoDalTests
    {
        [TestMethod()]
        public void GetEntitiesTest()
        {
            UserInfoDal userInfoDal = new UserInfoDal();
            IQueryable<UserInfo> temp = userInfoDal.GetEntities(u => u.UName.Contains("zs"));
            Assert.AreEqual(true, temp.Count() > 0);
        }

        [TestMethod()]
        public void AddTest()
        {
            UserInfoDal userInfoDal = new UserInfoDal();
            for (int i = 0; i < 10; i++)
            {
                userInfoDal.Add(new UserInfo() { UName = "xzx" + i, Pwd = "123", ShowName = "徐照兴" + i });
            }
            IQueryable<UserInfo> temp = userInfoDal.GetEntities(u => u.UName.Contains("xzx") && u.Pwd == "123");
            Assert.AreEqual(true, temp.Count() >= 10);
        }
        [TestMethod()]
        public void UpdateTest()
        {
            UserInfoDal userInfoDal = new UserInfoDal();
            bool b = userInfoDal.Update(new UserInfo() { Id = 4, Pwd = "123", UName = "zs", ShowName = "张三" });
            Assert.AreEqual(true, b);
        }

        [TestMethod()]
        public void GetPageEntitiesTest()
        {
            UserInfoDal userInfoDal = new UserInfoDal();
            IQueryable<UserInfo> temp= userInfoDal.GetPageEntities<int>(5, 3,out int totalNum, u => u.UName.Contains("xdz"), u => u.Id, true);
            Assert.AreEqual(21, totalNum);
        }
    }
}