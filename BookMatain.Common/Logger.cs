using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Common
{
    public class Logger
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public enum LogCategoryEnum
        {
            Error,
            Warn,
            Info,
            Debug
        }
        public static void Write(string message, LogCategoryEnum logCategory)
        {
            switch (logCategory)
            {
                case LogCategoryEnum.Error:
                    logger.Error(message);
                    break;
                case LogCategoryEnum.Warn:
                    logger.Warn(message);
                    break;
                case LogCategoryEnum.Info:
                    logger.Info(message);
                    break;
                case LogCategoryEnum.Debug:
                    logger.Debug(message);
                    break;
                default:
                    break;
            }
        }
    }
}
