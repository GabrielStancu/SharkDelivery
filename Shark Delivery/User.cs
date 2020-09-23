using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Delivery
{
    public class User
    {
        private readonly int Id;
        private string FirstName;
        private string LastName;
        private string Town;
        private string Street;
        private string FlatHouseNr;
        private string PhoneNr;
        private string MailAddress;

        public User(int id, string first, string last, string town, string street, string flat, string phone, string mail)
        {
            this.Id = id;
            this.FirstName = first;
            this.LastName = last;
            this.Town = town;
            this.Street = street;
            this.FlatHouseNr = flat;
            this.PhoneNr = phone;
            this.MailAddress = mail;
        }

        public int GetId()
        {
            return this.Id;
        }

        public string GetFirstName()
        {
            return this.FirstName;
        }

        public string GetLastName()
        {
            return this.LastName;
        }

        public string GetTown()
        {
            return this.Town;
        }

        public string GetStreet()
        {
            return this.Street;
        }

        public string GetFlatHouseNr()
        {
            return this.FlatHouseNr;
        }

        public string GetPhoneNr()
        {
            return this.PhoneNr;
        }

        public string GetMailAddress()
        {
            return this.MailAddress;
        }



        public void SetFirstName(string name)
        {
            this.FirstName = name;
        }

        public void SetLastName(string name)
        {
            this.LastName = name;
        }

        public void SetTown(string town)
        {
            this.Town = town;
        }

        public void SetStreet(string street)
        {
            this.Street = street;
        }

        public void SetFlatHouseNr(string nr)
        {
            this.FlatHouseNr = nr;
        }

        public void SetPhoneNr(string nr)
        {
            this.PhoneNr = nr;
        }

        public void SetMailAddress(string mail)
        {
            this.MailAddress = mail;
        }
    }
}
