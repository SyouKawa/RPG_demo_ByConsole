using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class ActionPanel:Panel
	{
		public List<string> menuOpt;//OSC
		public Player bridgePlayer;
		public ActionPanel(string _title , Pos _startPos , Size _size,Player player)
		: base(_title , _startPos , _size)
		{
			bridgePlayer = player;
			
			//default
			type = PanelType.Action;
			menuOpt = new List<string>();
			menuOpt.Add("Attack");
			menuOpt.Add("Bag");
		}

		public override void FillOptionContent()
		{
			for (int i = 0 ; i < menuOpt.Count ; i++)
			{
				OptionObject tempOption = new OptionObject();
				tempOption.index = i;
				tempOption.content.Add(menuOpt[i]);
				tempOption.size = new Size(menuOpt[i].Length , 1);
				if (!isHorizon)
				{
					tempOption.startPos = new Pos(startPos.x + 4 , startPos.y + tempOption.size.heightRow * i + 1);
				}
				else// +3= From Center
				{
					tempOption.startPos = new Pos(startPos.x + 4 + tempOption.size.widthCol * i , startPos.y + 1);
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
				case ConsoleKey.Enter:
					ResetOutputStyle();
					return OptionFunc();
				case ConsoleKey.Escape:
					ResetOutputStyle();
					controller.CloseCurPanel();
					break;
			}
			return -1;
		}

		public override int OptionFunc()
		{
			switch (pointer)
			{
				case 0:
					SkillPanel temp_skill = new SkillPanel("S k i l l " , new Pos(options[pointer].startPos.x + 10 , startPos.y) , new Size(6 , 10) , bridgePlayer);
					controller.OpenPanel(temp_skill);
					while (temp_skill.OperateOption() == -1) ;
					controller.CloseCurPanel();
					Fight.actionInfo = pointer;
					return pointer;
				case 1:
					SkillPanel temp_bag = new SkillPanel("B a g " , new Pos(options[pointer].startPos.x + 10 , startPos.y) , new Size(8 , 10) , bridgePlayer);
					controller.OpenPanel(temp_bag);
					while (temp_bag.OperateOption() == -1) ;
					controller.CloseCurPanel();
					Fight.actionInfo = pointer;
					return pointer;
			}

			return -1;
		}
	}
}
