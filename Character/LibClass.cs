using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace LibClass
{
    class LibClass
    {
        static void Main(string[] args)
        {
            ChannelServices.RegisterChannel(new TcpClientChannel(), false);
            Character obj = (Character)Activator.GetObject(typeof(Character), "tcp://localhost:8086/Pr");
            if (obj == null)
            {
                Console.WriteLine("Сервер не доступен");
                return;
            }
            else
                Console.WriteLine("Сервер работает");
            Console.ReadLine();
        }
    }

    public class Character
    {
        public string Name { get; set; }
        public Class Class { get; set; }
        public Race Race { get; set; }
        public Lineage Lineage { get; set; }
        public int Level { get; set; }
        public List<Stat> Stats { get; set; }
        public List<Skill> Skills { get; set; }
        public int HP { get; set; }
        public int ArmorClass { get; set; }
        public int Speed { get; set; }
        public List<Skill> SavingThrows { get; set; }
        public int ClassSave { get; set; }
    }
    public class Class
    {
        public string Name { get; set; }
        public List<string> TrainedSkills { get; set; }
        public string Fortitude { get; set; }
        public string Reflex { get; set; }
        public string Will { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
    }
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public string Size { get; set; }
        public string Speed { get; set; }
        public string AttributeBoost { get; set; }
        public string AttributeDisadvantage { get; set; }
        public List<string> Languages { get; set; }
        public List<string> Traits { get; set; }
        public string Ability { get; set; }
    }
    public class Lineage
    {
        public string Name { get; set; }
        public string AttributeBoost1 { get; set; }
        public string AttributeBoost2 { get; set; }
        public List<string> SkillFields { get; set; }
        public string Ability { get; set; }
    }
    public class Skill
    {
        public string Name { get; set; }
        public string BasicCharacteristic { get; set; }
        public string TrainingLevel { get; set; }
        public int Modifier { get; set; }

        public Skill(string Name, string BasicCharacteristic, string TrainingLevel)
        {
            this.Name = Name;
            this.BasicCharacteristic = BasicCharacteristic;
            this.TrainingLevel = TrainingLevel;
            if (TrainingLevel == "Untrained")
                this.Modifier = 0;
            else if (TrainingLevel == "Trained")
                this.Modifier = 2;
            else if (TrainingLevel == "Expert")
                this.Modifier = 4;
            else if (TrainingLevel == "Master")
                this.Modifier = 6;
            else if (TrainingLevel == "Legendary")
                this.Modifier = 8;
        }
        public void UpTrainingLevel()
        {
            if (TrainingLevel == "Untrained")
            {
                TrainingLevel = "Trained";
                this.Modifier += 2;
            }
            else if (TrainingLevel == "Trained")
            {
                TrainingLevel = "Expert";
                this.Modifier += 2;
            }
            else if (TrainingLevel == "Expert")
            {
                TrainingLevel = "Master";
                this.Modifier += 2;
            }
            else if (TrainingLevel == "Master")
            {
                TrainingLevel = "Legendary";
                this.Modifier += 2;
            }
        }
    }
    public class Stat
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Modifier { get; set; }
        public Stat(string Name, int Score)
        {
            this.Name = Name;
            this.Score = Score;
            this.Modifier = (int)Math.Floor((double)(Score - 10) / 2);
        }

    }
    public class Trait
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
