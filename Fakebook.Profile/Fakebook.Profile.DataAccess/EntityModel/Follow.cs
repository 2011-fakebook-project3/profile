using System.ComponentModel.DataAnnotations.Schema;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    public class Follow
    {
        [ForeignKey("Id")]
        public int UserId { get; set; }

        [ForeignKey("Id")]
        public int FollowingId { get; set; }

        public virtual EntityProfile User { get; set; }
        public virtual EntityProfile Following { get; set; }
    }
}
