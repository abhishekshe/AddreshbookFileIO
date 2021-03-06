using System;

namespace AddressBook
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Management System");
            //creating object for Address Book Class
            AddressBook addressBook = new AddressBook();
            bool flag = true;

            while (flag)
            {
                
                Console.WriteLine("\nEnter 1 to add New Address Book ");
                Console.WriteLine("Enter 2 to Add Contacts");
                Console.WriteLine("Enter 3 to Edit Contacts");
                Console.WriteLine("Enter 4 to Delete Contacts");
                Console.WriteLine("Enter 5 to display all the addressbooks and contact details");
                Console.WriteLine("Enter 6 to delete address book");
                Console.WriteLine("Enter 7 to Search Contact Details using City");
                Console.WriteLine("Enter 8 to search Contact Details using state");
                Console.WriteLine("Enter 9 to view contact details and count with city");
                Console.WriteLine("Enter 10 to view contact details and count with state");
                Console.WriteLine("Enter 11 to write contact details into text file");
                Console.WriteLine("Enter 12 to write contact details into csv file");
                Console.WriteLine("Enter any other key to exit");


                string options = Console.ReadLine();
                switch (options)
                {
                    case "1":
                        addressBook.AddAdressBook();
                        //addressBook.DisplayingAddressBooks(); 
                        break;
                    case "2":
                        addressBook.AddContactsInAddressBook();
                        break;
                    case "3":
                        addressBook.EditDetailsOfAddressBook();
                        break;
                    case "4":
                        addressBook.DeleteContactsOfAddressBook();
                        break;
                    case "5":
                        addressBook.DisplayingAddressBooks();
                        break;
                    case "6":
                        addressBook.DeletingAddressBook();
                        break;
                    case "7":
                        addressBook.SearchingByCity();
                        break;
                    case "8":
                        addressBook.SearchingByState();
                        break;
                    case "9":
                        addressBook.GettingCityNames();
                        addressBook.CreatingCityDictionary();
                        addressBook.ViewingCityDictionary();
                        break;
                    case "10":
                        addressBook.GettingStateNames();
                        addressBook.CreatingStateDictionary();
                        addressBook.ViewingStateDictionary();
                        break;
                    case "11":
                        addressBook.WritingAddressBookInTextFile();
                        addressBook.ReadingContactDetailsFromTextFile();
                        break;
                    case "12":
                        addressBook.WritingAddressBookInCsvFile();
                        addressBook.ReadingContactDetailsFromCSVFile();
                        break;
                    default:
                        flag = false;
                        break;
                }
            }
        }
    }
}