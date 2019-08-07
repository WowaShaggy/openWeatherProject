using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openweatherApiProject
{
    public class LoggerClass
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        Stopwatch sw;

        public TestContext TestContext { get; set; }

        [TestCleanup]
        public void TestCleanup()
        {
            sw.Stop();
            string testName = string.Format("{0}.{1}", TestContext.FullyQualifiedTestClassName, TestContext.TestName);
            UnitTestOutcome currentTestOutcome = TestContext.CurrentTestOutcome;
            string message = string.Format("Test '{0}' {1} and took {2} ms" , testName, currentTestOutcome.ToString().ToUpperInvariant(), sw.ElapsedMilliseconds);
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
            sw = System.Diagnostics.Stopwatch.StartNew();
            string testName = string.Format("{0}.{1}", TestContext.FullyQualifiedTestClassName, TestContext.TestName);
            Logger.Info("Started with test '{0}'", testName);
        }

    }
}
