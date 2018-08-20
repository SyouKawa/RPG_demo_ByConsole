using System;
using System.IO;
using System.Text;
using LitJson;
using System.Collections.Generic;
using RougelikeRPG.GameObject;

using System.Linq;
using System.Threading.Tasks;

namespace RougelikeRPG.Tool
{
    class Load
	{
		public enum ObjectMode
		{
			Null,
			Skill,
			Item,
			NPC,
			Monster,
			Player
		}

		//DataDecode Factory
		public static List<T> GetConfigFromFile<T>() {//NOT re-write at present
			Type temp = typeof(T);
			string path = Environment.CurrentDirectory + "\\"+temp.Name+"s.json";

			if (!File.Exists(path))
			{
				return null;
			}
			else
			{
				JsonData data=JsonMapper.ToObject(File.ReadAllText(path , Encoding.UTF8));
				List<T> dataList = new List<T>();
				for (int i = 0 ; i < data.Count ; i++)
				{
					T simpleData = JsonMapper.ToObject<T>(data[i].ToJson());
					dataList.Add(simpleData);
				}
				return dataList;
			}
        }

		#region Spawn and Add a Json-Data into File

		public static void AddSerializeJsonGameObject(Object obj,ObjectMode mode)
		{
			string jsonData = "";
			string path = mode.ToString()+"s.json";
			path = Environment.CurrentDirectory + "\\" + path;

			if (File.Exists(path))//文件存在，在末尾添加新的Json实例
			{
				FileStream file = new FileStream(path , FileMode.Open);
				file.Seek(-1 , SeekOrigin.End);

				switch (mode)
				{
					case ObjectMode.Item:
						Item item = obj as Item;
						jsonData = JsonMapper.ToJson(item);
						break;
					case ObjectMode.Skill:
						break;
					case ObjectMode.NPC:
						break;
				}
				jsonData = ",\n" + jsonData + "]";
				byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
				file.Write(byteData , 0 , byteData.Length);

				file.Close();
				file.Dispose();
			}
			else//文件不存在，创建新文件并写入Json字典
			{
				FileStream file = new FileStream(path , FileMode.Create);
				switch (mode)
				{
					case ObjectMode.Item:
						List<Item> dataList = new List<Item>();
						Item item = obj as Item;
						dataList.Add(item);
						jsonData = JsonMapper.ToJson(dataList);
						break;
					case ObjectMode.Skill:
						break;
					case ObjectMode.NPC:
						break;
				}
				byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
				file.Write(byteData , 0 , byteData.Length);

				file.Close();
				file.Dispose();
			}
		}
		
		#endregion

		#region LoadMapData Part
		/*
		public List<Block> LoadMapData(string path)
		{
			string[] mapData = File.ReadAllLines(path);
			List<Block> mapBlocks = new List<Block>();
			for (int i = 0 ; i < mapData.Length;i++)
			{
				char[] curLine = mapData[i].ToCharArray();
				for (int j=0;j<curLine.Length;j++)
				{
					Pos pos = new Pos((j*2+2) , i+1);// RowCol->X,Y
					Block tempCreateBlock = new Block();
					switch (curLine[j])
					{
						case '■':
							tempCreateBlock = new Block(pos , curLine[j] , BlockType.Wall);
							mapBlocks.Add(tempCreateBlock);
							break;
						case '★':
							tempCreateBlock = new Block(pos , curLine[j] , BlockType.Door);
							mapBlocks.Add(tempCreateBlock);
							break;
						case '▲':
							tempCreateBlock = new Block(pos , curLine[j] , BlockType.Box);
							mapBlocks.Add(tempCreateBlock);
							break;
						case '　':
							tempCreateBlock = new Block(pos , curLine[j] , BlockType.Null);
							mapBlocks.Add(tempCreateBlock);
							break;
						case '□':
							tempCreateBlock = new Block(pos , curLine[j] , BlockType.Door);
							mapBlocks.Add(tempCreateBlock);
							break;
						case '\n':
							tempCreateBlock = new Block(pos , curLine[j] , BlockType.Turn);
							mapBlocks.Add(tempCreateBlock);
							break;
						case '⊙':
							tempCreateBlock=new Block(pos , curLine[j] , BlockType.Monster);
							mapBlocks.Add(tempCreateBlock);
							tempCreateBlock.npc = LoadController.Instance.RollMonster();//return a monster
							break;
						case '¤':
							tempCreateBlock = new Block(pos , curLine[j] , BlockType.Item);
							mapBlocks.Add(tempCreateBlock);
							tempCreateBlock.item = new Item();
							break;
					}
					if (curLine[j] >= 'ァ' && curLine[j] <= 'ヶ')
					{
						tempCreateBlock = new Block(pos , curLine[j] , BlockType.NPC);
						mapBlocks.Add(tempCreateBlock);
						//TODO Roll a NPC from Manager.
						tempCreateBlock.npc = LoadController.Instance.RollNPC();//return a NPC
					}
				}
			}
			return mapBlocks;
		}
		*/
		#endregion
	}
}
