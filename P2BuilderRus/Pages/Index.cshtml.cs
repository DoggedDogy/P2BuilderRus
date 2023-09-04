using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using P2BuiderRus.Model;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using P2BuilderRus.Services;
using System.Text.Json;

namespace P2BuilderRus.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class IndexModel : PageModel
    {
        // Class properties
        public string CharacterName { get; set; }
        public string ClassName { get; set; }
        public string RaceName { get; set; }
        public string LineageName { get; set; }
        public List<string> ClassList = new List<string>();
        public List<string> RaceList = new List<string>();
        public List<string> LineageList = new List<string>();
        ClassService CS;
        RaceService RS;
        LineageService LS;
        SkillService SS;
        Character Blank;
        StatsService _StatsService;

        public ClassModel Class { get; set; }
        public Race Race { get; set; }
        public Lineage Lineage { get; set; }
        public int Level { get; set; }
        private string KeyAbility { get; set; }
        public List<Stat> Stats { get; set; }
        public List<Skill> Skills { get; set; }
        public int HP { get; set; }
        public int ArmorClass { get; set; }
        public int Speed { get; set; }
        public List<SavingThrow> SavingThrows { get; set; }
        public int ClassSave { get; set; }

        public IndexModel()
        {
            CS = new ClassService();
            RS = new RaceService();
            LS = new LineageService();
            SS = new SkillService();
            _StatsService = new StatsService();
            Stats = new List<Stat>();
            Skills = new List<Skill>();
            SavingThrows = new List<SavingThrow>();
            Level = 1;
            foreach (string stat in new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" })
                Stats.Add(new Stat(stat, 10));
            SavingThrows.Add(new SavingThrow("Fortitude", "Constitution", "Untrained"));
            SavingThrows.Add(new SavingThrow("Reflex", "Dexterity", "Untrained"));
            SavingThrows.Add(new SavingThrow("Will", "Wisdom", "Untrained"));
            //foreach (string[] skill in SS.GetSkillList().Result)
            //    Skills.Add(new Skill(skill[0], skill[1], "Untrained"));
            HP = 0;
            //ArmorClass = 10 + Stats[1].Modifier;
            Speed = 0;
            ClassSave = 0;
            //if (!Program.T)
            //{

            //Blank = new Character(CharacterName, CS.GetClassModel("Fighter").Result, RS.GetRace("Human").Result, LS.GetLineage("Guard").Result);
            //}
            //Program.T = true;
        }
        private void OnPostCharacterUpdate()
        {
            
        }
        // Helper function to get data from the excel files
        private DataTable GetDataFromExcel(string ConnectionString, string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        // Razor page functions
        public async void OnGet()
        {
            Class = await CS.GetClassModel("Fighter");
            Race = await RS.GetRace("Human");
            Lineage = await LS.GetLineage("Guard");

            ClassList = CS.GetClassModelList().Result;
            RaceList = RS.GetRaceList().Result;
            LineageList = LS.GetLineageList().Result;
        }
        //public async void OnGetStatListAsync()
        //{
        //    _StatsService.GetStatList(Stats);
        //}
        public async Task<PartialViewResult> OnGetClassModelAsync(string name)
        {
            return Partial("_ClassModelPartial", await CS.GetClassModel(name));
        }
        public async Task<PartialViewResult> OnGetRaceAsync(string name)
        {
            return Partial("_RacePartial", await RS.GetRace(name));
        }
        public async Task<PartialViewResult> OnGetLineageAsync(string name)
        {
            return Partial("_LineagePartial", await LS.GetLineage(name));
        }
        public async Task<PartialViewResult> OnGetClassBoostsAsync(string name)
        {
            return Partial("_ClassAbBoostPartial", await CS.GetClassAbBoosts(name));
        }
        public async Task<PartialViewResult> OnGetRaceBoostsAsync(string name)
        {
            return Partial("_RaceAbBoostPartial", await RS.GetRaceAbBoosts(name));
        }
        public async Task<PartialViewResult> OnGetLineageBoostsAsync(string name)
        {
            return Partial("_LineageAbBoostPartial", await LS.GetLineageAbBoosts(name));
        }
        public async Task<PartialViewResult> OnGetStatListAsync()
        {
            return Partial("_AbBoostPartial", Stats = await _StatsService.GetStatList());
        }
        public async Task<PartialViewResult> OnPostSkillListAsync(int[] ints)
        {

            int i = 0;
            List<Stat> Statsy = new List<Stat>();
            foreach (var stat in ints.Zip(new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" }, (i, s) => new { In = i, St = s }))
            {
                Statsy.Add(new Stat(stat.St, stat.In));
                Console.WriteLine(Statsy[i].Name + " " + Statsy[i].Score);
                i++;
            }
            return Partial("_SkillsPartial", await SS.PostSkillList(Statsy));
        }
        public async Task<PartialViewResult> OnPostSkillBoostAsync(int[] ints)
        {

            int i = 0;
            List<Stat> Statsy = new List<Stat>();
            foreach (var stat in ints.Zip(new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" }, (i, s) => new { In = i, St = s }))
            {
                Statsy.Add(new Stat(stat.St, stat.In));
                Console.WriteLine(Statsy[i].Name + " " + Statsy[i].Score);
                i++;
            }
            return Partial("_SkillBoostsPartial", await SS.PostSkillList(Statsy));
        }
        public async Task<PartialViewResult> OnPostStatsAsync(int[] ints)
        {
            int i = 0;
            List<Stat> Statsy = new List<Stat>();
            foreach (var stat in ints.Zip(new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" }, (i, s) => new { In = i, St = s }))
            {
                Statsy.Add(new Stat(stat.St, stat.In));
                i++;
            }
            return Partial("_StatsPartial", Statsy);
        }
        public async Task<JsonResult> OnPostClassModel(string name)
        {
            //Console.WriteLine(name + " help");
            Class = await CS.GetClassModel(name);
            ClassName = Class.ClassName;
            Console.WriteLine(ClassName + " Class");
            return new JsonResult ( new { result = ClassName } );
        }
        public async Task<JsonResult> OnPostRace(string name)
        {
            //Console.WriteLine(name + " help");
            Race = await RS.GetRace(name);
            RaceName = Race.Name;
            return new JsonResult(new { result = RaceName });
        }
        public async Task<JsonResult> OnPostLineage(string name)
        {
            //Console.WriteLine(name + " help");
            Lineage = await LS.GetLineage(name);
            LineageName = Lineage.Name;
            return new JsonResult(new { result = LineageName });
        }
        public async Task<JsonResult> OnPostName(string name)
        {
            //Console.WriteLine(name + " help");
            CharacterName = name;
            return new JsonResult(new { result = CharacterName });
        }
        public async Task<PartialViewResult> OnPostStatUpdateAsync(int[] ints, string name, int plus)
        {
            int i = 0;
            List<Stat> Statsy = new List<Stat>();
            Console.WriteLine(ints + " " + name + " " + plus + " help");
            foreach (var stat in ints.Zip(new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" }, (i,s) => new { In = i, St = s }))
            {
                Statsy.Add(new Stat(stat.St, stat.In));
                Console.WriteLine(Statsy[i].Name + " " + Statsy[i].Score);
                i++;
            }
            return Partial("_AbBoostPartial", await _StatsService.PostStatUpdate(Statsy, name, plus));
        }

        public void OnPost()
        {
            // Get data from form
            //CharacterName = Request.Form["CharacterName"];
            //ClassName = Request.Form["nameClass"];
            //RaceName = Request.Form["nameRace"];
            //LineageName = Request.Form["nameLineage"];


            // Get data from excel files
            //string ConnectionString = @"Data Source=WIN-4DHE83OFTCF;Initial Catalog=Pf;Trusted_Connection=true";
            //string classQuery = $"SELECT * FROM Classes WHERE Name='{ClassModelName}'";
            //DataTable classData = GetDataFromExcel(ConnectionString, classQuery);
            //DataRow classRow = classData.Rows[0];

            //string raceQuery = $"SELECT * FROM Race WHERE Name='{RaceName}'";
            //DataTable raceData = GetDataFromExcel(ConnectionString, raceQuery);
            //DataRow raceRow = raceData.Rows[0];

            //string lineageQuery = $"SELECT * FROM Backgrounds WHERE Name='{LineageName}'";
            //DataTable lineageData = GetDataFromExcel(ConnectionString, lineageQuery);
            //DataRow lineageRow = lineageData.Rows[0];

            //string ClassAbbBostQuery = $"SELECT [Key Ability 1] AS [Key Ability] FROM Classes WHERE Name='{ClassName}' UNION SELECT [Key Ability 2] AS [Key Ability] FROM Classes WHERE Name='{ClassName}'";
            //DataTable ClassAbbBostData = GetDataFromExcel(ConnectionString, classQuery);
            //DataRow ClassAbbBostRow = ClassAbbBostData.Rows[0];

            // Set CharacterLevel to 1 for now, will add leveling up later
            //CharacterLevel = Convert.ToInt32(Request.Form["CharacterLevel"]);
            //foreach (string stat in new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" })
            //{
            //    int i = 0;
            //    i++;
            //}
            //ViewData["ClassAbilityBost"] = ClassAbbBostData.AsEnumerable().Select(row => new SelectListItem
            //{
            //    Text = "Key Ability",
            //    Value = "Key Ability",
            //}).ToList();
            //foreach (string save in new string[] { "Fortitude", "Reflex", "Will" })
            //{
            //    Saves[save] = Convert.ToInt32(classRow.Field<string>(save));
            //}
            //ClassSave = Convert.ToInt32(classRow.Field<string>("ClassSave"));

            // Update race data
            //foreach (string stat in new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" })
            //{
            //    if (classRow.Field<string>(stat) != null)
            //        Stats[stat] += 2;
            //}
            //HP += Convert.ToInt32(raceRow.Field<string>("Hp"));

            //ArmorClass = 10 + (int)Math.Floor((double)(Stats["Dexterity"] - 10) / 2);

            // Set Speed based on race and lineage
            //Speed = raceRow.Field<int>("BaseSpeed");

            // Set Saves based on class and stats
            //Saves["Fortitude"] = classRow.Field<int>("Fortitude") + (int)Math.Floor((double)(Stats["Constitution"] - 10) / 2);
            //Saves["Reflex"] = classRow.Field<int>("Reflex") + (int)Math.Floor((double)(Stats["Dexterity"] - 10) / 2);
            //Saves["Will"] = classRow.Field<int>("Will") + (int)Math.Floor((double)(Stats["Wisdom"] - 10) / 2);

            // Set ClassSave based on class
            //ClassSave = 10 + Stats[classRow.Field<string>("ClassSave")];
        }
        //public void OnGet()
        //{
            // Get data from excel files
            // string ConnectionString = @"Data Source=WIN-4DHE83OFTCF;Initial Catalog=Pf;Trusted_Connection=true";

            // string classQuery = "SELECT * FROM Classes";
            // DataTable classData = GetDataFromExcel(ConnectionString, classQuery);

            // string raceQuery = "SELECT * FROM Races";
            // DataTable raceData = GetDataFromExcel(ConnectionString, raceQuery);

            // string lineageQuery = "SELECT * FROM Backgrounds";
            // DataTable lineageData = GetDataFromExcel(ConnectionString, lineageQuery);

            // string skillQuery = "SELECT * FROM Skills";
            // DataTable skilltData = GetDataFromExcel(ConnectionString, skillQuery);

            // ViewData["ClassName"] = ClassName;
            // ViewData["RaceName"] = RaceName;
            // ViewData["LineageName"] = LineageName;
            // Set ViewData for dropdowns
            //ViewData["Classes"] = classData.AsEnumerable().Select(row => new SelectListItem
            //{
            //    Text = row.Field<string>("Name"),
            //    Value = row.Field<string>("Name")
            //}).ToList();
            //ViewData["Races"] = raceData.AsEnumerable().Select(row => new SelectListItem
            //{
            //    Text = row.Field<string>("Name"),
            //    Value = row.Field<string>("Name")
            //}).ToList();

            //ViewData["Lineages"] = lineageData.AsEnumerable().Select(row =>
            //new SelectListItem
            //{
            //    Text = row.Field<string>("Name"),
            //    Value = row.Field<string>("Name")
            //}).ToList();
        //}
    }
}
