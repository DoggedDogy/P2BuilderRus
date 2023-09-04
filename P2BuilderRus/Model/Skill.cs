using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace P2BuiderRus.Model
{
    public class Skill
    {
        public string Name { get; set; }
        public string BasicCharacteristic { get; set; }
        public string TrainingLevel { get; set; }
        public int TrainingModifier { get; set; }
        private int statModifier { get; set; }
        public int Modifier { get; set; }

        public Skill(string Name, string BasicCharacteristic, string TrainingLevel, int statModifier)
        {
            this.Name = Name;
            this.BasicCharacteristic = BasicCharacteristic;
            this.TrainingLevel = TrainingLevel;
            if (TrainingLevel == "Untrained")
                this.TrainingModifier = 0;
            else if (TrainingLevel == "Trained")
                this.TrainingModifier = 2;
            else if (TrainingLevel == "Expert")
                this.TrainingModifier = 4;
            else if (TrainingLevel == "Master")
                this.TrainingModifier = 6;
            else if (TrainingLevel == "Legendary")
                this.TrainingModifier = 8;
            this.statModifier = statModifier;
            this.Modifier = statModifier + this.TrainingModifier;
        }
        public void UpTrainingLevel()
        {
            if (TrainingLevel == "Untrained")
            {
                TrainingLevel = "Trained";
                this.TrainingModifier += 2;
            }
            else if (TrainingLevel == "Trained")
            {
                TrainingLevel = "Expert";
                this.TrainingModifier += 2;
            }
            else if (TrainingLevel == "Expert")
            {
                TrainingLevel = "Master";
                this.TrainingModifier += 2;
            }
            else if (TrainingLevel == "Master")
            {
                TrainingLevel = "Legendary";
                this.TrainingModifier += 2;
            }
        }

    }
}
