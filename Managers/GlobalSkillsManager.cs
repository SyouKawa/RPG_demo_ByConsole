using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class GlobalSkillsManager {
        
        private static GlobalSkillsManager _instance;
        public static GlobalSkillsManager Instance{
            get{
                if(_instance==null) _instance=new GlobalSkillsManager(Environment.CurrentDirectory+"\\Config\\Skill_config.json");
                return _instance;
            }
        }
        public string configPath;
        public List<Skill> allSkills=new List<Skill>();
        
        public GlobalSkillsManager(){}
        public GlobalSkillsManager(string _configPath){
            configPath=_configPath;
            allSkills=new List<Skill>(LoadController.Instance.GetSkillsConfig(configPath));
        }
        public void InitGlobalSkillManager(string _configPath){
            configPath=_configPath;
            allSkills=new List<Skill>(LoadController.Instance.GetSkillsConfig(configPath));
        }
    }
    //TODO-Add,Del,Find,Change for Character.
}
