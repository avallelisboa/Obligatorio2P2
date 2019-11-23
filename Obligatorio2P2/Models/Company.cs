using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    public class Company : Client
    {
        private string bussinesName;
        private int rut;
        private int discount;

        public string BussinesName { get { return bussinesName; } }
        public int Rut { get { return rut; } }
        public int Discount { get { return discount; } }

        public class companyValidation
        {
            public companyValidation(bool isBussinesNameUsed, bool isRutUsed)
            {
                this.isBussinesNameUsed = isBussinesNameUsed;
                this.isRutUsed = isRutUsed;
            }
            public bool isBussinesNameUsed;
            public bool isRutUsed;
        }

        public static companyValidation isInformationCorrect(List<Company> clients, string companyName, string bussinesName, int rut)
        {
            int id = clients.Count;
            bool isBussinesNameUsed = false;
            bool isRutUsed = false;
            foreach (Company c in clients)
            {
                if (c.GetType() == typeof(Company))
                {
                    if (c.bussinesName == bussinesName)
                    {
                        isBussinesNameUsed = true;
                        break;
                    }
                    if (c.rut == rut)
                    {
                        isRutUsed = true;
                        break;
                    }
                }

            }
            companyValidation companyValidation = new companyValidation(isBussinesNameUsed, isRutUsed);
            return companyValidation;
        }

        private Company(int id, string companyName, string bussinesName, int rut, string address, string mail, string phone, string user, string password, bool isFromMontevideo, int discount) : base(id, companyName, address, mail, phone, isFromMontevideo)
        {
            this.bussinesName = bussinesName;
            this.rut = rut;
        }
        
        public static Company AddCompanyClient(int id, string companyName, string bussinesName, int rut, string address, string mail, string phone, string user, string password, bool isFromMontevideo, int discount)
        {
            return new Company(id, companyName, bussinesName, rut, address, mail, phone, user, password, isFromMontevideo, discount);
        }

        public override string ToString()
        {
            return "Company";
        }
    }
}