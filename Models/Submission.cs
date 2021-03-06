using System.ComponentModel.DataAnnotations;
namespace notcreepy.Models
{


    public class Submission : BaseEntity
    {
        [Key]
        public long id { get; set; }

        [Key]
        [RequiredAttribute]
        public long user_id { get; set; }

        [Key]
        [RequiredAttribute]
        public long challenge_id { get; set; }

        [RequiredAttribute]
        public string image { get; set; }

    }

}