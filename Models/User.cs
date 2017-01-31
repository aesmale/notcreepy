using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace notcreepy.Models
{
 public abstract class BaseEntity {}
 public class User : BaseEntity
 {
  [Key]
  public long Id { get; set; }
  [Required]
  [EmailAddress]
  public string Email { get; set; }
  [Required]
  public string Password { get; set; }

  public List<User> Followers { get; set; }

 }
}