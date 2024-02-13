using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    /// <summary>
    /// This class contains a static method responsible for seeding user data into the database.
    /// </summary>
    public static class Seed
    {
        /// <summary>
        /// Seeds user data into the database if no users exist.
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public static async Task SeedUsers(UserManager<User> userManager)
    {
      if (!userManager.Users.Any())
      {
        var usersToSeed = JsonSerializer.Deserialize<List<User>>(File.ReadAllText("Data/Users.json"));

        foreach (var user in usersToSeed)
        {
          user.DateOfBirth = DateTime.SpecifyKind(user.DateOfBirth, DateTimeKind.Utc);
          await userManager.CreateAsync(user, "Passw0rd!");
          
        }

      }

      
    }
    }
}