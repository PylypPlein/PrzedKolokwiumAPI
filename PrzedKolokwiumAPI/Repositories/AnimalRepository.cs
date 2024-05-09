using System.Data.SqlClient;
using PrzedKolokwiumAPI.Models;

namespace PrzedKolokwiumAPI.Repositories;

public interface IAnimalRepository
{
    public IEnumerable<Animal> FetchAllAnimals(string orderBy);
    public bool CreateAnimal(string name, string description, string category, string area);
    public bool UpdateAnimal(int idAnimal, Animal animal);
    public Animal GetAnimalById(int idAnimal);
}
public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        var safeOrderBy = new string[] { "IdAnimal", "Name" }.Contains(orderBy) ? orderBy : "IdAnimal";
        using var command = new SqlCommand($"SELECT IdAnimal, Name FROM ANIMAL ORDER BY {safeOrderBy}", connection);
        using var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        while (reader.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!
            };
                animals.Add(animal);
        }

        return animals;
    }

    public bool CreateAnimal(string name, string description, string category, string area)
    {
        var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command =
            new SqlCommand(
                "INSERT INTO Animal (Name, Description,CATEGORY,AREA) VALUES  (@name,@description,@category,@area)",
                connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@area", area);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }

    public Animal GetAnimalById(int idAnimal)
    {
        var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using var command = new SqlCommand("SELECT IdAnimal, Name FROM Animal WHERE IdAnimal = @idAnimal", connection);
        command.Parameters.AddWithValue("@idAnimal", idAnimal);
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Animal()
            {
                IdAnimal = (int)reader["idAnimal"],
                Name = reader["Name"].ToString()!
            };
        }
        else
        {
            return null;
        }
        
    }

    public bool UpdateAnimal(int idAnimal, Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand("UPDATE Animal SET Name = @name , Description = @description , CATEGORY=@category,AREA=@area WHERE IdAnimal = @idAnimal", connection);
        command.Parameters.AddWithValue("@name", animal.Name);
        command.Parameters.AddWithValue("@description", animal.Description);
        command.Parameters.AddWithValue("@category", animal.CATEGORY);
        command.Parameters.AddWithValue("@area", animal.AREA);
        command.Parameters.AddWithValue("@idAnimal", idAnimal);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 4;
    }
}