using PrzedKolokwiumAPI.Dto;
using PrzedKolokwiumAPI.Models;
using PrzedKolokwiumAPI.Repositories;

namespace PrzedKolokwiumAPI.Services;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool AddAnimal(CreateAnimalDTO animalDto);
    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO animalDto);
}
public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return _animalRepository.FetchAllAnimals(orderBy);
    }

    public bool AddAnimal(CreateAnimalDTO animalDto)
    {
        return _animalRepository.CreateAnimal(animalDto.Name, animalDto.Description,animalDto.CATEGORY,animalDto.AREA);
    }

    public bool UpdateAnimal(int idAnimal, UpdateAnimalDTO updateAnimalDto)
    {
        var animalToUpDate = _animalRepository.GetAnimalById(idAnimal);
        if (animalToUpDate == null)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(updateAnimalDto.Name))
        {
            animalToUpDate.Name = updateAnimalDto.Name;
        }
        if (!string.IsNullOrEmpty(updateAnimalDto.Description))
        {
            animalToUpDate.Description = updateAnimalDto.Description;
        }

        if (!string.IsNullOrEmpty(updateAnimalDto.CATEGORY))
        {
            animalToUpDate.CATEGORY = updateAnimalDto.CATEGORY;
        }

        if (!string.IsNullOrEmpty(updateAnimalDto.AREA))
        {
            animalToUpDate.AREA = updateAnimalDto.AREA;
        }

        return _animalRepository.UpdateAnimal(idAnimal, animalToUpDate);
    }
}