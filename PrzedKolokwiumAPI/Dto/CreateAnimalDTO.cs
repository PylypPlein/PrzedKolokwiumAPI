using System.ComponentModel.DataAnnotations;

namespace PrzedKolokwiumAPI.Dto;

public class CreateAnimalDTO
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public string CATEGORY { get; set; }
    public string AREA { get; set; }
}