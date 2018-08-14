using System;
using System.IO;
using System.Text;
using LitJson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game_VSmode_verTest {
    class LoadManager {

        private JsonData data;
        public void LoadConfigData(string file_text) {
            data = JsonMapper.ToObject(File.ReadAllText(file_text, Encoding.UTF8));
        }

        //DataDecode Factory
        public Object GetConfigFromFile(string path, LoadMode mode) {//NOT re-write at present
            switch (mode) {
                case LoadMode.Skill: return GetSkillsConfig(path);
                case LoadMode.Item: return null;
                case LoadMode.Player: return null;
            }
            return null;
        }

        #region Different Data Decode-Implement in detail

        public List<Skill> GetSkillsConfig(string path) {
            LoadConfigData(path);
            List<Skill> temp_skill_list_ref = new List<Skill>();
            for (int i = 0; i < data.Count; i++) {
                Skill temp_skill_ref = DecodeSimpleSkill(data[i]);
                temp_skill_list_ref.Add(temp_skill_ref);
            }
            return temp_skill_list_ref;
        }

        public Skill DecodeSimpleSkill(JsonData skill_config_data) {
            SkillType temp_type_ref = null;
            int _skillID = (int)skill_config_data["skillID"];
            string _skill_name = skill_config_data["skill_name"].ToString();
            JsonData temp_type = skill_config_data["type"];
            {
                bool _isPositive = (bool)temp_type["isPositive"];
                bool _isMagic = (bool)temp_type["isMagic"];
                bool _isAOE = (bool)temp_type["isAOE"];
                bool _isConsume = (bool)temp_type["isConsume"];
                temp_type_ref = new SkillType(_isPositive, _isMagic, _isAOE, _isConsume);
            }
            int _cost = (int)skill_config_data["cost"];
            int _damage = (int)skill_config_data["damage"];
            int _acc_opposite_fear = (int)skill_config_data["acc_opposite_fear"];
            int _acc_opposite_anger = (int)skill_config_data["acc_opposite_anger"];
            return new Skill(_skillID, _skill_name, temp_type_ref, _cost, _damage, _acc_opposite_fear, _acc_opposite_anger);
        }
		#endregion

		#region LoadMapData Part

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
					switch (curLine[j])
					{
						case '■':
							mapBlocks.Add(new Block(pos , curLine[j] , BlockType.Wall));
							break;
						case '★':
							mapBlocks.Add(new Block(pos , curLine[j] , BlockType.Door));
							break;
						case '▲':
							mapBlocks.Add(new Block(pos , curLine[j] , BlockType.Box));
							break;
						case '　':
							mapBlocks.Add(new Block(pos , curLine[j] , BlockType.Null));
							break;
						case '□':
							mapBlocks.Add(new Block(pos , curLine[j] , BlockType.Door));
							break;
						case '\n':
							mapBlocks.Add(new Block(pos , curLine[j] , BlockType.Turn));
							break;
					}
					if (curLine[j] > 'ァ' && curLine[j] < 'ヶ')
					{
						mapBlocks.Add(new Block(pos , curLine[j] , BlockType.NPC));
					}
				}
			}
			return mapBlocks;
		}
		
		#endregion
	}
}
