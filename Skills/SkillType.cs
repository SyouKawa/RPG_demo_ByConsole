using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class SkillType {
        public bool isPositive;
        public bool isMagic;
        public bool isAOE;
        public bool isConsume;

        public SkillType(){}
        public SkillType(bool _isPositive,bool _isMagic,bool _isAOE,bool _isConsume)
        {
            isPositive=_isPositive;
            isMagic=_isMagic;
            isAOE=_isAOE;
            isConsume=_isConsume;
        }
    }
}
