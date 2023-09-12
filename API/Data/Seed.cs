using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public static class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager)
    {
      if (!userManager.Users.Any())
      {
        var usersToSeed = JsonSerializer.Deserialize<List<User>>(File.ReadAllText("Data/Users.json"));

        foreach (var user in usersToSeed)
        {
          await userManager.CreateAsync(user, "Passw0rd!");
        //  if(user.UserName.ToLower()=="rebecca")
         // await userManager.AddToRoleAsync(user,"Admin"); 
         // else {await userManager.AddToRoleAsync(user,"User"); }
          
        }

      }

      
    }
    }
}