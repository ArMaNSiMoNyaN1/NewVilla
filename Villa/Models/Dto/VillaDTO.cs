using System.ComponentModel.DataAnnotations;

namespace Villa.Models.Dto;

public class VillaDTO
{
    public int Id { get; set; }
    
    [Required] [MaxLength(30)] 
    public string name { get; set; }
    
    public string Details { get; set; }
    
    public double Rate { get; set; }
    public int Square { get; set; }
    
    public string ImageUrl{ get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public DateTime UpdateDate { get; set; }
}