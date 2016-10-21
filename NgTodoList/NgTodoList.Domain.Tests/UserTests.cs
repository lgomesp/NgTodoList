using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NgTodoList.Domain.Tests
{
    //classe para teste de usuários
    [TestClass]
    public class Given_an_user
    {
        [TestMethod]
        [TestCategory("User - New User")]
        //este teste espera uma exceção como retorno
        [ExpectedException(typeof(Exception))]
        public void The_name_might_be_valid()
        {
            var user = new User("L", "lucas@lucas.com", "lucas");
        }

        [TestMethod]
        [TestCategory("User - New User")]
        [ExpectedException(typeof(Exception))]
        public void The_email_might_not_be_null()
        {
            var user = new User("Lucas Gomes", "", "lucas");
        }

        [TestMethod]
        [TestCategory("User - New User")]
        [ExpectedException(typeof(Exception))]
        public void The_email_might_be_valid()
        {
            var user = new User("Lucas Gomes", "teste", "lucas");
        }

        [TestMethod]
        [TestCategory("User - New User")]
        //este teste espera uma exceção como retorno
        [ExpectedException(typeof(Exception))]
        public void The_password_might_be_valid()
        {
            var user = new User("Lucas", "lucas@lucas.com", "123");
        }

        [TestMethod]
        [TestCategory("User - New User")]
        public void The_user_is_valid()
        {
            var user = new User("Lucas", "lucas@lucas.com", "1234567");
            //usuário não é nulo
            Assert.AreNotEqual(null, user);
        }

    }

    [TestClass]
    public class Changing_password
    {
        private User user = new User("Lucas Gomes", "lucas@lucas.com", "lucas122");

        [TestMethod]
        [TestCategory("User - Change Password")]
        [ExpectedException(typeof(Exception))]
        public void The_email_might_be_valid()
        {
            user.ChangePassword("", "lucas", "lucasgomes", "lucasgomes");
        }

        [TestMethod]
        [TestCategory("User - Change Password")]
        [ExpectedException(typeof(Exception))]
        public void The_password_might_be_valid()
        {
            user.ChangePassword("lucas@lucas.com", "asd", "lucasgomes", "lucasgomes");
        }

        [TestMethod]
        [TestCategory("User - Change Password")]
        [ExpectedException(typeof(Exception))]
        public void User_and_password_might_be_valid()
        {
            user.ChangePassword("lucas@lucas.com", "lucas", "as", "as");
        }

        [TestMethod]
        [TestCategory("User - Change Password")]
        [ExpectedException(typeof(Exception))]
        public void The_password_and_conf_should_match()
        {
            user.ChangePassword("lucas@lucas.com", "asd", "lucas123", "lucas12");
        }

        [TestMethod]
        [TestCategory("User - Change Password")]
        [ExpectedException(typeof(Exception))]
        public void The_password_might_be_encrypted()
        {
            var password = "mysecurepassword";
            user.ChangePassword("lucas@lucas.com", "asd", password, password);

            Assert.AreNotEqual(password, user.Password);
        }
    }
}
