using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace P2BuiderRus.Model
{
    public class Race
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public string Size { get; set; }
        public int Speed { get; set; }
        public List<string> AttributeBoosts { get; set; }
        public List<string> AttributeFlaws { get; set; }
        public List<string> Languages { get; set; }
        public List<string> Special { get; set; }
        public List<string> Keywords { get; set; }
        public int LanguagesNumber { get; set; }

        public Race(DataTable cl)
        {
            Name = cl.Rows[0][0].ToString();
            HP = Convert.ToInt32(cl.Rows[0][2]);
            Size = cl.Rows[0][3].ToString();
            AttributeBoosts = cl.Rows[0][5].ToString().Split(',').ToList();
            AttributeFlaws = cl.Rows[0][6].ToString().Split(',').ToList();
            Special = cl.Rows[0][8].ToString().Split(',').ToList();
            Speed = Convert.ToInt32(cl.Rows[0][4]);
            Keywords = cl.Rows[0][1].ToString().Split(',').ToList();
            Languages = cl.Rows[0][7].ToString().Split(',').ToList();
            LanguagesNumber = Convert.ToInt32(cl.Rows[0][9]);
        }

        public Race()
        {
        }
    }
}
