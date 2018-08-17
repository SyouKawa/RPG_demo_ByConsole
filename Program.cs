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
			//LoadManager.SerializeJsonTemplate();

			DisplayController.Instance.OpenPanel(new MapPanel("M a p " , new Pos(0 , 0) , new Size(30 , 30)));
			while (true)
			{
				DisplayController.Instance.ControlCurPanel();
				if (DisplayController.Instance.panels.Peek().type == PanelType.Fight)
				{
					FightPanel curFight = (FightPanel)(DisplayController.Instance.panels.Peek());
					curFight.fightscene.RoundBattle();
				}
			}
        }
    }
}
