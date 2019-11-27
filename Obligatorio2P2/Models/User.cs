using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopSystem
{
    public class User
    {
        private string user;
        private string password;
        private string name;
        private string role;
        private int id;
        private Client client;

        public string UserName { get { return user; } }
        public string Password { get { return password; } }
        public string Name { get { return name; } }
        public string Role { get { return role; } }
        public int Id { get { return id; } }
        public Client Client { get { return client; } }

        public User(string user, string password, string name, string role, int id)
        {
            this.user = user;
            this.password = password;
            this.name = name;
            this.role = role;
            this.id = id;
        }

        public class userValidation
        {
            public userValidation(bool isUserUsed)
            {
                this.isUserUsed = isUserUsed;
            }
            public bool isUserUsed;
        }

        public static userValidation isInformationCorrect(List<User> users, string user)
        {
            int id = users.Count;
            bool isUserUsed = false;
            foreach (User u in users)
            {
                if (u.UserName == user)
                {
                    isUserUsed = true;
                }
                if (isUserUsed) break;
            }
            userValidation _userValidation = new userValidation(isUserUsed);
            return _userValidation;
        }

        public string setRole(string role)
        {
            if (role == "client" || role == "guest" || role == "admin")
            {
                this.role = role;
                return "El rol fue cambiado correctamente";
            }
            else return "El rol ingresado es inválido";
        }

        public void setClient(Client client)
        {
            this.client = client;
        }
    }
}