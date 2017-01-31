using System.ComponentModel.DataAnnotations;
namespace notcreepy.Models
{
 public class Challenge : BaseEntity
 {
  [Key]
  public long id { get; set; }
  [Required]
  [MinLength(3)]
  public string name { get; set; }
 }
}