using System.Linq;

namespace Service
{
    public class UserService
    {
        public static User Authorize(string login, string password)
        {
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (user == null)
                {
                    return null;
                }
                user.Role = context.Roles.FirstOrDefault(r => r.Id == user.RoleId);
                return user;
            }
        }

        public static bool IsLoginExist(string login)
        {
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == login);
                if (user != null)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool IsLoginValid(string login)
        {
            foreach (var character in login)
            {
                if (char.IsDigit(character) || char.IsSymbol(character))
                {
                    return false;
                }
            }

            return login.Length >= 6 && login.Length <= 15;
        }

        public static bool IsPasswordValid(string password)
        {
            return password.Length >= 6 && password.Length <= 30;
        }
    }
}
