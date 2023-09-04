using P2BuiderRus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;

namespace P2BuilderRus.Services
{
    public class RaceService : IModelService
    {
        public async Task<List<string>> GetRaceList() => ConvertToString(await GetDataFromDB("Name", "Races"));

        public async Task<Race> GetRace(string RaceName)
        {
            Console.WriteLine(await GetDataFromDB("*", $"Races WHERE Name = '{RaceName}'"));
            return new Race(await GetDataFromDB("*", $"Races WHERE Name = '{RaceName}'"));
        }
        public async Task<List<string>[]> GetRaceAbBoosts(string RaceName)
        {
            List<string>[] model = new List<string>[2];
            model[0] = (await GetDataFromDB("[Ability Boosts]", $"Races WHERE Name = '{RaceName}'")).Rows[0][0].ToString().Split(',').ToList();
            model[1] = (await GetDataFromDB("[Ability Flaws]", $"Races WHERE Name = '{RaceName}'")).Rows[0][0].ToString().Split(',').ToList();
            return model;
        }
    }
}
