using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace notcreepy.Models
{
 public class Relationship : BaseEntity
 {
  //  public void relationship(){
  //    folowee = UserRepository.FindById(followee_id);
  //    follower = UserRepository.FindById(follower_id);
  //  }
    
[Key]
public long relationship_id {get;set;}

  [Key]
  [RequiredAttribute]
  public long followee_id { get; set; }

  [Key]
  [RequiredAttribute]
  public long follower_id { get; set;}

  public User followee {get; set;}

  public User follower {get; set;}
 }
}