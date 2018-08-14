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

		//Json-Data
        public List<Skill> GetSkillsConfig(string path){
            return new LoadManager().GetSkillsConfig(path);
        }

		//TxT-Data
		public List<Block> LoadMapData(string path)
		{
			return new LoadManager().LoadMapData(path);
		}

	}
}
