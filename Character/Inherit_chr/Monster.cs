using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class Monster : Character {
        Item[] dropitems = new Item[5];
        int rate_level;
        float rate;
                public Monster(){
            name = "Slem";
            career = Career.UNEMPLOYED;
            skill[0] = new Skill("Jump",false,false,10,0,0,0,BuffName.NULL);
            skill[1] = new Skill("Throw",true,false,25,5,0,10,BuffName.NULL);
        }
    }
}
