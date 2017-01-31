using System.ComponentModel.DataAnnotations;
namespace notcreepy.Models
{


    public class Submission : BaseEntity
    {
        public void submission()
        {
            upvotes = 0;
            downvotes = 0;

        }
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

        public int upvotes { get; set; }

        public int downvotes { get; set; }
    }

}