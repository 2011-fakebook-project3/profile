using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    public class DomainProfile
    {
        //[anything]@[anything].[anything]
        public string Email { get; set; }


        //should be a url
        //defaults to a default image.
        public Uri ProfilePictureUrl { get; set; }

        public string Name { get => $"{FirstName} {LastName}"; }

        //A-Z, ', ., - only... probs
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //any 10 digits, can have - or () spaces optional, or null
        public string PhoneNumber { get; set; }

        //not future
        public DateTime BirthDate { get; set; }

        //can be null, or reasonable text (sanitized so they don't get funky)
        public string Status { get; set; }

        public DomainProfile() {}

        public DomainProfile(string email)
        {
            Email = email;
        }

        public DomainProfile(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
