using illumina_Coding_Assignment.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Xml;

namespace illumina_Coding_Assignment.Services
{
    public class ContactService
    {
        public List<Contact> _allContacts;
        private readonly string _contactsJsonPath;

        public ContactService(IWebHostEnvironment webHostEnvironment)
        {
            _contactsJsonPath = Path.Combine(webHostEnvironment.WebRootPath, "Data", "Contact.json");

            try
            {
                RefreshData();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"Error loading contacts: {ex.Message}");
                _allContacts = new List<Contact>();
            }
        }

        public List<(string Code, string Country)> CountryCodes { get; } = new List<(string, string)>
    {
        ("+65", "Singapore"),
        ("+60", "Malaysia"),
        ("+1", "United States"),
        ("+971", "United Arab Emirates"),
        ("+44", "United Kingdom"),
        ("+81", "Japan"),
        ("+86", "China"),
        ("+91", "India"),
        ("+49", "Germany"),
        ("+33", "France"),
    };

        public void RefreshData()
        {
            if (File.Exists(_contactsJsonPath))
            {
                string jsonContent = File.ReadAllText(_contactsJsonPath);
                _allContacts = JsonConvert.DeserializeObject<List<Contact>>(jsonContent);
            }
            else
            {
                _allContacts = new List<Contact>();
            }
        }


        // Gets a contact by its unique identifier.
        public List<Contact> GetContactById(Guid contactId)
        {
            if (contactId != Guid.Empty && _allContacts != null)
            {
                return _allContacts.Where(c => c.Id == contactId).ToList();
            }

            return new List<Contact>();
        }


        // Adds a new contact.
        public void AddContact(Contact contact)
        {
            if (contact != null)
            {
                _allContacts.Add(contact);
                SaveContactsToJson();
            }
        }

        // Deletes a contact by its unique identifier.
        public bool DeleteContact(Guid contactId)
        {
            Contact contactToDelete = _allContacts.FirstOrDefault(c => c.Id == contactId);

            if (contactToDelete != null)
            {
                _allContacts.Remove(contactToDelete);
                SaveContactsToJson();
                return true;
            }

            return false; // Contact not found
        }


        // Updates an existing contact.
        public bool UpdateContact(Contact updatedContact)
        {
            if (updatedContact != null)
            {
                int index = _allContacts.FindIndex(c => c.Id == updatedContact.Id);
                if (index != -1)
                {
                    _allContacts[index] = updatedContact;
                    SaveContactsToJson();
                    return true;
                }
            }

            return false; // Contact not found or updatedContact is null
        }


        private void SaveContactsToJson()
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(_allContacts);
                File.WriteAllText(_contactsJsonPath, jsonContent);

                RefreshData();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"Error saving contacts: {ex.Message}");
            }
        }
    }
}
