using System;
using System.Collections.Generic;
using System.Text;
using Game_VSmode_verTest;

namespace Game_VSmode_verTest
{
    class Program
    {
		public static int actionInfo = -1, attackInfo = -1, chrInfo = -1;
		static void Main(string[] args){
			//Player player = new Player();
			//Monster slem = new Monster();
			//FightScene fight_scene=new FightScene(player,slem);
			//fight_scene.FightOne2One(player,slem);
            Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.Default;
			//DisplayController.Instance.DrawFightPanel(new FightPanel("F i g h t ", 0, 0, 40,30),new List<Character>());//width=80
			//SkillPanel test = new SkillPanel("S k i l l " , new Pos(10 , 1) , new Size(11 , 10));
			//test.Draw();
			FightPanel fightPanel = new FightPanel("F i g h t " , new Pos(4 , 4) , new Size(30 , 30));
			//GlobalStaticPanel menuPanel =new GlobalStaticPanel(PanelType.Action);
			//fightPanel.Draw();
			while (true)
			{
				//ActionRes curAction=DisplayController.Instance.ControlCurPanel();
				switch (DisplayController.Instance.panels.Peek().type)
				{
					case PanelType.Map:
						break;
					case PanelType.Fight:
						while (chrInfo == -1)
						{
							chrInfo = DisplayController.Instance.ControlCurPanel();
						}
						if (actionInfo != -1 && attackInfo != -1 && chrInfo != -1)
						{
							((FightPanel)(DisplayController.Instance.panels.Peek())).fightscene.RoundBattle(chrInfo,actionInfo,attackInfo);
						}
						break;
				}
			}
            //Console.ReadKey();
        }
    }
}
