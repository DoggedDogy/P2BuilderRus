using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace P2BuiderRus.Model
{
    public class ClassModel
    {
        public string ClassName { get; set; }
        public int HP { get; set; }
        public string PerceptionTraining { get; set; }
        public string FortitudeTraining { get; set; }
        public string ReflexTraining { get; set; }
        public string WillTraining { get; set; }
        public List<string> TrainedSkills { get; set; }
        public int TrainedSkillsNumber { get; set; }
        public List<string> KeyAbility { get; set; }
        public string Desk { get; set; }

        public ClassModel(DataTable cl)
        {
            ClassName = cl.Rows[0][0].ToString();
            HP = Convert.ToInt32(cl.Rows[0][2]);
            PerceptionTraining = cl.Rows[0][3].ToString();
            FortitudeTraining = cl.Rows[0][4].ToString();
            ReflexTraining = cl.Rows[0][5].ToString();
            WillTraining = cl.Rows[0][6].ToString();
            TrainedSkills = cl.Rows[0][7].ToString().Split(',').ToList();
            TrainedSkillsNumber = Convert.ToInt32(cl.Rows[0][8]);
            KeyAbility = cl.Rows[0][1].ToString().Split(',').ToList();
        }

        public ClassModel()
        {
        }
    }
}
