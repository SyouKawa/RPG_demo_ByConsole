using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class DisplayController
	{
		private static DisplayController _instance;
		public static DisplayController Instance
		{
			get
			{
				if (_instance == null) _instance = new DisplayController();
				return _instance;
			}
		}

		public int pointer = -1;
		public Stack<Panel> panels = new Stack<Panel>();

		public Object OperateCurPanel()
		{//for Top(Cur)-element
			ConsoleKey cur_key = Console.ReadKey(true).Key;
			switch (cur_key)
			{
				case ConsoleKey.Escape:
					CloseCurPanel();
					return null;

				case ConsoleKey.UpArrow:
					if (0 != pointer)
					{
						pointer--;
						RefreshCurPanel();
					}
					return null;

				case ConsoleKey.DownArrow:
					if (pointer == ((SkillPanel)panels.Peek()).skills.Count - 1) ;
					else pointer++;
					RefreshCurPanel();
					return null;

				case ConsoleKey.Enter:
					//HOW-TO-DO? Different Panel return
					Object selected = ((SkillPanel)panels.Peek()).skills[pointer];
					CloseCurPanel();
					return selected;
				case ConsoleKey.Spacebar:
					Skill temp_skill = ((SkillPanel)panels.Peek()).skills[pointer];
					//DescripPanel descrpPanel = new DescripPanel(temp_skill.skill_name , 20 , 10 , 12 , 5);
					//descrpPanel.content = "Damage:" + temp_skill.damage.ToString() + " Cost:" + temp_skill.cost.ToString();
					//OpenNewPanel(descrpPanel);

					break;
			}
			return null;
		}

		public void OpenNewPanel(Panel panel)
		{
			_instance.panels.Push(panel);
			switch (_instance.panels.Peek().type)
			{
				case PanelType.Act:
					break;
				case PanelType.Skill:
					//DrawSkillPanel(panel);
					break;
				case PanelType.Fight:
					//DrawFightPanel(panel);
					break;
				case PanelType.Map:
					break;
				case PanelType.Bag:
					break;
				case PanelType.Menu:
					break;
				case PanelType.Descrp:
					//DrawDescrpPanel((DescripPanel)panel);
					break;
			}
		}
		public void RefreshCurPanel()
		{
			switch (panels.Peek().type)
			{
				case PanelType.Act:
					break;
				case PanelType.Skill:
					//DrawSkillPanel(panels.Peek());
					break;
				case PanelType.Map:
					break;
				case PanelType.Bag:
					break;
				case PanelType.Menu:
					break;
			}
		}
		public void DrawPanel(Panel panel)
		{
			switch (panel.type)
			{
				case PanelType.Act:
					break;
				case PanelType.Skill:
					//DrawSkillPanel(panels.Peek());
					break;
				case PanelType.Map:
					break;
				case PanelType.Bag:
					break;
				case PanelType.Menu:
					break;
			}
		}
		public void CloseCurPanel()
		{
			_instance.panels.Pop();
			Console.Clear();
			foreach (Panel p in _instance.panels)
			{
				DrawPanel(p);
			}
		}

		//public static void DrawPanelFrame(Panel panel)
		//{
		//	Console.SetCursorPosition(panel.startX+1 , panel.startY);//panel.startX+1 : prevent the first '-' beyond width
		//	int simple_space_count = (panel.panelCol*2 - panel.title.Length) / 2 / 2;
		//	string temp_space1 = new string('━' , simple_space_count);
		//	string temp_space2 = new string(' ' , simple_space_count);
		//	Console.Write(temp_space1 + temp_space2 + panel.title + temp_space2 + temp_space1);

		//	for (int p_line = panel.startY + 1 ; p_line < panel.startY + panel.panelRow ; p_line++)
		//	{
		//		Console.SetCursorPosition(panel.startX , p_line);
		//		if (p_line != panel.startY + panel.panelRow - 1)
		//		{
		//			Console.Write("‖");//except the last line
		//			string space = new string(' ' , panel.panelCol*2 - 2);
		//			Console.Write(space);
		//			Console.Write("‖");
		//		}
		//		else if (p_line == panel.startY + panel.panelRow - 1)
		//		{
		//			Console.CursorLeft += 1;
		//			for (int i = panel.panelCol *2-1; i > 0 ; i--)
		//			{
		//				Console.Write("━");//the-last-line
		//			}
		//		}
		//	}
		//}

		//public void DrawSkillPanel(Panel panel)
		//{
		//	//DrawFrame
		//	DrawPanelFrame(panel);
		//	SkillPanel skillPanel = panel as SkillPanel;
		//	//DrawContent
		//	for (int p_line = skillPanel.startY + 1, data_line = 0 ; p_line < skillPanel.startY + 1 + skillPanel.skills.Count ; p_line++, data_line++)
		//	{
		//		Console.SetCursorPosition(panel.startX + 2 , p_line);// +1: except drawed-left-frame
		//		if (data_line < skillPanel.skills.Count)
		//		{
		//			if (data_line == pointer)
		//			{
		//				Console.BackgroundColor = ConsoleColor.Magenta;
		//				Console.ForegroundColor = ConsoleColor.Black;
		//			}
		//			Console.Write(skillPanel.skills[data_line].skill_name);
		//		}
		//	}
		//}
		//public void DrawDescrpPanel(DescripPanel panel)
		//{
		//	DrawPanelFrame(panel);
		//	//TO-DO wait a return to display
		//	Console.SetCursorPosition(panel.startX + 2 , panel.startY + 2);
		//	Console.Write(panel.content);
		//}

		//public void DrawFightPanel(FightPanel panel,List<Character>chrs)
		//{
		//	DrawPanelFrame(panel);

		//	//TODO-Draw ACSII-Pic-Area + Monster's Status

		//	//Draw Chr-List-Area + Status

		//	//Test chrsCode
		//	chrs.Add(new Player());
		//	chrs.Add(new Monster());

		//	Console.SetCursorPosition(panel.startX + 2 , panel.startY + 15);
			//chrs[0].PrintData();

		//}

	}
}
