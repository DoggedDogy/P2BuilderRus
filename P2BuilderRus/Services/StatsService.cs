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
    public class StatsService : IModelService
    {
        public async Task<List<Stat>> GetStatList() {
            List<Stat> Stats = new List<Stat>();
            foreach (string stat in new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" })
                Stats.Add(new Stat(stat, 10));
            return Stats;
        }
        public async Task<List<Stat>> PostStatUpdate(List<Stat> Stats, string name, int plus)
        {
            Stats.Find(x => name.Contains(x.Name)).Update(plus);
            return Stats;
        }
    }
}
