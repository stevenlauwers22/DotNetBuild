using System;
using DotNetBuild.Core.Facilities.Logging;

namespace DotNetBuild.Runner.Facilities.Logging
{
    public class LoggerFacilityProvider
        : FacilityProvider<IWantToLog, ILogger>
    {
        private readonly Func<ILogger> _loggerFunc;
        
        public LoggerFacilityProvider(Infrastructure.Logging.ILogger logger, Func<ILogger> loggerFunc)
            : base(logger)
        {
            if (loggerFunc == null) 
                throw new ArgumentNullException("loggerFunc");

            _loggerFunc = loggerFunc;
        }

        protected override ILogger GetFacility()
        {
            return _loggerFunc();
        }
    }
}