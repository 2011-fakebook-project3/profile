using Fakebook.Profile.Domain.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    public class DomainProfile
    {
        //[anything]@[anything].[anything]
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                Regex emailRegex = new Regex(RegularExpressions.EmailCharacters);
                Match m = emailRegex.Match(value);
                if (!m.Success)
                    throw new ArgumentException();
                _email = value;
            }
        }

        //should be a url, defualts to an image.
        public Uri ProfilePictureUrl { get; set; }

        public string Name
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }

        //A-Z, ', ., - only... probs
        private string _firstName;
        public string FirstName 
        {
            get { return _firstName; }
            set
            {
                Regex nameRegex = new Regex(RegularExpressions.NameCharacters);
                Match m = nameRegex.Match(value);
                if (!m.Success)
                    throw new ArgumentException();
                _firstName = value;
            }
        }

        private string _lastName;
        public string LastName 
        {
            get { return _lastName; }
            set
            {
                Regex nameRegex = new Regex(RegularExpressions.NameCharacters);
                Match m = nameRegex.Match(value);
                if (!m.Success)
                    throw new ArgumentException();
                _lastName = value;
            }
        }

        private string _phoneNumber;
        //any 10 digits, can have - or () spaces optional, or null
        public string PhoneNumber 
        {
            get { return _phoneNumber; }
            set
            {
                Regex phoneRegex = new Regex(RegularExpressions.PhoneNumberCharacters);
                Match m = phoneRegex.Match(value);
                if (!m.Success)
                    throw new ArgumentException();
                _phoneNumber = value;
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate 
        {
            get { return _birthDate; }
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException();
                _birthDate = value;
            }
        }

        //can be null, or reasonable text (sanitized so they don't get funky)
        public string Status { get; set; }

        public DomainProfile() { }

        public DomainProfile(string email)
        {
            this.Email = email;
            if (email == null)
            {
                throw new ArgumentNullException();            
            }
            
        }

        public DomainProfile(string email, string phoneNumber)
        {
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }
    }
}
