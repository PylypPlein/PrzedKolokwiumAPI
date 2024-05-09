using Microsoft.AspNetCore.Mvc;
using PrzedKolokwiumAPI.Dto;
using PrzedKolokwiumAPI.Services;

namespace PrzedKolokwiumAPI.Controllers;

[ApiController]
[Route("/api/animal")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllAnimals([FromQuery] string orderBy)
    {
        var animals = _animalService.GetAllAnimals(orderBy);
        return Ok(animals);
    }
    [HttpPost("")]
    public IActionResult AddAnimal([FromBody] CreateAnimalDTO animalDto)
    {
        var succes = _animalService.AddAnimal(animalDto);
        return succes ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }
    [HttpPut("{idAnimal}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateAnimal(int idAnimal, [FromBody] UpdateAnimalDTO updateAnimalDto)
    {
        var succes = _animalService.UpdateAnimal(idAnimal, updateAnimalDto);
        return succes ? StatusCode(StatusCodes.Status200OK) : Conflict();
    }
}