using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PageObject.Tests
{
    [TestFixture]
    class WorkWithCartTest : TestBase
    {
        [Test]
        public void WorkWithCart()
        {
            app.AddAndDeleteProductCart();
        }
    }
}
