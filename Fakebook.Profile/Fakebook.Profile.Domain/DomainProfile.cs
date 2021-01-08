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
            get => _email; 
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
        public Uri ProfilePictureUrl { get; set; } = new Uri("https://publicdomainvectors.org/photos/defaultprofile.png");

        public string Name => $"{FirstName} {LastName}";

        //A-Z, ', ., - only
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Invalid first name, {value}");
                }

                Regex nameRegex = new Regex(RegularExpressions.NameCharacters);
                // throw null exception if value is null
                Match m = nameRegex.Match(value);
                if (!m.Success)
                    throw new ArgumentException();
                _firstName = value;
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Invalid first name, {value}");
                }

                Regex nameRegex = new Regex(RegularExpressions.NameCharacters);
                Match m = nameRegex.Match(value);
                if (!m.Success)
                    throw new ArgumentException();
                _lastName = value;
            }
        }

        private string _phoneNumber;
        //any up to 15 digits, can have - or () spaces optional, or null
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {  
                if(value is not null)
                {
                    Regex phoneRegex = new Regex(RegularExpressions.PhoneNumberCharacters);
                    Match m = phoneRegex.Match(value);
                    if (!m.Success)
                        throw new ArgumentException();
                }
                
                _phoneNumber = value;

            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value.Date > DateTime.Now.Date) 
                    throw new ArgumentException();
                _birthDate = value;
            }
        }

        //can be null, or reasonable text (sanitized so they don't get funky)
        string _status;
        public string Status { 
            get => _status;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _status = null;
                }
                else
                {
                    //todo: check for funk, like js, html, and sql injection. Packages can help
                    _status = value;
                }
            }
        }

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
