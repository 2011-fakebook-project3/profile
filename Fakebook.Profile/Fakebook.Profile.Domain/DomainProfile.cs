using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    public class DomainProfile
    {

        public string Email { get; set; }
        public Uri ProfilePictureUrl { get; set; }

        public string Name
        {
            get
            {
                return FirstName+' '+LastName;
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Status { get; set; }


        public DomainProfile(string email)
        {
            this.Email = email;
        }
    }
}
