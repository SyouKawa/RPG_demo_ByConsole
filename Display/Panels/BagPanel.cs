using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class BagPanel:Panel
	{
		//TODO-Change to read from Maneger.

		public List<Item> items;//OSC
		public string configPath;

		public BagPanel(string _title , Pos _startPos , Size _size , Player player)
		: base(_title , _startPos , _size)
		{
			type = PanelType.Bag;
			InitItemList(player);
		}

		public override void UpdateOptions()
		{
			foreach (OptionObject opt in options)
			{
				opt.DrawVertical(pointer);
			}
		}

		public override void FillOptionContent()
		{
			for (int i = 0 ; i < items.Count ; i++)
			{
				OptionObject tempOption = new OptionObject();
				tempOption.index = i;
				tempOption.content.Add(items[i].name + "\n");
				tempOption.size = new Size(items[i].name.Length , 1);
				//pos:startpos+padding_Axis * index
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
				case ConsoleKey.UpArrow:
					if (0 != pointer)
					{
						pointer--;
						UpdateOptions();
					}
					break;
				case ConsoleKey.DownArrow:
					if (pointer >= options.Count - 1) ;
					else pointer++;
					UpdateOptions();
					break;
				case ConsoleKey.Escape:
					controller.CloseCurPanel();
					return -2;
				case ConsoleKey.Spacebar:
					controller.OpenPanel(new DescripPanel("I n f o " , new Pos(30 , 10) , new Size(10 , 4) , items[pointer].description));
					((DescripPanel)controller.panels.Peek()).OperateOption();
					break;
			}
			return -1;
		}

		public void InitItemList(Player player)
		{
			items = new List<Item>();
			foreach (Item item in player.bag)
			{
				items.Add(item);
			}
		}
	}
}
