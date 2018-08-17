using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class TeamPanel:Panel
	{
		public MapPanel map;

		public TeamPanel(string _title, Pos _startPos, Size _size,MapPanel _map)
		: base(_title, _startPos, _size)
		{
			map = _map;

			//default
			isHorizon = true;
		}

		public override void FillOptionContent()
		{
			options.Clear();
			for (int i = 0 ; i < map.teamDict[map.pointer].Count ; i++)
			{
				OptionObject tempOption = new OptionObject();
				tempOption.index = i;
				tempOption.content = map.teamDict[map.pointer][i].npc.GetChrData();
				tempOption.size = new Size(13 , 6);
				if (!isHorizon)
				{
					tempOption.startPos = new Pos(startPos.x + 2 , startPos.y + tempOption.size.heightRow * i + 1);
				}
				else
				{
					tempOption.startPos = new Pos(startPos.x + 2 + tempOption.size.widthCol * i , startPos.y + 1);
				}
				options.Add(tempOption);
			}
		}

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
				case ConsoleKey.Q:
					if (pointer == 0)
					{
						controller.OpenPanel(new DescripPanel("I n f o " , new Pos(startPos.x + 3 , startPos.y + 3) , new Size(10 , 4) , "无法让自己离队"));
					}
					else
					{
						int srcIndex=map.Pos2Index(map.teamDict[map.pointer][pointer].pos);
						map.map.RemoveAt(srcIndex);
						map.map.Insert(srcIndex,map.teamDict[map.pointer][pointer]);
						map.teamDict[map.pointer].RemoveAt(pointer);
						Draw();
					}
					break;
				case ConsoleKey.Escape:
					controller.CloseCurPanel();
					break;
			}
			return -1;
		}
	}
}
