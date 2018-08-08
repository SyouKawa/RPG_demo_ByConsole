using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class SkillPanel:Panel{
        //content
        public List<Skill> skills;
        public string configPath;

        public SkillPanel(string _title,int _startX,int _startY,int _panelRow,int _panelCol)
        :base(_title,_startX,_startY,_panelRow,_panelCol){
            type=PanelType.Skill;
            //configPath=_configPath;
            InitSkillList();
        }

        public void InitSkillList(){
            configPath=Environment.CurrentDirectory+"\\Config\\Skill_config.json";
            //skills=new List<Skill>(LoadManager.Instance.GetConfigFromFile(configPath,LoadMode.Skill)as List<Skill>);
        }
    }
}
