using System.Globalization;
using System.Text.Json;

namespace AddressBook
{
    public class ContactPersonInformation
    {
        //Declaring List to store contact details
        List<ContactDetails> contactDetailsList;
        Dictionary<string, List<ContactDetails>> cityDetailsDictionary;
        HashSet<string> cityList;
        HashSet<string> stateList;
        private readonly NLog nLog = new NLog();

       
        public ContactPersonInformation()
        {
            contactDetailsList = new List<ContactDetails>();
            cityDetailsDictionary = new Dictionary<string, List<ContactDetails>>();
            cityList = new HashSet<string>();
            stateList = new HashSet<string>();
        }

        
        public void AddingContactDetails()
        {
            ContactPersonInformation contactPersonalInformation = new ContactPersonInformation();
            
                

            while (true)
            {
            //used goto method to call the method again
            Repeat: Console.WriteLine("Please enter first name, last name, address, city, state, zip, phoneno and email");
                string firstName = Console.ReadLine();
                if (firstName == "")
                {
                    //if first name is null, then no more contact details are entered
                    nLog.LogInfo("No more contact details have been entered");
                    break;
                }

                string lastName = Console.ReadLine();
                bool checkForContactInList = contactPersonalInformation.CheckingForNameinExistingContactList(contactDetailsList, firstName, lastName);
                if (checkForContactInList == false)
                {
                    continue;
                }
                string address = Console.ReadLine();
                string city = Console.ReadLine();
                string state = Console.ReadLine();
                int zip = Convert.ToInt32(Console.ReadLine());
                double phoneNo = Convert.ToDouble(Console.ReadLine());
                if (phoneNo <= 200000)
                {
                    //if phone no is less than 200000 than details are entered again
                    nLog.LogError("Entered Wrong Phone no. : AdditionContactDetails()");
                    Console.WriteLine("Wrong phone details entered, please enter your details again");
                    goto Repeat;
                }
                string eMail = Console.ReadLine();

                ContactDetails contactDetails = new ContactDetails(firstName, lastName, address, city, state, zip, phoneNo, eMail);

                //Adding Contact details in the list
                contactDetailsList.Add(contactDetails);
                nLog.LogDebug("Contact Details Addition Successful: AddingContactDetails()");
            }
            //sorting the values in the address book using lambda expression

        }
        
        public void DisplayContactDetails()
        {
            SortingContactDetails();

            foreach (ContactDetails contactPerson in contactDetailsList)
            {
                Console.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
            }
            nLog.LogDebug("Displaying Contact Details Successful :DisplayingContactDetails()");
        }
       
        public void AddingContactDetailsinTextFile(string addressBookName)
        {
            string path = @"C:\Users\vishu\source\repos\Address Book FileIO Day 20\Address Book FileIO Day 20\DataFile.txt";
            if (File.Exists(path))
            {

                using (StreamWriter sr = File.AppendText(path))
                {
                    sr.WriteLine("The name of the address book is" + addressBookName);
                    foreach (ContactDetails contactPerson in contactDetailsList)
                    {
                        sr.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
                    }

                    
                    sr.Close();
                }
            }
        }
     
