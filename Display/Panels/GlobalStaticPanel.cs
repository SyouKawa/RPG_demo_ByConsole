using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class GlobalStaticPanel:Panel
	{

		public List<string> menuOpt;//OSC

		public GlobalStaticPanel(PanelType type)
		:base()
		{
			switch (type)
			{
				case PanelType.Main:
					CreateMainPanel();
					break;
				case PanelType.Menu:
					CreateMenuPanel();
					break;
			}
		}

		public void CreateMenuPanel()
		{
			title = "M e n u ";
			startPos = new Pos(20 , 20);
			size = new Size(6 , 5);
			options = new List<OptionObject>();

			//default
			type = PanelType.Menu;
			isHorizon = false;
			menuOpt= new List<string>();
			menuOpt.Add("Save");
			menuOpt.Add("Load");
			menuOpt.Add("Quit");

			//no self-call
			//controller.OpenPanel(this);
		}

		public void CreateMainPanel()
		{
			title = "M a i n ";
			startPos = new Pos(10 , 10);
			size = new Size(6 , 5);
			options = new List<OptionObject>();

			//default
			type = PanelType.Menu;
			isHorizon = false;
			menuOpt = new List<string>();
			menuOpt.Add("NewGame");
			menuOpt.Add("Load");
			menuOpt.Add("Quit");

			controller.OpenPanel(this);
		}

		public override void FillOptionContent()
		{
			for (int i=0 ;i<menuOpt.Count ;i++ )
			{
				OptionObject tempOption = new OptionObject();
				tempOption.index = i;
				tempOption.content.Add(menuOpt[i]);
				tempOption.size = new Size(menuOpt[i].Length , 1);
				if (!isHorizon)
				{
					tempOption.startPos = new Pos(startPos.x + 5 , startPos.y + tempOption.size.heightRow * i + 1);
				}
				else// +3= From Center
				{
					tempOption.startPos = new Pos(startPos.x + 5 + tempOption.size.widthCol * i , startPos.y + 1);
				}
				options.Add(tempOption);
			}

		}

		public override int OperateOption()
		{
			if (!isTop) return -1;
			ConsoleKey curKey = Console.ReadKey(true).Key;
			switch (curKey)
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
					break;
				case ConsoleKey.Enter:
					OptionFunc();//3-kinds
					break;
			}
			return -1;
		}

		public override int OptionFunc()
		{
			switch (type)
			{
				case PanelType.Main:
					MainOptionFunc();
					break;
				case PanelType.Menu:
					MenuOptionFunc();
					break;
			}
			return -1;
		}
		#region OptionFunc()-> Menu,Main,Action Implement
		public void MenuOptionFunc()
		{
			switch (pointer)
			{
				case 2:
					Environment.Exit(0);
					break;
			}
		}
		public void MainOptionFunc()
		{
			switch (pointer)
			{
				case 2:
					Environment.Exit(0);
					break;
			}
		}
		#endregion
	}
}
