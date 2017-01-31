using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace notcreepy.Models
{
 public abstract class BaseEntity {}
 public class User : BaseEntity
 {
  [Key]
  public long id { get; set; }
  [RequiredAttribute]
  [MinLengthAttribute(5)]
  public string username {get; set;}
  [Required]
  [EmailAddress]
  public string email { get; set; }
  [Required]
  [MinLengthAttribute(8)]
  public string password { get; set; }
 }
}