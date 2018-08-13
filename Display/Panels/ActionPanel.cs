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

		public ActionPanel(string _title , Pos _startPos , Size _size)
		: base(_title , _startPos , _size)
		{
			//default
			type = PanelType.Action;
			menuOpt = new List<string>();
			menuOpt.Add("Attack");
			menuOpt.Add("Bag");
			menuOpt.Add("Escape");
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

		public override void OperateOption()
		{
			if (!isTop) return;
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
					OptionFunc();
					break;
				case ConsoleKey.Escape:
					ResetOutputStyle();
					controller.CloseCurPanel();
					break;
			}
		}

		public override void OptionFunc()
		{
			switch (pointer)
			{
				case 0:
					controller.OpenPanel(new SkillPanel("S k i l l " , new Pos(this.options[pointer].startPos.x , this.startPos.y + 5) , new Size(11 , 10)));
					break;
			}
		}
	}
}
