using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Game_VSmode_verTest{
    class Skill
    {
        public int skillID;
        public string skill_name;
        public int cost;//isMagic=true minus MP, false add fatigue;
        public int damage;
        public int acc_opposite_fear;
        public int acc_opposite_anger;
        public SkillType type;
        public SkillRL rl;

        public Skill(){}
        public Skill(Skill temp){
            skillID=temp.skillID;
            skill_name = temp.skill_name;
            cost = temp.cost;
            damage = temp.damage;
            acc_opposite_fear = temp.acc_opposite_fear;
            acc_opposite_anger = temp.acc_opposite_anger;
        }
        public Skill(int _skillID,string _skill_name ,SkillType _type,int _cost,int _damage,int _acc_opposite_fear,int _acc_opposite_anger/*,SkillRL _rl*/)
        {
            skillID=_skillID;
            skill_name = _skill_name;
            cost = _cost;
            damage = _damage;
            acc_opposite_fear = _acc_opposite_fear;
            acc_opposite_anger = _acc_opposite_anger;
            type=_type;
            //rl=_rl;
            //Container
        }
    }
}
