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
        public List<Skill> allSkills;
        public Dictionary<int,Skill> skillsDict;
        
        public GlobalSkillsManager(){}
        public GlobalSkillsManager(string _configPath){
            configPath=_configPath;
            allSkills=new List<Skill>(LoadController.Instance.GetSkillsConfig(configPath));
            skillsDict=new Dictionary<int, Skill>();
            foreach(Skill temp in allSkills){
                skillsDict.Add(temp.skillID,temp);
            }
        }
        public void InitGlobalSkillManager(string _configPath){
            configPath=_configPath;
            allSkills=new List<Skill>(LoadController.Instance.GetSkillsConfig(configPath));
        }    
        //TODO-Add,Del,Find,Change for Character.
        public Skill GetSkill(int skillID){
			if (skillsDict.ContainsKey(skillID))
			{
				//DON'T change Prototype
				return new Skill(skillsDict[skillID]);
			} 
            else return null;
        }

		public void AddSpecialEffectPositive(int skillID , List<Player> team , Player monster)
		{//for Attack
			switch (skillID)
			{
				case 1001:

					break;
				case 1002:

					break;
			}
		}

		public void AddSpecialEffectPassive(int skillID , List<Player> team , Player monster)
		{//for OnHit
			switch (skillID)
			{
				case 1001:

					break;
				case 1002:

					break;
			}
		}

	}

}
