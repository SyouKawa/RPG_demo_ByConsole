using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class FightPanel:Panel{

		List<Character> chrs;//OSC
							 //string MonsterPic;
		#region InitPart
		public FightPanel(string _title , Pos _startPos , Size _size)
		: base(_title , _startPos , _size)
		{
			type=PanelType.Fight;
            InitCharacterList();
			isHorizon = true;

			controller.OpenPanel(this);
        }

		public void InitCharacterList()
		{
			chrs = new List<Character>();
			Player p1 = new Player();
			chrs.Add(p1);
			chrs.Add(p1);
			chrs.Add(p1);
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
			for (int i=0;i<chrs.Count ;i++ )
			{
				OptionObject tempOption = new OptionObject();
				tempOption.index = i;
				tempOption.content=chrs[i].GetChrData();
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


		public override void OperateOption()
		{
			if (!isTop) return;
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
				case ConsoleKey.K:
					ResetOutputStyle();
					controller.OpenPanel(new SkillPanel("S k i l l " , new Pos(this.options[pointer].startPos.x , this.startPos.y + 5) , new Size(11 , 10)));
					break;
			}
		}

	}
}
