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
		public bool quitFight;

		#region InitPart
		public FightPanel(string _title , Pos _startPos , Size _size,Fight fight)
		: base(_title , _startPos , _size)
		{
			type=PanelType.Fight;
			isHorizon = true;
			fightscene = fight;

			fightscene.fightPanel = this;
			logPanel = new LogPanel("L o g " , new Pos(startPos.x + 60 , startPos.y + 2) , new Size(14 , 26));

		}
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

		public override void Draw()
		{
			base.Draw();
			logPanel.Draw();
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

					int pushPanelRes = temp_actionPanel.OperateOption();
					while (pushPanelRes == -1)//still choosing options
					{
						pushPanelRes = temp_actionPanel.OperateOption();
					}
					if (pushPanelRes == -2)//closed pushPanel
					{
						return -1;
					}
					//else controller.CloseCurPanel();

					Fight.chrInfo = pointer;
					return pointer;
				case ConsoleKey.Escape:
					controller.CloseCurPanel();
					fightscene = null;
					Console.BackgroundColor = ConsoleColor.Black;
					break;
					//return -2;
			}
			return -1;
		}

	}
}
