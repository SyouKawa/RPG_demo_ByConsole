using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game_VSmode_verTest{
    class Skill
    {
        public string skill_name;
        public SkillType type;
        public int cost;//isMagic=true minus MP, false add fatigue;
        public int damage;
        public int acc_opposite_fear;
        public int acc_opposite_anger;

        public Skill(){}
        public Skill(string _skill_name ,SkillType _type,int _cost,int _damage,int _acc_opposite_fear,int _acc_opposite_anger)
        {
            skill_name = _skill_name;
            type=_type;
            cost = _cost;
            damage = _damage;
            acc_opposite_fear = _acc_opposite_fear;
            acc_opposite_anger = _acc_opposite_anger;
        }
    }
}
