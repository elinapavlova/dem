using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckLoginTest()
        {
            string login = "admin";

            var result = UserService.IsLoginValid(login);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CheckPasswordTest()
        {
            string password = "admin";

            var result = UserService.IsPasswordValid(password);
            Assert.AreEqual(false, result);
        }
    }
}
