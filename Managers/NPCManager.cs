using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class NPCManager
	{
		private static NPCManager _instance;
		public static NPCManager Instance
		{
			get
			{
				if (_instance == null) _instance = new NPCManager(Environment.CurrentDirectory + "\\Config\\NPC_config.json");
				return _instance;
			}
		}

		public string configPath;
		public string monsterpath;
		public List<Player> allNPCs;
		public List<Player> allMonster;
		//public Dictionary<int , Block> NPCsDict;

		public NPCManager() { }
		public NPCManager(string _configPath)
		{
			configPath = _configPath;
			allNPCs = new List<Player>(LoadController.Instance.GetNPCsConfig(configPath));
			allMonster = new List<Player>(LoadController.Instance.GetMonsterConfig(Environment.CurrentDirectory + "\\Config\\Monster_config.json"));
			//NPCsDict = new Dictionary<int , Block>();
			//foreach (Block temp in allNPCs)
			//{
			//	NPCsDict.Add(temp.skillID , temp);
			//}
		}
	}
}
