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
		public List<Block> npcSaveData;
		public Dictionary<int , List<Block>> teamDict;//int ->pointer.
		public string configPath;
		public bool isRuning;//prevent running-update refreshed by txt

		public MapPanel(string _title , Pos _startPos , Size _size)
		: base(_title , _startPos , _size)
		{
			type = PanelType.Map;
			isHorizon = true;
			isRuning = false;
			//default add Player
			map = new List<Block>();
			chrBlock = new List<Block>();
			chrBlock.Add(new Block(new Pos(4 , 9) , '术' , BlockType.Player));
			chrBlock[0].npc = new Player();
			chrBlock.Add(new Block(new Pos(6 , 9) , '匠' , BlockType.Player));
			chrBlock[1].npc = new Player(1);

			pointer = 0;

			//default init player's teamlist
			teamDict = new Dictionary<int , List<Block>>();
			for (int i=0 ;i<chrBlock.Count ;i++ )
			{
				List<Block> newlist = new List<Block>();
				teamDict.Add(i , newlist);
				newlist.Add(chrBlock[i]);//add self.
			}

			//default add NPCs(Block)
			npcSaveData = new List<Block>();
			//in InitBlockList();
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
			//while(controller.panels.Peek()==this) ControlCheck();
		}

		public int Pos2Index(Pos pos)
		{
			return (pos.y - 1) * 10 + (pos.x-2 ) / 2;
		}

		public void InitBlockList()
		{
			if (!isRuning)
			{
				configPath = Environment.CurrentDirectory + "\\Config\\map001.txt";
				map = LoadController.Instance.LoadMapData(configPath);
				foreach (Block curBlock in map)
				{
					if (curBlock.type == BlockType.NPC)
					{
						npcSaveData.Add(new Block(curBlock));
					}
				}
			}
			isRuning = true;

		}

		public void CheckBlock(Pos nextPos)
		{
			int index = (nextPos.y - 1) * 10 + (nextPos.x-2) / 2;
			//kinds of Block Check and effect.
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
			if (map[index].style == '▲')
			{
				//controller.OpenPanel()
			}
			if (map[index].style == '⊙')
			{
				ConsoleKey cur_key = Console.ReadKey(true).Key;
				if (cur_key == ConsoleKey.Enter)
				{
					//Create a FightPanel with current Player-info
					Fight curFight = new Fight();
					{
						curFight.playerTeam.Add(new Player());
						curFight.playerTeam.Add(new Player(1));//TODO NPC in team.
					}
					controller.OpenPanel(new FightPanel("F i g h t " , new Pos(0 , 0) , new Size(45 , 30) , curFight));
				}
			}
			if (map[index].type==BlockType.NPC)
			{
				ConsoleKey cur_key = Console.ReadKey(true).Key;
				if (cur_key == ConsoleKey.Enter)
				{
					if (teamDict[pointer].Count < 3)
					{
						//Create a MessagePanel with current Player-info
						DescripPanel Info = new DescripPanel("I n f o " , new Pos(10 , 10) , new Size(10 , 4) , map[index].npc.name + "加入了队伍");
						controller.OpenPanel(Info);
						//updata team-info
						teamDict[pointer].Add(new Block(map[index]));

						//update block-info
						map[index].style = '　';
						map[index].type = BlockType.Null;
						map[index].npc = null;

						UpdateOptions();
						controller.OnlyDrawTopPanel();
					}
					else
					{
						DescripPanel Info = new DescripPanel("I n f o " , new Pos(10 , 10) , new Size(10 , 4) ,"队伍已满");
						controller.OpenPanel(Info);
					}
				}
			}
		}

		public override int OperateOption()
		{
			if (!isTop) return-1;
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
				case ConsoleKey.T:
					controller.OpenPanel(new TeamPanel("T e a m " , new Pos(10 , 10) , new Size(23 , 6) , this));
					break;
				case ConsoleKey.W:
					Pos posW = new Pos(chrBlock[pointer].pos.x, chrBlock[pointer].pos.y-1);
					CheckBlock(posW);
					Console.SetCursorPosition(80 , 3);
					Console.Write(chrBlock[pointer].pos.x.ToString() + "," + chrBlock[pointer].pos.y.ToString() + "  ");
					foreach (Block i in map)
					{
						if (i.style == '★')
						{
							Console.SetCursorPosition(80 , 4);
							Console.Write(i.pos.x.ToString()+"," + i.pos.y.ToString());
						}
					}
					break;
				case ConsoleKey.S:
					Pos posS = new Pos(chrBlock[pointer].pos.x , chrBlock[pointer].pos.y + 1);
					CheckBlock(posS);
					Console.SetCursorPosition(80 , 3);
					Console.Write(chrBlock[pointer].pos.x.ToString() + "," + chrBlock[pointer].pos.y.ToString()+"  ");
					break;
				case ConsoleKey.A:
					Pos posA = new Pos(chrBlock[pointer].pos.x - 2 , chrBlock[pointer].pos.y);
					CheckBlock(posA);
					Console.SetCursorPosition(80 , 3);
					Console.Write(chrBlock[pointer].pos.x.ToString() + "," + chrBlock[pointer].pos.y.ToString() + "  ");
					break;
				case ConsoleKey.D:
					Pos posD = new Pos(chrBlock[pointer].pos.x + 2 , chrBlock[pointer].pos.y);
					CheckBlock(posD);
					//Console.SetCursorPosition(80 , 3);
					//Console.Write(chrBlock[pointer].pos.x.ToString() + "," + chrBlock[pointer].pos.y.ToString() + "  ");
					break;
				case ConsoleKey.Escape:
					ResetOutputStyle();
					controller.OpenPanel(new GlobalStaticPanel(PanelType.Menu));
					break;
			}
			return -1;
		}
	}
}
