using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace notcreepy.Models
{
    public class Followship : BaseEntity
    {
        [Key]
        public long followship_id { get; set; }

        [Key]
        [RequiredAttribute]
        public long followee_id { get; set; }

        [Key]
        [RequiredAttribute]
        public long follower_id { get; set; }

        public User followee { get; set; }

        public User follower { get; set; }
    }
}