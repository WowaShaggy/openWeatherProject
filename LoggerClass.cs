using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openweatherApiProject
{
    public class LoggerClass
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public TestContext TestContext { get; set; }

        [TestCleanup]
        public void TestCleanup()
        {
            string testName = string.Format("{0}.{1}", TestContext.FullyQualifiedTestClassName, TestContext.TestName);
            UnitTestOutcome currentTestOutcome = TestContext.CurrentTestOutcome;
            string message = string.Format("Test '{0}' {1}" , testName, currentTestOutcome.ToString().ToUpperInvariant());
            if (currentTestOutcome != UnitTestOutcome.Passed)
            {
                Logger.Error(message);
            }
            else
            {
                Logger.Info(message);
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            string testName = string.Format("{0}.{1}", TestContext.FullyQualifiedTestClassName, TestContext.TestName);
            Logger.Info("Started with test '{0}'", testName);
        }

    }
}
