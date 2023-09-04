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
    public class SkillService : IModelService
    {
        public async Task<List<Skill>> PostSkillList(List<Stat> Stats)
        {
            Console.WriteLine(Stats[0].Score);
            List<Skill> Skills = new List<Skill>();
            foreach (string[] item in ConvertToStringList(await GetDataFromDB("*", "Skills")))
            {
                Skills.Add(new Skill(item[0], item[1], "Untrained", Stats.Find(x => x.Name.Contains(item[1])).Modifier));
            }
            return Skills;
        }
    }
}
