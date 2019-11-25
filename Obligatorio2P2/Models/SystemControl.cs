using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    public class SystemControl
    {
        private SystemControl() { }                                                                  //Private constructor
        private static SystemControl _systemControl = new SystemControl();                           //Just one instance
        public static SystemControl getSystemControl() { return _systemControl; }                    //Get the instance
        private List<Client> clients = new List<Client>();                                           //Clients list
        private List<User> users = new List<User>();
        private List<ProductStock> catalogue = new List<ProductStock>();                             //Lista de productStocks(las categorías). Para cada categoría hay un productStock
        private List<Purchase> purchases = new List<Purchase>();                                     //Purchases list
        private User loggedUser;
        public int NumberOfClients { get { return clients.Count; } }
        public List<ProductStock> getCatalogue() { return catalogue; }

        public class registerStatus
        {
            public registerStatus(bool wasRegisterSuccessful, string message)
            {
                this.wasRegisterSuccessful = wasRegisterSuccessful;
                this.message = message;
            }
            public bool wasRegisterSuccessful;
            public string message;
        }

        public string setRole(User user, string role)
        {
            string message;
            if(role == "Client" && user.Client == null)
            {
                return "Debe asignarle un cliente para poder cambiar su rol a cliente";
            }
            else if(role == "Client" && user.Client != null)
            {
                message = user.setRole("Client");
            }
            else
            {
                message = user.setRole(role);
            }
            return message;
        }

        public string setRole(User user, string role, Client client)
        {
            string message;
            if(role == "Client" && client == null)
            {
                message = user.setRole(role);
                user.setClient(client);
            }
            else if(role == "Client" && client != null)
            {
                message = "El usuario ya tiene un cliente asignado";
            }
            else
            {
                message = "El usuario debe tener rol cliente para asignarle uno";
            }
            return message;
        }

        public registerStatus addUser(string user, string password, string name, string role)
        {
            int id = users.Count;
            var isUserInformationCorrect = User.isInformationCorrect(users, user);
            bool isUserUsed = isUserInformationCorrect.isUserUsed;
            if (!isUserUsed)
            {
                User _user = new User(user, password, name, role);
                users.Add(_user);
                return new registerStatus(true, "El usuario fue registrado correctamente");
            }
            else return new registerStatus(false, "El usuario no fue registrado");
        }

        public registerStatus addCommonClient(string name, int identificationCard, string celular, string mail, string address, string user, string password, bool isFromMontevideo)
        {
            int id = clients.Count;
            var isClientInformationCorrect = Client.isInformationCorrect(clients, user, mail);
            bool isMailUsed = isClientInformationCorrect.isMailUsed;

            List<Common> commonClientList = new List<Common>();
            foreach (Client c in clients)
            {
                if (c.GetType() == typeof(Common))
                {
                    commonClientList.Add((Common)c);
                }
            }
            var isCommonClientInformationCorrect = Common.isInformationCorrect(commonClientList, identificationCard);
            bool isIdentificationCardUsed = isCommonClientInformationCorrect.isIdentificationCardUsed;
            if (!isMailUsed && !isIdentificationCardUsed)
            {
                Client _client = Common.AddCommonClient(id, name, identificationCard, celular, address, mail, user, password, isFromMontevideo);
                clients.Add(_client);
                return new registerStatus(true, "El cliente fue registrado correctamente");
            }
            else return new registerStatus(false, "El cliente no fue registrado");
        }

        public registerStatus addCompanyClient(string companyName, string bussinesName, int rut, string mail, string phone, string address, string user, string password, bool isFromMontevideo, int discount)
        {
            int id = clients.Count;
            var isClientInformationCorrect = Client.isInformationCorrect(clients, user, mail);
            bool isMailUsed = isClientInformationCorrect.isMailUsed;

            List<Company> companyClientList = new List<Company>();
            foreach (Client c in clients)
            {
                if (c.GetType() == typeof(Company))
                {
                    companyClientList.Add((Company)c);
                }
            }
            var isCompanyClientInformationCorrect = Company.isInformationCorrect(companyClientList, companyName, bussinesName, rut);
            bool isBussinesNameUsed = isCompanyClientInformationCorrect.isBussinesNameUsed;
            bool isRutUsed = isCompanyClientInformationCorrect.isRutUsed;
            if (!isMailUsed && !isBussinesNameUsed && !isRutUsed)
            {
                Client _client = Company.AddCompanyClient(id, companyName, bussinesName, rut, address, mail, phone, user, password, isFromMontevideo, discount);
                clients.Add(_client);
                return new registerStatus(true, "El cliente fue registrado correctamente");
            }
            else return new registerStatus(false, "El cliente no fue registrado");
        }

        public void preLoad()
        {
            addCommonClient("Jorge", 45876212, "091425631", "jorgito@gmail.com", "Bulevar Artigas 87546", "jorge", "jorge", true);        //Nombre, CI, celular, mail, dirección, usuario, contraseña, esDeMonevideo?
            addCommonClient("Javier", 54789653, "091879564", "javier@gmail.com", "Bulevar Artigas 97463", "javier", "javier", true);
            addCommonClient("Juan", 16549829, "091879564", "juan@gmail.com", "Bulevar Artigas 1087465", "juan", "juan", false);

            addCompanyClient("company1", "company1 s.a.", 154684654, "company1@gmail.com", "25842143579", "Luis Alberto de Herrera 154843223", "company1", "company1", true, 3); //nombre empresa, razón social, rut, mail, teléfono, address, usuario, contraseña, esDeMontevideo
            addCompanyClient("company2", "company2 s.a.", 878746845, "company2@gmail.com", "65487651321", "Luis Alberto de Herrera 648948455", "company2", "company2", true, 7);
            addCompanyClient("company3", "company3 s.a.", 346456148, "company3@gmail.com", "324564561551", "Luis Alberto de Herrera 878456456", "company3", "company3", false, 5);

            addProductStock("Frescos"); addProductStock("Congelados"); addProductStock("Hogar"); addProductStock("Téxtiles"); addProductStock("Tecnología"); //Categorías precargadas(1 ProductStock para cada categoría)

            catalogue[0].addProduct("Escarola", 59, "Precio por Kg", false, 100);            //Nombre, precio, descripción, esDeMontevideo                            //Se  agregan los productos
            catalogue[0].addProduct("Espinaca", 24, "Precio por Kg", false, 100);

            catalogue[1].addProduct("Croquetas", 99, "Precio por Kg", false, 100);
            catalogue[1].addProduct("Buñuelo", 40, "Precio por Kg", true, 100);

            catalogue[2].addProduct("Detergente", 60, "Precio por L", false, 100);
            catalogue[2].addProduct("Jabón de manos", 35, "Precio por unidad", true, 100);

            catalogue[3].addProduct("Toallas", 70, "Precio por unidad", false, 100);
            catalogue[3].addProduct("Sábanas", 150, "Precio por unidad", false, 100);

            catalogue[4].addProduct("PC Gamer", 12500, "Precio por unidad", false, 100);
            catalogue[4].addProduct("Televisor Led", 15000, "Precio por unidad", false, 10);

            login("jorge", "jorge");                            //Compras de los clientes
            var purchase1 = getPurchase();
            purchase1.addToPurchase(0, 0, 10);
            purchase1.buy();
            var purchase2 = getPurchase();
            purchase2.addToPurchase(0, 1, 10);
            purchase2.buy();
            login("javier", "Javier");
            var purchase3 = getPurchase();
            purchase3.addToPurchase(1, 0, 10);
            purchase3.buy();
            var purchase4 = getPurchase();
            purchase4.addToPurchase(1, 1, 10);
            purchase4.buy();
            login("juan", "juan");
            var purchase5 = getPurchase();
            purchase5.addToPurchase(2, 0, 10);
            purchase5.buy();
            var purchase6 = getPurchase();
            purchase6.addToPurchase(2, 1, 10);
            purchase6.buy();
        }

        public bool login(string user, string password)
        {
            bool wasFounded = false;
            bool isPasswordCorrect = false;
            foreach (User _user in users)
            {
                if (_user.UserName == user)
                {
                    wasFounded = true;
                    if (_user.Password == password) isPasswordCorrect = true;
                    loggedUser = _user;
                    break;
                }
            }
            if (wasFounded && isPasswordCorrect) return true;
            else return false;
        }

        public Purchase getPurchase()
        {
            if (loggedUser != null && loggedUser.Client != null)
            {
                Purchase _purchase = Purchase.getPurchase(loggedUser.Client, catalogue);
                purchases.Add(_purchase);
                loggedUser.Client.addPurchase(_purchase);
                return _purchase;
            }
            else throw new Exception("There is no logged user");
        }

        public string addProductStock(string name)
        {
            int id = catalogue.Count;
            bool wasFounded = false;
            foreach (ProductStock s in catalogue)
            {
                if (s.Name == name) wasFounded = true;
            }
            if (!wasFounded)
            {
                catalogue.Add(new ProductStock(name, id));
                return "The product stock was successfully created";
            }
            else return "The product stock was already created";
        }

        public List<Client> getClientsByDate(DateTime date)
        {
            List<Client> _clients = new List<Client>();
            foreach (Client c in clients)
            {
                int result = DateTime.Compare(c.RegisterDate, date);
                if (result < 0) _clients.Add(c);
            }
            return _clients;
        }

        public List<Purchase> getPurchasesBetweenDates(DateTime d1, DateTime d2)
        {
            List<Purchase> _purchases = new List<Purchase>();
            foreach (Purchase p in purchases)
            {
                int result1 = DateTime.Compare(p.Date, d1);
                int result2 = DateTime.Compare(p.Date, d2);
                if (result1 > 0 && result2 < 0) _purchases.Add(p);
            }
            return _purchases;
        }

        public List<Product> getLast10Products(Client client)
        {
            List<Product>_products = client.getLast10Products();
            return _products;
        }

        public List<Product> getMostBoughtProducts(Client client)
        {
            List<Product> _products = client.getMostBoughtProduct();
            return _products;
        }

        public DateTime getDateLastPurchase(Client client)
        {
            DateTime date = client.getDateLastPurchase();
            return date;
        }

        public void changeProductdPrice(int productStockId, int productId, int productPrice)
        {
            catalogue[productStockId].changeProductPrice(productId, productPrice);
        }

        public void changeProductDescription(int productStockId, int productId, string productDescription)
        {
            catalogue[productStockId].changeProductDescription(productId, productDescription);
        }
    }
}
