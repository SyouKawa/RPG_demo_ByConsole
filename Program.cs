using System;
using System.Collections.Generic;
using System.Text;
using Game_VSmode_verTest;

namespace Game_VSmode_verTest
{
    class Program
    {
		static void Main(string[] args){
            Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
			//Test-Code
			//FightPanel fightPanel = new FightPanel("F i g h t " , new Pos(4 , 4) , new Size(30 , 30));
			MapPanel map = new MapPanel("M a p " , new Pos(0 , 0) , new Size(30 , 30));
			while (true)
			{
				switch (DisplayController.Instance.panels.Peek().type)
				{
					case PanelType.Map:
						map.InitBlockList();
						map.UpdateOptions();
						break;
					case PanelType.Fight:
							((FightPanel)(DisplayController.Instance.panels.Peek())).fightscene.RoundBattle();
						break;
				}
			}
        }
    }
}
