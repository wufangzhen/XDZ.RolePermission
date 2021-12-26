using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    public class UserInfoBll
    {
        public IUserInfoDal userInfoDal { get; set; }
        public void ShowBll()
        {
            userInfoDal.Show();
            Console.WriteLine("复杂属性注入演示");
        }
    }
}
