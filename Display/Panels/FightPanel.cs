using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class FightPanel:Panel{

		//List<Character> chrs;//OSC
		string MonsterPic; //string MonsterPic;
		public Fight fightscene;

		#region InitPart
		public FightPanel(string _title , Pos _startPos , Size _size)
		: base(_title , _startPos , _size)
		{
			type=PanelType.Fight;
            //InitCharacterList();
			isHorizon = true;

			//default
			fightscene = new Fight();
			InitCharacterList();
			fightscene.hostile = new Monster();
			fightscene.fightPanel = this;

			controller.OpenPanel(this);
		}

		public void InitCharacterList()
		{
			Player p1 = new Player();
			fightscene.playerTeam.Add(p1);
			fightscene.playerTeam.Add(p1);
			fightscene.playerTeam.Add(p1);
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
					tempOption.startPos = new Pos(startPos.x + 2 + 2+ tempOption.size.widthCol * i , startPos.y + 1);
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
					break;
				case ConsoleKey.Enter:
					ResetOutputStyle();
					ActionPanel temp_actionPanel = new ActionPanel("A c t i o n " , new Pos(this.options[pointer].startPos.x , this.startPos.y + 5) , new Size(6 , 5));
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
