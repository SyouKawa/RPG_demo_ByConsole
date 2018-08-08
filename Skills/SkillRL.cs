using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class SkillRL{//Skill Release Limit
        int[] PropertyLimit=new int[11];
        #region Character Element Property Limit
            int fire_limit;
            int ice_limit;
            int thunder_limit;
            int earth_limit;
            int wind_limit;
            int diamond_limit;
            int wood_limit;
        #endregion

        #region Character Base Property Limit
            int dodge;
            int speed;
            int intelligence;
            int physical_strength;
        #endregion
        public SkillRL(){}
        public SkillRL(int a){
            
        }
    }
}
