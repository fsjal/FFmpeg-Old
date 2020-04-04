using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace FFmpeg.Util
{
    public static class Logger
    {
        public static ILog Log { get; } = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static string ConfigFile { get; set; } = "log4net.config";
        static Logger()
        {
            XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(ConfigFile));
        }
    }
}
