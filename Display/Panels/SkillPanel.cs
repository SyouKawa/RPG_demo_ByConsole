using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class SkillPanel:Panel{
		//TODO-Change to read from Maneger.

        public List<Skill> skills;//OSC
        public string configPath;

        public SkillPanel(string _title, Pos _startPos , Size _size,Player player)
        :base(_title,_startPos , _size)
		{
            type=PanelType.Skill;
            InitSkillList(player);
        }

		public override void UpdateOptions()
		{
			foreach(OptionObject opt in options)
			{
				opt.DrawVertical(pointer);
			}
		}

		public override void FillOptionContent()
		{
			for (int i=0;i<skills.Count ;i++)
			{
				OptionObject tempOption = new OptionObject();
				tempOption.index = i;
				tempOption.content.Add(skills[i].skill_name+"\n");
				tempOption.size = new Size(skills[i].skill_name.Length , 1);
				//pos:startpos+padding_Axis * index
				if (!isHorizon)
				{
					tempOption.startPos = new Pos(startPos.x+2, startPos.y + tempOption.size.heightRow * i+1);
				}
				else
				{
					tempOption.startPos = new Pos(startPos.x+2 + tempOption.size.widthCol * i , startPos.y+1);
				}
				options.Add(tempOption);
			}
		}

		public override int OperateOption()
		{
			if (!isTop) return -1;
			ConsoleKey cur_key = Console.ReadKey(true).Key;
			switch (cur_key)
			{
				case ConsoleKey.UpArrow:
					if (0 != pointer)
					{
						pointer--;
						UpdateOptions();
					}
					break;
				case ConsoleKey.DownArrow:
					if (pointer >= options.Count - 1) ;
					else pointer++;
					UpdateOptions();
					break;
				case ConsoleKey.Escape:
					controller.CloseCurPanel();
					return -2;
				case ConsoleKey.Enter:
					controller.CloseCurPanel();
					Fight.attackInfo = pointer;
					return pointer;
			}
			return -1;
		}

		public void InitSkillList(Player player){
			//configPath=Environment.CurrentDirectory+"\\Config\\Skill_config.json";
			skills = new List<Skill>();
			foreach (int id in player.ownSkillID)
			{
				skills.Add(GlobalSkillsManager.Instance.GetSkill(id));
			}
        }
    }
}
