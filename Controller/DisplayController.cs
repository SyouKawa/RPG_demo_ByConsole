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
		public int IDcount = 1000;
		public Stack<Panel> panels = new Stack<Panel>();

		public void OpenPanel(Panel tempanel)
		{
			//1.Reset the oldPanel's isTop
			//2.(Auto in Contructor)Set newPanel's isTop
			if (panels.Count == 0)
			{
				panels.Push(tempanel);
				tempanel.Draw();
				return;
			}
			panels.Peek().isTop = false;
			panels.Push(tempanel);
			tempanel.Draw();
		}

		public void PopPanel()
		{
			//1.pop top
			//2. tell the newTopPanel Set isTop;
			panels.Pop();
			panels.Peek().isTop = true;
		}

		public void CloseCurPanel()
		{
			PopPanel();
			Console.Clear();
			foreach (Panel p in _instance.panels)
			{
				p.Draw();
			}
		}

		public void ControlCurPanel()
		{
			panels.Peek().OperateOption();
		}

		//public void DrawDescrpPanel(DescripPanel panel)
		//{
		//	DrawPanelFrame(panel);
		//	//TO-DO wait a return to display
		//	Console.SetCursorPosition(panel.startX + 2 , panel.startY + 2);
		//	Console.Write(panel.content);
		//}


	}
}
