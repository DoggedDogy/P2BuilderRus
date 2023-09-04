using P2BuilderRus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2BuiderRus.Model
{
    public class Character
    {
        SkillService SS = new SkillService();
        ClassService CS;
        RaceService RS;
        LineageService LS;
        public string Name { get; set; }
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
        public Character(string name, ClassModel clas, Race race, Lineage lineage)
        {
            Name = name;
            Class = clas;
            Race = race;
            Lineage = lineage;
            Stats = new List<Stat>();
            Skills = new List<Skill>();
            SavingThrows = new List<SavingThrow>();
            Level = 1;
            foreach (string stat in new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" })
                Stats.Add(new Stat(stat, 10));
            SavingThrows.Add(new SavingThrow("Fortitude", "Constitution", "Untrained"));
            SavingThrows.Add(new SavingThrow("Reflex", "Dexterity", "Untrained"));
            //SavingThrows.Add(new SavingThrow("Will", "Wisdom", "Untrained"));
            //foreach (string[] skill in SS.GetSkillList().Result)
            //    Skills.Add(new Skill(skill[0], skill[1], "Untrained"));
            HP = 0;
            ArmorClass = 10 + Stats[1].Modifier;
            Speed = 0;
            ClassSave = 0;
        }
        public Character()
        {
            CS = new ClassService();
            RS = new RaceService();
            LS = new LineageService();
            Name = "";
            Class = CS.GetClassModel("Wizard").Result;
            Race = RS.GetRace("Human").Result;
            Lineage = LS.GetLineage("Guard").Result;
            Level = 1;
            Stats = new List<Stat>();
            Skills = new List<Skill>();
            SavingThrows = new List<SavingThrow>();
            foreach (string stat in new string[] { "Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma" })
                Stats.Add(new Stat(stat, 10));
            SavingThrows.Add(new SavingThrow("Fortitude", "Constitution", "Untrained"));
            SavingThrows.Add(new SavingThrow("Reflex", "Dexterity", "Untrained"));
            SavingThrows.Add(new SavingThrow("Will", "Wisdom", "Untrained"));
            //foreach (string[] skill in SS.GetSkillList().Result)
            //    Skills.Add(new Skill(skill[0], skill[1], "Untrained"));
        }
        public void Update()
        {
            HP = Class.HP + Race.HP + Stats[2].Modifier;
            ArmorClass = 10 + Stats[1].Modifier;
            foreach (SavingThrow save in SavingThrows)
            {
                save.Modifier = Stats.Find(delegate (Stat i) { return i.Name == save.BasicCharacteristic;}).Modifier;
            }
            //foreach (Skill skill in Skills)
            //{
            //    skill.Modifier = Stats.Find(delegate (Stat i) { return i.Name == skill.BasicCharacteristic; }).Modifier;
            //}
            ClassSave = 10 + Stats.Find(delegate (Stat i) { return i.Name == KeyAbility; }).Modifier;
            Speed = Race.Speed;
        }
        public void UpdateTrainings()
        {
            
        }
    }
}
