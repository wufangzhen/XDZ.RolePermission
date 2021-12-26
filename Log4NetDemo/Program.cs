using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //从配置文件App.config或web.config中读取log4net的配置，然后进行一个初始化工作
            log4net.Config.XmlConfigurator.Configure();
            ILog logWriter =log4net.LogManager.GetLogger("DemoWriter");
            logWriter.Info("ddd");
            logWriter.Debug("sssss调试级别的消息");
            logWriter.Error("ccccc错误级别的消息");
        }
    }
}
