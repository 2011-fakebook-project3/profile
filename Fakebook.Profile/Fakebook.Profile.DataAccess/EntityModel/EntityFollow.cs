using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    // back up plan just in case self-referencing table does not work
    public class EntityFollow
    {
        /*
        public EntityFollow() { }
        public EntityFollow(int id, int userId)
        {
            FolloweeId = id;
            FollowerId = userId;
        }
        */

        public string FolloweeEmail { get; set; }
        public string FollowerEmail { get; set; }

        // added virtual
        public virtual EntityProfile Followee { get; set; }
        public virtual EntityProfile Follower { get; set; }

    }
}