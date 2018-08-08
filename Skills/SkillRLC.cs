using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class SkillRLC {//Skill Rlease Limit Check
        #region Character Status Limit
            bool isFatigue;
            bool isMP_Empty;
            bool isAnger;
            bool isFear;
            bool isBlessing;
        #endregion

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

        #region Character Equipment Limit
            bool isFullEquiped;
            bool isLuckyNum;
        #endregion
    }
}
