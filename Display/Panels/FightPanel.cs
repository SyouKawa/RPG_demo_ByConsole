using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class FightPanel:Panel{

		//List<Character> chrs;//OSC
		string MonsterPic; //string MonsterPic;
		public LogPanel logPanel;

		public Fight fightscene;

		#region InitPart
		public FightPanel(string _title , Pos _startPos , Size _size,Fight fight)
		: base(_title , _startPos , _size)
		{
			type=PanelType.Fight;
			isHorizon = true;
			fightscene = fight;

			//fightsceneSetVaule outside
			//InitCharacterList();
			//fightscene.hostile = new Player();
			fightscene.fightPanel = this;
			logPanel = new LogPanel("L o g " , new Pos(startPos.x + 60 , startPos.y + 2) , new Size(14 , 26));

			//controller.OpenPanel(this);
		}

		//TestCode
		//public void InitCharacterList()
		//{
		//	Player p1 = new Player();
		//	fightscene.playerTeam.Add(p1);
		//	fightscene.playerTeam.Add(new Player(2));
		//}
		#endregion

		#region Override DrawPart
		public override void UpdateOptions()
		{
			foreach (OptionObject opt in options)
			{
				opt.DrawHorizon(pointer);
			}
		}

		public override void FillOptionContent()
		{
			for (int i=0;i< fightscene.playerTeam.Count ;i++ )
			{
				OptionObject tempOption = new OptionObject();
				tempOption.index = i;
				tempOption.content= fightscene.playerTeam[i].GetChrData();
				tempOption.size = new Size(13 , 11);
				if (!isHorizon)
				{
					tempOption.startPos = new Pos(startPos.x + 2 , startPos.y + tempOption.size.heightRow * i + 1);
				}
				else
				{
					tempOption.startPos = new Pos(startPos.x + 2 + 2+ tempOption.size.widthCol * i , startPos.y + 13);
				}
				options.Add(tempOption);
			}

		}
		#endregion


		public override int OperateOption()
		{
			if (!isTop) return -1;
			ConsoleKey cur_key = Console.ReadKey(true).Key;
			switch (cur_key)
			{
				case ConsoleKey.LeftArrow:
					if (0 != pointer)
					{
						pointer--;
						UpdateOptions();
					}
					break;

				case ConsoleKey.RightArrow:
					if (pointer >= options.Count - 1) ;
					else pointer++;
					UpdateOptions();
					ResetOutputStyle();
					break;
				case ConsoleKey.Enter:
					ResetOutputStyle();
					ActionPanel temp_actionPanel = new ActionPanel("A c t i o n " , new Pos(options[pointer].startPos.x , options[pointer].startPos.y + 5) , new Size(6 , 4),fightscene.playerTeam[pointer]);
					controller.OpenPanel(temp_actionPanel);
					while (temp_actionPanel.OperateOption() == -1) ;
					Fight.chrInfo = pointer;
					return pointer;
					//break;
				case ConsoleKey.Escape:
					ResetOutputStyle();
					controller.OpenPanel(new GlobalStaticPanel(PanelType.Menu));
					break;
			}
			return -1;
		}

	}
}