        public void AddingContactDetailsInCsvFile(string addressBookName)
        {

            //giving path of the csv file with the help of address book name
            string path = @"C:\Users\vishu\source\repos\Address Book FileIO Day 20\Address Book FileIO Day 20\ContactListCsv\" + addressBookName + ".csv";
            //path for writing each csv file, is converted into stream
            StreamWriter writer = new StreamWriter(path);
            
            var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            foreach (ContactDetails contactPerson in contactDetailsList)
            {
                Console.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
            }
            
            csv.WriteRecords(contactDetailsList);
            
            writer.Flush();
            
            writer.Close();

        }
        public void AddingContactDetailsInJsonFile(string addressBookName)
        {
            
            string pathforJson = @"C:\Users\abhis\source\repos\AddreshbookFileIO\AddreshbookFileIO\Abhi.json" + addressBookName + ".json";
            
            StreamWriter writerJson = new StreamWriter(pathforJson);
            
            JsonSerializer serializer = new JsonSerializer();
            JsonWriter sw = new JsonTextWriter(writerJson);
            
            serializer.Serialize(sw, contactDetailsList);
            writerJson.Flush();
            writerJson.Close();
        }


       
        public void EditingContactDetails()
        {
            ContactPersonInformation contact = new ContactPersonInformation();
       
        addingDetailsAgainForEditing: Console.WriteLine("Please help us, first identify you");
            Console.WriteLine("Please enter your first name and phone no");
            string firstNm = Console.ReadLine();
            int editCheck = 0;
            double mobileNo = Convert.ToDouble(Console.ReadLine());
            foreach (ContactDetails contactDetails in contactDetailsList)
            {
                
                if (contactDetails.firstName == firstNm && contactDetails.phoneNo == mobileNo)
                {
                
                EditAgain: Console.WriteLine("please select the serial no. of field which you want to change\n 1. First name \n2.Last name\n3.Address\n4.City\n5.State\n6.Zip code\n7.Phone no.\n 8.email");
                    int inputForEditing = Convert.ToInt32(Console.ReadLine());
                    editCheck++;
                    switch (inputForEditing)
                    {
                        case 1:
                        firstname: Console.WriteLine("please enter the first name");
                            string newFirstName = Console.ReadLine();
                            if (contactDetails.firstName == newFirstName)
                            {
                                Console.WriteLine("You entered same user name");
                                Console.WriteLine("Please enter details again");
                                goto firstname;
                            }
                            //details are edited
                            contactDetails.firstName = newFirstName;
                            nLog.LogDebug("Debug successful, firstname successfully changed : EditingContactDetails()");
                            Console.WriteLine("Do you want to update anything else, press y to update again,else press enter");
                            string inputAfterFirstName = Console.ReadLine();
                            if (inputAfterFirstName.ToLower() == "y")
                            {
                                nLog.LogInfo("User wants to update further information");
                                goto EditAgain;
                            }
                            else
                            {
                                nLog.LogInfo("Updation Process Completed : EditinContactDetails()");
                                break;
                            }
                        case 2:
                        lastname: Console.WriteLine("please enter the last name");
                            string newlastName = Console.ReadLine();
                            if (contactDetails.lastName == newlastName)
                            {
                                Console.WriteLine("You entered same user name");
                                Console.WriteLine("Please enter details again");
                                goto lastname;
                            }
                            contactDetails.lastName = newlastName;
                            nLog.LogDebug("Debug successful, lastname successfully changed : EditingContactDetails()");
                            Console.WriteLine("Do you want to update anything else, press y to update again,else press enter");
                            string inputAfterlastName = Console.ReadLine();
                            if (inputAfterlastName.ToLower() == "y")
                            {
                                nLog.LogInfo("User wants to update further information");
                                goto EditAgain;
                            }
                            else
                            {
                                nLog.LogInfo("Updation Process Completed : EditinContactDetails()");
                                break;
                            }
                        case 3:
                        address: Console.WriteLine("please enter the address");
                            string newaddress = Console.ReadLine();
                            if (contactDetails.address == newaddress)
                            {
                                Console.WriteLine("You entered same user name");
                                Console.WriteLine("Please enter details again");
                                goto address;
                            }
                            contactDetails.address = newaddress;
                            nLog.LogDebug("Debug successful, address successfully changed : EditingContactDetails()");
                            Console.WriteLine("Do you want to update anything else, press y to update again,else press enter");
                            string inputAfteraddress = Console.ReadLine();
                            if (inputAfteraddress.ToLower() == "y")
                            {
                                nLog.LogInfo("User wants to update further information");
                                goto EditAgain;
                            }
                            else
                            {
                                nLog.LogInfo("Updation Process Completed : EditinContactDetails()");
                                break;
                            }
                        case 4:
                        city: Console.WriteLine("please enter the city");
                            string newcity = Console.ReadLine();
                            if (contactDetails.city == newcity)
                            {
                                Console.WriteLine("You entered same detail");
                                Console.WriteLine("Please enter details again");
                                goto city;
                            }
                            contactDetails.city = newcity;
                            nLog.LogDebug("Debug successful, city successfully changed : EditingContactDetails()");
                            Console.WriteLine("Do you want to update anything else, press y to update again,else press enter");
                            string inputAftercity = Console.ReadLine();
                            if (inputAftercity.ToLower() == "y")
                            {
                                nLog.LogInfo("User wants to update further information");
                                goto EditAgain;
                            }
                            else
                            {
                                nLog.LogInfo("Updation Process Completed : EditinContactDetails()");
                                break;
                            }
                        case 5:
                        state: Console.WriteLine("please enter the state");
                            string newstate = Console.ReadLine();
                            if (contactDetails.state == newstate)
                            {
                                Console.WriteLine("You entered same detail");
                                Console.WriteLine("Please enter details again");
                                goto state;
                            }
                            contactDetails.state = newstate;
                            nLog.LogDebug("Debug successful, state successfully changed : EditingContactDetails()");
                            Console.WriteLine("Do you want to update anything else, press y to update again,else press enter");
                            string inputAfterstate = Console.ReadLine();
                            if (inputAfterstate.ToLower() == "y")
                            {
                                nLog.LogInfo("User wants to update further information");
                                goto EditAgain;
                            }
                            else
                            {
                                nLog.LogInfo("Updation Process Completed : EditinContactDetails()");
                                break;
                            }
                        case 6:
                        zip: Console.WriteLine("please enter the zip code");
                            int newzip = Convert.ToInt32(Console.ReadLine());
                            if (contactDetails.zip == newzip)
                            {
                                Console.WriteLine("You entered same detail");
                                Console.WriteLine("Please enter details again");
                                goto zip;
                            }
                            contactDetails.zip = newzip;
                            nLog.LogDebug("Debug successful, zip successfully changed : EditingContactDetails()");
                            Console.WriteLine("Do you want to update anything else, press y to update again,else press enter");
                            string inputAfterzip = Console.ReadLine();
                            if (inputAfterzip.ToLower() == "y")
                            {
                                nLog.LogInfo("User wants to update further information");
                                goto EditAgain;
                            }
                            else
                            {
                                nLog.LogInfo("Updation Process Completed : EditinContactDetails()");
                                break;
                            }
                        case 7:
                        phoneno: Console.WriteLine("please enter the zip code");
                            double newmobileno = Convert.ToDouble(Console.ReadLine());
                            if (contactDetails.phoneNo == newmobileno || newmobileno <= 200000)
                            {
                                Console.WriteLine("You either entered same details or wrong details");
                                Console.WriteLine("Please enter details again");
                                goto phoneno;
                            }
                            contactDetails.phoneNo = newmobileno;
                            nLog.LogDebug("Debug successful, phoneno successfully changed : EditingContactDetails()");
                            Console.WriteLine("Do you want to update anything else, press y to update again,else press enter");
                            string inputAfterphone = Console.ReadLine();
                            if (inputAfterphone.ToLower() == "y")
                            {
                                nLog.LogInfo("User wants to update further information");
                                goto EditAgain;
                            }
                            else
                            {
                                nLog.LogInfo("Updation Process Completed : EditinContactDetails()");
                                break;
                            }
                        case 8:
                        email: Console.WriteLine("please enter the email code");
                            string newemail = Console.ReadLine();
                            if (contactDetails.eMail == newemail)
                            {
                                Console.WriteLine("You entered same detail");
                                Console.WriteLine("Please enter details again");
                                goto email;
                            }
                            contactDetails.eMail = newemail;
                            nLog.LogDebug("Debug successful, email successfully changed : EditingContactDetails()");
                            Console.WriteLine("Do you want to update anything else, press y to update again,else press enter");
                            string inputAfteremail = Console.ReadLine();
                            if (inputAfteremail.ToLower() == "y")
                            {
                                nLog.LogInfo("User wants to update further information");
                                goto EditAgain;
                            }
                            else
                            {
                                nLog.LogInfo("Updation Process Completed : EditinContactDetails()");
                                break;
                            }
                        default:
                            Console.WriteLine("Wrong input entered");
                            Console.WriteLine("Do you want to input again");
                            string input = Console.ReadLine();
                            if (input.ToLower() == "y")
                            {
                                goto EditAgain;
                            }
                            else
                                break;

                    }
                }

                

            }
            
            if (editCheck == 0)
            {
                Console.WriteLine("It looks like you are entering wrong details");
                nLog.LogError("Wrong details entered for editing");
                nLog.LogInfo("Nothing has been edited");
            }
            Console.WriteLine("Do you want to input again, press y to input details");

            string check = Console.ReadLine();
            if (check.ToLower() == "y")
            {
                nLog.LogInfo("details are being entered again by user");
                goto addingDetailsAgainForEditing;
            }
        }
        
