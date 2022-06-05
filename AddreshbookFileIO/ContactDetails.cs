using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook
{
    public class ContactDetails
    {
        public string firstName;
        public string lastName;
        public string address;
        public string city;
        public string state;
        public int zip;
        public double phoneNo;
        public string eMail;

        
        public ContactDetails(string firstName, string lastName, string address, string city, string state, int zip, double phoneNo, string eMail)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phoneNo = phoneNo;
            this.eMail = eMail;
        }

    }
}