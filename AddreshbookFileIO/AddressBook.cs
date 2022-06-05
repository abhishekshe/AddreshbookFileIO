using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AddressBook
{
    public class AddressBook
    {
        public Dictionary<string, ContactPersonInformation> addressBookMapper = new Dictionary<string, ContactPersonInformation>();
        public Dictionary<string, List<ContactDetails>> cityDetailsDictionary = new Dictionary<string, List<ContactDetails>>();
        public Dictionary<string, List<ContactDetails>> stateDetailsDictionary = new Dictionary<string, List<ContactDetails>>();
        public HashSet<string> cityList = new HashSet<string>();
        public HashSet<string> stateList = new HashSet<string>();
       
        public void AddAdressBook()
        {
            Console.WriteLine("\nEnter Name for the New Address Book");
            string name = Console.ReadLine();
            
            if (addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("Address Book Already exist with this name");
            }
            else
            {
                
                ContactPersonInformation contactPersonInformation = new ContactPersonInformation();
                addressBookMapper.Add(name, contactPersonInformation);
            }
        }

     
        public void AddContactsInAddressBook()
        {
            Console.WriteLine("\nEnter Name of address book to add new contact");
            string name = Console.ReadLine();
            
            if (!addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("No address book found with this name");
                Console.WriteLine("Please Enter Valid Name from following names:");
                foreach (KeyValuePair<string, ContactPersonInformation> tempPair in addressBookMapper)
                {
                    Console.WriteLine(tempPair.Key);
                }
            }
            else
            {
                
                ContactPersonInformation contactPersonInformation = addressBookMapper[name];
                contactPersonInformation.AddingContactDetails();
                contactPersonInformation.DisplayContactDetails();
            }
        }
        
        public void EditDetailsOfAddressBook()
        {
            Console.WriteLine("\nEnter Name of address book to modify contact details");
            string name = Console.ReadLine();
            
            if (!addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("No address book found with this name");
                Console.WriteLine("Please Enter Valid Name from following names:");
                
                foreach (KeyValuePair<string, ContactPersonInformation> tempPair in addressBookMapper)
                {
                    Console.WriteLine(tempPair.Key);
                }
            }
            else
            {
                
                ContactPersonInformation contactPersonInformation = addressBookMapper[name];
                contactPersonInformation.EditingContactDetails();
                contactPersonInformation.DisplayContactDetails();
            }
        }
       

        public void DeleteContactsOfAddressBook()
        {
            Console.WriteLine("\nEnter Name of address book to delete contact details");
            string name = Console.ReadLine();
            if (!addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("No address book found with this name");
                Console.WriteLine("Please Enter Valid Name from following names:");
                foreach (KeyValuePair<string, ContactPersonInformation> tempPair in addressBookMapper)
                {
                    Console.WriteLine(tempPair.Key);
                }
            }
            else
            {
                
                ContactPersonInformation contactPersonInformation = addressBookMapper[name];
                contactPersonInformation.DeletingContactDetails();
                contactPersonInformation.DisplayContactDetails();
            }
        }
      
        public void DisplayingAddressBooks()
        {
            Console.WriteLine("***********************************************************");
            foreach (KeyValuePair<string, ContactPersonInformation> dictionaryPair in addressBookMapper)
            {
                Console.WriteLine("the name of address book is " + dictionaryPair.Key);
                ContactPersonInformation contactPersonInformation = dictionaryPair.Value;
                contactPersonInformation.DisplayContactDetails();
            }
        }
        
        public void DeletingAddressBook()
        {
            Console.WriteLine("\nEnter Name of address book to delete ");
            string name = Console.ReadLine();
            if (!addressBookMapper.ContainsKey(name))
            {
                Console.WriteLine("No address book found with this name");
                Console.WriteLine("Please Enter Valid Name from following names:");
                foreach (KeyValuePair<string, ContactPersonInformation> tempPair in addressBookMapper)
                {
                    Console.WriteLine(tempPair.Key);
                }
            }
            else
            {
                
                addressBookMapper.Remove(name);
            }
        }
        
        public void SearchingByCity()
        {
            Console.WriteLine("Please enter the city");
            string searchCity = Console.ReadLine();

            
            foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
            {
                Console.WriteLine("Name of the address book: " + keyValuePair.Key);
                ContactPersonInformation contactPersonInformation = keyValuePair.Value;

                contactPersonInformation.SearchingContactDetailsByCity(searchCity);
            }
            Console.WriteLine("Do you want to enter city again, press y for yes");
            string checkInput = Console.ReadLine();
            if (checkInput.ToLower() == "y")
            {
                SearchingByCity();
            }

        }
      
        public void SearchingByState()
        {
            

            Console.WriteLine("Please enter the state");
            string searchState = Console.ReadLine();
            
            foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
            {
                Console.WriteLine("Name of the address book: " + keyValuePair.Key);
                ContactPersonInformation contactPersonInformation = keyValuePair.Value;
                contactPersonInformation.SearchingContactDetailsByState(searchState);
            }
            Console.WriteLine("Do you want to enter state again, press y for yes");
            string checkInput = Console.ReadLine();
            if (checkInput.ToLower() == "y")
            {
                
                SearchingByState();
            }

        }
        
        public void ViewingCityDictionary()
        {
            foreach (KeyValuePair<string, List<ContactDetails>> cityDetails in cityDetailsDictionary)
            {
                Console.WriteLine(cityDetails.Key + ":");
                
                int index = 0;
                foreach (ContactDetails contactPerson in cityDetails.Value)
                {
                    Console.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
                    index++;
                }
                
                Console.WriteLine($"Total no of contact details in {cityDetails.Key} are {index}");
                Console.WriteLine("");

            }
        }
       
        public void ViewingStateDictionary()
        {
            foreach (KeyValuePair<string, List<ContactDetails>> stateDetails in stateDetailsDictionary)
            {
                Console.WriteLine(stateDetails.Key);
                
                int index = 0;
                foreach (ContactDetails contactPerson in stateDetails.Value)
                {
                    Console.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
                    index++;

                }
                
                Console.WriteLine($"Total no of contact details in {stateDetails.Key} are {index}");
                Console.WriteLine("");
            }
        }
       
        public void GettingCityNames()
        {
            
            foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
            {
                ContactPersonInformation contactPersonInformation = keyValuePair.Value;
                
                foreach (string city in contactPersonInformation.GettingCityList())
                {
                    cityList.Add(city);
                }
            }
        }
       
        public void CreatingCityDictionary()
        {
            
            foreach (string cityName in cityList)
            {
                
                List<ContactDetails> cityDetailsList = new List<ContactDetails>();
                
                foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
                {
                    ContactPersonInformation contactPersonInformation = keyValuePair.Value;
                    cityDetailsList = contactPersonInformation.AddingContactDetailsByCity(cityName, cityDetailsList);
                }
                
                cityDetailsDictionary.Add(cityName, cityDetailsList);
            }
        }
       
        public void GettingStateNames()
        {
            
            foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
            {
                ContactPersonInformation contactPersonInformation = keyValuePair.Value;
         
                foreach (string state in contactPersonInformation.GettingStateList())
                {
                    stateList.Add(state);
                }
            }
        }
      
        public void CreatingStateDictionary()
        {
            
            foreach (string stateName in stateList)
            {
                
                List<ContactDetails> stateDetailsList = new List<ContactDetails>();
                
                foreach (KeyValuePair<string, ContactPersonInformation> keyValuePair in addressBookMapper)
                {
                    ContactPersonInformation contactPersonInformation = keyValuePair.Value;
                    stateDetailsList = contactPersonInformation.AddingContactDetailsByState(stateName, stateDetailsList);
                }
                
                stateDetailsDictionary.Add(stateName, stateDetailsList);
            }
        }
    }
}