        public void DeletingContactDetails()
        {
        addingDetailsForDeleting: Console.WriteLine("Please help us, first identify you");
            Console.WriteLine("Please enter your first name and phone no");
            string firstNm = Console.ReadLine();
            double mobileNo = Convert.ToDouble(Console.ReadLine());
            int index = 0;
            foreach (ContactDetails contactDetails in contactDetailsList)
            {
                if (contactDetails.firstName == firstNm && contactDetails.phoneNo == mobileNo)
                {
                    //removing selected object of contact details from contact details list
                    contactDetailsList.Remove(contactDetails);
                    Console.WriteLine("deletion operation successful");
                    nLog.LogDebug("Deletion Operation Successful:DeletingContactDetails()");
                    index++;
                    break;
                }

            }
            //Console.WriteLine("Your input does not match our data");
            if (index == 0)
            {
                Console.WriteLine("It looks like you are entering wrong details");
                nLog.LogError("Wrong details entered for deleting");
                nLog.LogInfo("Nothing has been deleted");
            }
            Console.WriteLine("Do you want to input again, press y to input details");

            string check = Console.ReadLine();
            if (check.ToLower() == "y")
            {
                nLog.LogInfo("details are being entered again by user:DeletingContactDetails()");
                goto addingDetailsForDeleting;
            }
            else
            {
                nLog.LogInfo("Process Completed");
            }

        }
      
