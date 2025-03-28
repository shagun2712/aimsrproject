using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class BlogPost
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ImageUrl { get; set; }  // Ensure column name matches DB schema
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
