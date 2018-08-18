using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Game_VSmode_verTest.Tool;

namespace Game_VSmode_verTest{
    class LoadController {
        private static LoadController _instance;
        public static LoadController Instance{
            get{
                if(_instance==null) _instance=new LoadController();
                return _instance;
            }
        }
		Random random = new Random();


		//Json-Data
        public List<Skill> GetSkillsConfig(string path){
            return new LoadManager().GetSkillsConfig(path);
        }

		public List<Player> GetNPCsConfig(string path)
		{
			return new LoadManager().GetNPCsConfig(path);
		}

		public List<Player> GetMonsterConfig(string path)
		{
			return new LoadManager().GetMonsterConfig(path);
		}

		//TxT-Data
		public List<Block> LoadMapData(string path)
		{
			return new LoadManager().LoadMapData(path);
		}

		public Player RollMonster()
		{
			int range = NPCManager.Instance.allMonster.Count;
			int index = random.Next(2 , range);
			return NPCManager.Instance.allMonster[index];
		}

		public Player RollNPC()
		{
			int range = NPCManager.Instance.allNPCs.Count;
			int index = random.Next(2 , range);
			return NPCManager.Instance.allNPCs[index];
		}

	}
}
