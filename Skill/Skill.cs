using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game_VSmode_verTest{
class Skill {
        public string skill_name;

        //to-self
        public bool isMagic;
        public bool isAOE;
        public int cost;//isMagic=true minus MP, false add fatigue;

        //to-hostile
        public int damage;
        public int acc_opposite_fear;
        public int acc_opposite_anger;//if anger >fear damage*2,if fear>anger damage *0.5 (random stop to vs)
        BuffName add_buff = BuffName.NULL;
        //SpecialEffect add_se = new SpecialEffect();

        public Skill(
            string _skill_name ,bool _isMagic,bool _isAOE,
            int _damage,int _cost,
            int _acc_opposite_fear,int _acc_opposite_anger,
            BuffName _add_buff
            ){
            skill_name = _skill_name;
            isMagic = _isMagic;
            isAOE = _isAOE;
            damage = _damage;
            cost = _cost;
            acc_opposite_fear = _acc_opposite_fear;
            acc_opposite_anger = _acc_opposite_anger;
            add_buff = _add_buff;
        }
        //For FileInput
        public Skill(string path){ 
            StreamReader sr = new StreamReader(path,Encoding.Default);
            String line;
            //string[] =new string[6];
            while ((line = sr.ReadLine()) != null) {
                //skill=line.ToString();
            }
        }
    }
}
