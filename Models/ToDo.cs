using System.ComponentModel.DataAnnotations;
namespace notcreepy.Models
{


    public class ToDo : BaseEntity
    {
        [Key]
        public long id { get; set; }

        [Key]
        [RequiredAttribute]
        public long challenge_id { get; set; }

        [Key]
        [RequiredAttribute]
        public long challenger_id { get; set; }

        [Key]
        [RequiredAttribute]
        public long challengee_id { get; set; }

    }

}