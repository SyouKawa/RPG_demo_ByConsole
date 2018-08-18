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

		#region Spawn a Template Json-Skill
		public static void SerializeSkillJsonTemplate()
		{
			//prepare for string(in Json)
			Skill template = new Skill();
			template.skillID = 0;
			template.skill_name = "name";
			template.cost = 10;
			template.damage = 10;
			template.isArmor = false;
			template.acc_opposite_fear = 0;
			template.acc_opposite_anger = 0;
			template.propertyRL = new int[4] { 0 , 0 , 0 , 0 };
			template.accPropertyValue = new int[4] { 0 , 0 , 0 , 0 };
			template.requireItems = new List<Item>();
			template.requireItems.Add(new Item("name1" , "descrp" , 1 , 1));
			template.requireItems.Add(new Item("name2" , "descrp" , 1 , 1));
			template.descrp = "descrp";
			template.info = "info";
			string jsondata = JsonMapper.ToJson(template);
			//Write into File as a template to use
			FileStream file = new FileStream(Environment.CurrentDirectory + "\\SkillTemplate.json" , FileMode.Create);
			byte[] bytesdata = Encoding.UTF8.GetBytes(jsondata);
			file.Write(bytesdata , 0 , bytesdata.Length);
			file.Close();
		}

		public static void SerializeNPCJsonTemplate()
		{
			Player template = new Player();
			//Default
			template.name = "name";
			template.HP = 100;
			template.MP = 100;
			template.Armor = 0;

			//Custom
			template.ownSkillID = new List<int>() { 1000 , 1000 };
			template.bag = new List<Item>() {new Item(),new Item()};
			//RL
			template.Property = new int[4] { 0,0,0,0};
			//template.buffs = new List<Buff>();
			string jsondata = JsonMapper.ToJson(template);
			FileStream file = new FileStream(Environment.CurrentDirectory + "\\NPCTemplate.json" , FileMode.Create);
			byte[] bytesdata = Encoding.UTF8.GetBytes(jsondata);
			file.Write(bytesdata , 0 , bytesdata.Length);
			file.Close();
			//return new Player(_name ,_HP ,_MP ,_Armor , List<int> _ownSkillID , List < Item > _bag , int[] _Property);
		}
		#endregion

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

		public List<Player> GetNPCsConfig(string path)
		{
			LoadConfigData(path);
			List<Player> NPCsList = new List<Player>();
			for (int i = 0 ; i < data.Count ; i++)
			{
				Player simpleNPC = DecodeSimpleNPC(data[i]);
				NPCsList.Add(simpleNPC);
			}
			return NPCsList;
		}

		public List<Player> GetMonsterConfig(string path)
		{
			LoadConfigData(path);
			List<Player> MonstersList = new List<Player>();
			for (int i = 0 ; i < data.Count ; i++)
			{
				Player simpleNPC = DecodeSimpleNPC(data[i]);
				MonstersList.Add(simpleNPC);
			}
			return MonstersList;
		}

		public Player DecodeSimpleNPC(JsonData npcData)
		{
			return JsonMapper.ToObject<Player>(npcData.ToJson());
		}

		public Skill DecodeSimpleSkill(JsonData skill_config_data)
		{
            int _skillID = (int)skill_config_data["skillID"];
            string _skill_name = skill_config_data["skill_name"].ToString();
			int _cost = (int)skill_config_data["cost"];
			int _damage = (int)skill_config_data["damage"];
			bool _isArmor = (bool)skill_config_data["isArmor"];
			int _acc_opposite_fear = (int)skill_config_data["acc_opposite_fear"];
			int _acc_opposite_anger = (int)skill_config_data["acc_opposite_anger"];

			JsonData temp = null;

			int[] _porpertyRL = null;
			temp = skill_config_data["propertyRL"];
			_porpertyRL = JsonMapper.ToObject<int[]>(temp.ToJson());

			int[] _accPropertyValue = null;
			temp = skill_config_data["accPropertyValue"];
			_accPropertyValue = JsonMapper.ToObject<int[]>(temp.ToJson());

			List<Item> _requireItems = null;
			temp = skill_config_data["requireItems"];
			_requireItems = JsonMapper.ToObject<List<Item>>(temp.ToJson());

			string _descrp=skill_config_data["descrp"].ToString();
			string _info = skill_config_data["info"].ToString();

			return new Skill(_skillID , _skill_name ,
				_cost , _damage , _isArmor,
				_acc_opposite_fear , _acc_opposite_anger ,
				_requireItems ,
				_porpertyRL[0] , _porpertyRL[1] , _porpertyRL[2] , _porpertyRL[3] ,
				_accPropertyValue[0] , _accPropertyValue[1] , _accPropertyValue[2] , _accPropertyValue[3],
				_descrp,_info);

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
		
		#endregion
	}
}
