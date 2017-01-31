using System.ComponentModel.DataAnnotations;
namespace notcreepy.Models
{


    public class Submission : BaseEntity
    {
        public void submission()
        {
            upvote = 0;
            downvote = 0;

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

        public int upvote { get; set; }

        public int downvote { get; set; }
    }

}