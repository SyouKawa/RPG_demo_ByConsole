using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class SkillRL{//Skill Release Limit
        int[] PropertyLimit=new int[4];
        #region Character Element Property Limit
            int fireRL;
            int waterRL;
			int sanRL;
            int evilRL;
            //int wind_limit;
            //int diamond_limit;
            //int wood_limit;
        #endregion

        #region Character Base Property Limit
            //int dodge;
            //int speed;
            //int intelligence;
            //int physical_strength;
        #endregion
        public SkillRL(){}
        public SkillRL(int _fireRL,int _waterRL,int _sanRL,int _evilRL){
			fireRL = _fireRL;
			waterRL = _waterRL;
			sanRL = _sanRL;
			evilRL = _evilRL;
        }
    }
}
