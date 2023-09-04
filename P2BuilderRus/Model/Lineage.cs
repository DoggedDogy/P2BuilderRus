using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace P2BuiderRus.Model
{
    public class Lineage
    {
        public string Name { get; set; }
        public List<string> AttributeBoost1 { get; set; }
        public List<string> AttributeBoost2 { get; set; }
        public List<string> Skill1 { get; set; }
        public List<string> Skill2 { get; set; }
        public List<string> SkillFeat { get; set; }
        public string Desk { get; set; }
        public Lineage(DataTable cl)
        {
            Name = cl.Rows[0][0].ToString();
            AttributeBoost1 = cl.Rows[0][2].ToString().Split(',').ToList();
            AttributeBoost2 = cl.Rows[0][3].ToString().Split(',').ToList();
            Skill1 = cl.Rows[0][4].ToString().Split(',').ToList();
            Skill2 = cl.Rows[0][5].ToString().Split(',').ToList();
            Desk = cl.Rows[0][1].ToString();
            SkillFeat = cl.Rows[0][6].ToString().Split(',').ToList();
        }
        public Lineage()
        {

        }
    }
}
