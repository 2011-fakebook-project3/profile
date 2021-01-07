using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    public class EntityFollow
    {
        
         public EntityFollow() { }
         public EntityFollow(string followeeEmail, string followerEmail)
         {
            FolloweeEmail = followeeEmail;
            FollowerEmail = followerEmail;
         }
         

        public string FolloweeEmail { get; set; }
        public string FollowerEmail { get; set; }

        // added virtual
        public virtual EntityProfile Followee { get; set; }
        public virtual EntityProfile Follower { get; set; }

    }
}