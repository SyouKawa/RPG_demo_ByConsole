using System;
using System.Collections.Generic;
using System.Text;
using Game_VSmode_verTest;

namespace Game_VSmode_verTest
{
    class Program
    {
        static void Main(string[] args){
			//Player player = new Player();
			//Monster slem = new Monster();
			//FightScene fight_scene=new FightScene(player,slem);
			//fight_scene.FightOne2One(player,slem);
            Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
            //DisplayController.Instance.DrawFightPanel(new FightPanel("F i g h t ", 0, 0, 40,30),new List<Character>());//width=80
			SkillPanel test=new SkillPanel("S k i l l " ,new Pos(62 , 1) ,new Size(11 , 10));
			test.Draw();
			FightPanel fightPanel = new FightPanel("F i g h t " , new Pos(1 , 1) , new Size(60 , 40));
			fightPanel.Draw();
			while (true)
			{
				fightPanel.MoveBetweenOptions();
			}
            Console.ReadKey();
        }
    }
}