        public bool CheckingForNameinExistingContactList(List<ContactDetails> contactDetailsList, string firstName, string lastName)
        {
            foreach (ContactDetails contactDetail in contactDetailsList)
            {
                if (firstName.Equals(contactDetail.firstName) && lastName.Equals(contactDetail.lastName))
                //if (contactDetail.firstName == firstName && contactDetail.lastName == lastName && contactDetail.address == address && contactDetail.city == city && contactDetail.state == state && contactDetail.zip == zip && contactDetail.phoneNo == phoneNo && contactDetail.eMail == eMail)
                {
                    //if same contact details are entered, than details are entered again
                    nLog.LogError("Contact details have already been entered");
                    Console.WriteLine("Contact details have already been entered \n please add new contact details");
                    return false;
                }

            }
            return true;
        }
        
        public void SearchingContactDetailsByCity(string searchCity)
        {
            //used to check if city exist and increments the index. If index=0, exception is thrown
            int index = 0;
            foreach (ContactDetails contactPerson in contactDetailsList)
            {
                //checks if city is there in list
                if (contactPerson.city.Equals(searchCity))
                {
                    Console.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
                    index++;
                }
            }
            if (index == 0)
            {
                //custom exception is thrown when city is not in list
                Console.WriteLine("there is no state in this list with this name");
            }

        }
        
        public void SearchingContactDetailsByState(string searchState)
        {
            
            int index = 0;
            foreach (ContactDetails contactPerson in contactDetailsList)
            {
                if (contactPerson.state.Equals(searchState))
                {
                    
                    Console.WriteLine($"First Name : {contactPerson.firstName} || Last Name: {contactPerson.lastName} || Address: {contactPerson.address} || City: {contactPerson.city} || State: {contactPerson.state}|| zip: {contactPerson.zip} || Phone No: {contactPerson.phoneNo} || eMail: {contactPerson.eMail}");
                    index++;
                }
            }
            if (index == 0)
            {
                
                Console.WriteLine("there is no state in this list with this name");
            }
        }
        
        public List<ContactDetails> AddingContactDetailsByCity(string cityName, List<ContactDetails> cityDetailsList)
        {
            
            foreach (ContactDetails contactPerson in contactDetailsList)
            {
                
                if (contactPerson.city.Equals(cityName))
                {
                    cityDetailsList.Add(contactPerson);
                }
            }
            return cityDetailsList;
        }
    
        public HashSet<string> GettingCityList()
        {
           
            foreach (ContactDetails contactPerson in contactDetailsList)
            {
                cityList.Add(contactPerson.city);
            }
            return cityList;
        }
        
        public List<ContactDetails> AddingContactDetailsByState(string stateName, List<ContactDetails> stateDetailsList)
        {
            
            foreach (ContactDetails contactPerson in contactDetailsList)
            {
                
                if (contactPerson.state.Equals(stateName))
                {
                    stateDetailsList.Add(contactPerson);
                }
            }
            return stateDetailsList;
        }
       
        public HashSet<string> GettingStateList()
        {
            
            foreach (ContactDetails contactPerson in contactDetailsList)
            {
                stateList.Add(contactPerson.state);
            }
            return stateList;
        }
        public List<ContactDetails> SortingContactDetails()
        {
            Console.WriteLine("Please press 1 to sort the data by name");
            Console.WriteLine("Please press 2 to sort the data by city");
            Console.WriteLine("Please press 3 to sort the data by state");
            Console.WriteLine("Please press 4 to sort the data by zip");
            Console.WriteLine("Please press any other to return the unsorted contacts");
            int sortingContacts = Convert.ToInt32(Console.ReadLine());
            switch (sortingContacts)
            {
                case 1:
                    contactDetailsList.Sort((emp1, emp2) => emp1.firstName.CompareTo(emp2.firstName));
                    contactDetailsList.Sort((emp1, emp2) => emp1.lastName.CompareTo(emp2.lastName));
                    return contactDetailsList;
                case 2:
                    contactDetailsList.Sort((emp1, emp2) => emp1.city.CompareTo(emp2.city));
                    return contactDetailsList;
                case 3:
                    contactDetailsList.Sort((emp1, emp2) => emp1.state.CompareTo(emp2.state));
                    return contactDetailsList;
                case 4:
                    contactDetailsList.Sort((emp1, emp2) => emp1.zip.CompareTo(emp2.zip));
                    return contactDetailsList;
                default:
                    return contactDetailsList;
            }

        }
    }

}