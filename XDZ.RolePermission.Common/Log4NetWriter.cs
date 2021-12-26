using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDZ.RolePermission.Common
{
    public class Log4NetWriter : ILogWrite
    {
        public void WriteLogInfo(string txt)
        {
            ILog logWriter = log4net.LogManager.GetLogger("Demo");
            logWriter.Error(txt);
        }
    }
}
