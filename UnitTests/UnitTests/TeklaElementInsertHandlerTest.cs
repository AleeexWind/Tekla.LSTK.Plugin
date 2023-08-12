using LSTK.Frame.Frameworks.TeklaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.UnitTests
{
    public class TeklaElementInsertHandlerTest
    {
        [Fact]
        public void IsInsertionSucceed()
        {
            //Act
            bool success = TeklaElementInsertHandler.InsertElement(null);

            //Assert
            Assert.False(success);
        }        
    }
}
