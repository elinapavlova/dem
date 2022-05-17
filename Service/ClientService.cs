using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ClientService
    {
        public static List<Client> GetAll()
        {
            using (var context = new Context())
            {
                var clients = context.Clients.ToList();
                return clients;
            }
        }

        public static Client Remove(int id)
        {
            using (var context = new Context())
            {
                var client = context.Clients.FirstOrDefault(c => c.Id == id);
                if (client == null)
                {
                    return null;
                }
                var result = context.Clients.Remove(client);
                context.SaveChanges();
                return result;
            }
        }

        public static Client Add(string surname, string name, string lastname, string phone)
        {
            using (var context = new Context())
            {
                var client = new Client
                {
                    Surname = surname,
                    Name = name,
                    Lastname = lastname,
                    Phone = phone
                };
                var result = context.Clients.Add(client);
                context.SaveChanges();
                return result;
            }
        }

        public static bool IsDataValid(string surname, string name, string lastname, string phone)
        {
            if (!IsSurnameValid(surname) || !IsNameValid(name) || !IsLastnameValid(lastname) || !IsPhoneValid(phone))
            {
                return false;
            }
            return true;
        }

        private static bool IsSurnameValid(string surname)
        {
            return !string.IsNullOrEmpty(surname) && !string.IsNullOrWhiteSpace(surname) && 
                surname.Length >= 6 && surname.Length <= 75 &&
                !ContainsDigit(surname) && !ContainsSymbol(surname);
        }

        private static bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name) &&
                name.Length >= 6 && name.Length <= 75 &&
                !ContainsDigit(name) && !ContainsSymbol(name);
        }

        private static bool IsLastnameValid(string lastname)
        {
            if (string.IsNullOrEmpty(lastname))
            {
                return true;
            }
            return !string.IsNullOrEmpty(lastname) && !string.IsNullOrWhiteSpace(lastname) &&
                lastname.Length >= 6 && lastname.Length <= 75 &&
                !ContainsDigit(lastname) && !ContainsSymbol(lastname);
        }

        private static bool IsPhoneValid(string phone)
        {
            return phone.Length == 11 && !ContainsLetter(phone) && !ContainsSymbol(phone);
        }

        public static string MapPhone(string phone)
        {
            if (phone.StartsWith("+7"))
            {
                phone = phone.Replace("+7", "8");
            }
            return phone;
        }

        private static bool ContainsSymbol(string text)
        {
            foreach (var item in text)
            {
                if (char.IsSymbol(item))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ContainsDigit(string text)
        {
            foreach (var item in text)
            {
                if (char.IsDigit(item))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ContainsLetter(string text)
        {
            foreach (var item in text)
            {
                if (char.IsLetter(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
