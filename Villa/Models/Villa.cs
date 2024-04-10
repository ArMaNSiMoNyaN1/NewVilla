using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Villa.Models;

public class Villa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    public string Name { get; set; }

    public string Details { get; set; }

    public double Rate { get; set; }

    public int Square { get; set; }

    public string ImageUrl { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdateDate { get; set; }
}