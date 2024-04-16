using System.ComponentModel.DataAnnotations;

namespace illumina_Coding_Assignment.Models
{
    public class Contact
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        public string UnitNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; }

        public Contact()
        {
            Id = Guid.NewGuid();
        }
    }
}
