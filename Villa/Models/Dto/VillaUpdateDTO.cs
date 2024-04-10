using System.ComponentModel.DataAnnotations;

namespace Villa.Models.Dto;

public class VillaUpdateDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    public string Details { get; set; }

    [Required]
    public double Rate { get; set; }
    [Required]
    public int Square { get; set; }
    [Required]
    public string ImageUrl { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdateDate { get; set; }
}