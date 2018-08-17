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

			//Test - SerializeJson
			LoadManager.SerializeJsonTemplate();

			//Test FightDisplay and Spawn a Round
			//Fight testFight = new Fight();
			//{
			//	testFight.playerTeam.Add(new Player());
			//	testFight.playerTeam.Add(new Player(1));
			//	testFight.playerTeam.Add(new Player(1));
			//}
			//FightPanel fightPanel = new FightPanel("F i g h t " , new Pos(4 , 4) , new Size(45 , 30) , testFight);
			MapPanel map = new MapPanel("M a p " , new Pos(0 , 0) , new Size(30 , 30));
			while (true)
			{
				switch (DisplayController.Instance.panels.Peek().type)
				{
					case PanelType.Map:
						map.ControlCheck();
						break;
					case PanelType.Fight:
							((FightPanel)(DisplayController.Instance.panels.Peek())).fightscene.RoundBattle();
						break;
				}
			}
        }
    }
}
