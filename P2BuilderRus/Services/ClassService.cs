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
    public class ClassService : IModelService
    {
        public async Task<List<string>> GetClassModelList() => ConvertToString(await GetDataFromDB("Name", "Classes"));

        public async Task<ClassModel> GetClassModel(string ClassName)
        {
            //Console.WriteLine(GetDataFromDB("*", $"Classes WHERE Name = '{ClassName}'").Result.Rows[0][0] + " service");
            return new ClassModel(await GetDataFromDB("*", $"Classes WHERE Name = '{ClassName}'"));
        }
        public async Task<List<string>> GetClassAbBoosts(string ClassName)
        {
            List<string> model = new List<string>();
            model = (await GetDataFromDB("[Key ability]", $"Classes WHERE Name = '{ClassName}'")).Rows[0][0].ToString().Split(',').ToList();
            return model;
        }
    }
}
