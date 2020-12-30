using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    using System;
    public class User
    {
        // commmented out Post 
        public User()
        {
            //Posts = new List<Post>();
            Followers = new List<User>();
            Followees = new List<User>();
        }

        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Status { get; set; }
        public ICollection<User> Followees { get; set; }
        public ICollection<User> Followers { get; set; }
        //public ICollection<Post> Posts { get; set; }
    }


}
