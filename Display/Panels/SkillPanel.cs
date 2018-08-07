using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest.Display{
    class SkillPanel:Panel{
        //content
        public List<Skill> skills;
        public string configPath;
        //tool
        public int pointer;

        public SkillPanel(string _title,int _startX,int _startY,int _panelRow,int _panelCol,string _configPath)
        :base(_title,_startX,_startY,_panelRow,_panelCol){
            configPath=_configPath;
        }

        public void InitSkillList(){
            configPath=Environment.CurrentDirectory+"\\Config\\Skill_config.json";
            skills=new List<Skill>(LoadManager.Instance.GetConfigFromFile(configPath,LoadMode.Skill)as List<Skill>);
        }
    }
}
