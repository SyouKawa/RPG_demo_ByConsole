using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class MapPanel:Panel
	{
		public List<Block> map;//replace OptionObject to Block
		public List<Block> chrBlock;
		public string configPath;

		public MapPanel(string _title , Pos _startPos , Size _size)
		: base(_title , _startPos , _size)
		{
			type = PanelType.Map;
			isHorizon = true;

			//default
			map = new List<Block>();
			chrBlock = new List<Block>();
			chrBlock.Add(new Block(new Pos(4 , 9) , '法' , BlockType.Player));
			chrBlock.Add(new Block(new Pos(6 , 9) , '战' , BlockType.Player));
			chrBlock.Add(new Block(new Pos(8 , 9) , '牧' , BlockType.Player));
			pointer = 0;

			controller.OpenPanel(this);
		}

		public override void FillOptionContent()
		{
			//Read Text into List<Block>map
			InitBlockList();
		}

		public override void UpdateOptions()
		{
			//Print Init List<Block>map
			foreach (Block curBlcok in map)
			{
				Console.SetCursorPosition(curBlcok.pos.x , curBlcok.pos.y);
				Console.Write(curBlcok.style);
			}
			//Print Init Character
			foreach (Block curchr in chrBlock)
			{
				Console.SetCursorPosition(curchr.pos.x , curchr.pos.y);
				map[Pos2Index(curchr.pos)].type = BlockType.Player;
				Console.Write(curchr.style);
			}

			//check-loop
			while(true) CharacterMove();
		}

		public int Pos2Index(Pos pos)
		{
			return (pos.y - 1) * 10 + (pos.x - 2) / 2;
		}

		public void InitBlockList()
		{
			configPath = Environment.CurrentDirectory + "\\Config\\map001.txt";
			map = LoadController.Instance.LoadMapData(configPath);
		}

		public void CheckBlock(Pos nextPos)
		{
			int index = (nextPos.y - 1) * 10 + (nextPos.x - 2) / 2;
			if (map[index].style == '　'&& map[index].type!=BlockType.Player)
			{
				//change display
				Console.SetCursorPosition(chrBlock[pointer].pos.x , chrBlock[pointer].pos.y);
				Console.Write('　');
				Console.SetCursorPosition(nextPos.x , nextPos.y);
				Console.Write(chrBlock[pointer].style);

				//change style
				map[Pos2Index(chrBlock[pointer].pos)].type = BlockType.Null;//former->null
				chrBlock[pointer].pos = nextPos;
				map[Pos2Index(chrBlock[pointer].pos)].type = BlockType.Player;//latter->NPC
			}
		}

		public void CharacterMove()
		{
			if (!isTop) return;
			ConsoleKey cur_key = Console.ReadKey(true).Key;
			switch (cur_key)
			{
				//pointer in this Class equals chr's index
				case ConsoleKey.Tab:
					if (pointer < chrBlock.Count-1)
					{
						pointer++;
					}
					else
					{
						pointer = 0;
					}
					break;
				case ConsoleKey.W:
					Pos posW = new Pos(chrBlock[pointer].pos.x, chrBlock[pointer].pos.y-1);
					CheckBlock(posW);
					break;
				case ConsoleKey.S:
					Pos posS = new Pos(chrBlock[pointer].pos.x , chrBlock[pointer].pos.y + 1);
					CheckBlock(posS);
					break;
				case ConsoleKey.A:
					Pos posA = new Pos(chrBlock[pointer].pos.x - 2 , chrBlock[pointer].pos.y);
					CheckBlock(posA);
					break;
				case ConsoleKey.D:
					Pos posD = new Pos(chrBlock[pointer].pos.x + 2 , chrBlock[pointer].pos.y);
					CheckBlock(posD);
					break;
			}
		}
	}
}
