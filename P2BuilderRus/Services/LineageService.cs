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
    public class LineageService : IModelService
    {
        public async Task<List<string>> GetLineageList() => ConvertToString(await GetDataFromDB("Name", "Backgrounds"));

        public async Task<Lineage> GetLineage(string LineageName)
        {
            Console.WriteLine(await GetDataFromDB("*", $"Backgrounds WHERE Name = '{LineageName}'"));
            return new Lineage(await GetDataFromDB("*", $"Backgrounds WHERE Name = '{LineageName}'"));
        }
        public async Task<List<string>[]> GetLineageAbBoosts(string LineageName)
        {
            List<string>[] model = new List<string>[2];
            model[0] = (await GetDataFromDB("[Ability Boost1]", $"Backgrounds WHERE Name = '{LineageName}'")).Rows[0][0].ToString().Split(',').ToList();
            model[1] = (await GetDataFromDB("[Ability Boost2]", $"Backgrounds WHERE Name = '{LineageName}'")).Rows[0][0].ToString().Split(',').ToList();
            return model;
        }
    }
}
