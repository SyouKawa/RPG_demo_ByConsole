using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game_VSmode_verTest{
    class Player : Character {
		//TO-DO Json存档可读取
		//GrowTree growTree;
		//Equipment equipment;
		//int exp;
		//public List<Item> items;

		public Player(){
            name = "魔术师";
            //career = Career.UNEMPLOYED;
            ownSkillID.Add(1001);
            ownSkillID.Add(1002);
            ownSkillID.Add(1003);
            ownSkillID.Add(1004);
            
        }
		public Player(int a)
		{
			name = "钟表匠";
			//career = Career.UNEMPLOYED;
			ownSkillID.Add(1005);
			ownSkillID.Add(1006);
			ownSkillID.Add(1007);
			ownSkillID.Add(1008);

		}

		public Player(string _name,int _HP,int _MP,int _Armor,List<int> _ownSkillID,List<Item> _bag,int[] _Property)
		{
			name = _name;
			HP = _HP;
			MP = _MP;
			Armor = _Armor;
			ownSkillID = _ownSkillID;
			bag = _bag;
			Property = _Property;
		}

		private void GetDropItems(Character chr){

        }
        private void BuyItem(NPC npc,Item item) {
            
        }
        private void SellItem(NPC noc, Item item) {

        }
    }
}
