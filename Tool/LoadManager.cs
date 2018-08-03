using System;
using System.IO;
using System.Text;
using LitJson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_VSmode_verTest.Tool{
    class LoadManager{
        private static LoadManager _instance;
        public static LoadManager Instance{
            get{
                if(_instance==null) _instance=new LoadManager();
                return _instance;
            }
        }

        private JsonData data;
        public void LoadConfigData(string file_text)
        {
            data=JsonMapper.ToObject(File.ReadAllText(file_text,Encoding.UTF8));
        }
        private void GetSkillsFromJson(){//TO-DO re-write to GetConfigFromFile(string path)
        //TO-DO switch case skill item character etc.
            for(int i=0;i<data.Count;i++){
                DecodeSimpleSkill(data[i]);
            }
        }

        public Object GetConfigFromFile(string path){
            LoadConfigData(path);
            string str="Skill";
            switch(str){
                case "Skill":
                    List<Skill> temp_skill_list_ref=new List<Skill>();
                    for(int i=0;i<data.Count;i++){
                        Skill temp_skill_ref=DecodeSimpleSkill(data[i]);
                        temp_skill_list_ref.Add(temp_skill_ref);
                    }
                    return temp_skill_list_ref;
                case "Monster":return null;
                case "Item":return null;
            }
            return null;
        }
        
        public Skill DecodeSimpleSkill(JsonData skill_config_data)
        {
            SkillType temp_type_ref=null;
            string _skill_name=skill_config_data["skill_name"].ToString();
            JsonData temp_type=skill_config_data["type"];
            {
                bool _isPositive=(bool)temp_type["isPositive"];
                bool _isMagic=(bool)temp_type["isMagic"];
                bool _isAOE=(bool)temp_type["isAOE"];
                bool _isConsume=(bool)temp_type["isConsume"];
                temp_type_ref=new SkillType(_isPositive,_isMagic,_isAOE,_isConsume);
            }
            int _cost=(int)skill_config_data["cost"];
            int _damage=(int)skill_config_data["damage"];
            int _acc_opposite_fear=(int)skill_config_data["acc_opposite_fear"];
            int _acc_opposite_anger=(int)skill_config_data["acc_opposite_anger"];
            return new Skill(_skill_name ,temp_type_ref,_cost,_damage,_acc_opposite_fear,_acc_opposite_anger);
        } 
    }
}
