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
					if (OptionFunc() == -2)//only close pushedPanel.
					{
						return -1;//keep this loop
					}
					else
					{
						return pointer;
					}
				case ConsoleKey.Escape:
					ResetOutputStyle();
					controller.CloseCurPanel();
					return -2;
			}
			return -1;
		}

		public override int OptionFunc()
		{
			switch (pointer)
			{
				case 0:
					SkillPanel temp_skill = new SkillPanel("S k i l l " , new Pos(options[pointer].startPos.x + 10 , startPos.y) , new Size(6 , 8) , bridgePlayer);
					controller.OpenPanel(temp_skill);

					int pushPanelRes = temp_skill.OperateOption();
					while (pushPanelRes == -1)//still choosing options
					{
						pushPanelRes = temp_skill.OperateOption();
					}
					if (pushPanelRes == -2) return -2;//closed pushPanel
					else controller.CloseCurPanel();

					Fight.actionInfo = pointer;
					return pointer;
				case 1:
					BagPanel temp_bag = new BagPanel("B a g " , new Pos(options[pointer].startPos.x + 10 , startPos.y) , new Size(8 , 8) , bridgePlayer);
					controller.OpenPanel(temp_bag);

					pushPanelRes = temp_bag.OperateOption();
					while (pushPanelRes == -1)//still choosing options
					{
						pushPanelRes = temp_bag.OperateOption();
					}
					if (pushPanelRes == -2) return -2;//closed pushPanel and stay here
					else controller.CloseCurPanel();
					//controller.CloseCurPanel();

					Fight.actionInfo = pointer;
					return -1;
					//return pointer;
			}

			return -1;
		}
	}
}
