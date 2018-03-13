using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PageObject.App;

namespace PageObject.Tests
{

    class TestBase
    {
        public Application app;

        [SetUp]
        public void Start()
        {
            app = new Application();
        }

        [TearDown]
        public void Stop()
        {
            app.Quit();
        }
    }
}
