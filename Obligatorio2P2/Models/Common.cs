using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem
{
    public class Common : Client
    {
        private string address;
        private int identificationCard;
        private Common(int id, string name, int identificationCard, string phone, string address, string mail, bool isFromMontevideo) : base(id, name, address, mail, phone, isFromMontevideo)
        {
            this.address = address;
        }

        public int IdentificationCard { get { return identificationCard; } }

        public class commonValidation
        {
            public commonValidation(bool isIdentificationCardUsed)
            {
                this.isIdentificationCardUsed = isIdentificationCardUsed;
            }
            public bool isIdentificationCardUsed;
        }

        public static commonValidation isInformationCorrect(List<Common> clients, int identificationCard)
        {
            int id = clients.Count;
            bool isIdentificationCardUsed = false; ;
            foreach (Common c in clients)
            {
                if (c.GetType() == typeof(Common))
                {

                    if (c.IdentificationCard == identificationCard)
                    {
                        isIdentificationCardUsed = true;
                        break;
                    }
                }

            }
            commonValidation commonValidation = new commonValidation(isIdentificationCardUsed);
            return commonValidation;
        }

        public static Common AddCommonClient(int id, string name, int identificationCard, string phone, string address, string mail, string user, string password, bool isFromMontevideo)
        {
            return new Common(id, name, identificationCard, phone, address, mail, isFromMontevideo);
        }

        public override string ToString()
        {
            return "Common";
        }
    }
}