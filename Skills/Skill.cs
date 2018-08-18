using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Game_VSmode_verTest{
    class Skill
    {
        public int skillID;
        public string skill_name;
        public int cost;
        public int damage;
		public bool isArmor;
        public int acc_opposite_fear;
        public int acc_opposite_anger;
        
		//fire,water,san,evil
        public int[] propertyRL;
		public int[] accPropertyValue;

		public List<Item> requireItems;

		public string descrp;
		public string info;

		#region Constructure
		public Skill()
		{

		}
        public Skill(Skill temp){
            skillID=temp.skillID;
            skill_name = temp.skill_name;
            cost = temp.cost;
            damage = temp.damage;
			isArmor = temp.isArmor;
			acc_opposite_fear = temp.acc_opposite_fear;
            acc_opposite_anger = temp.acc_opposite_anger;

			//最好不要指向，之后是整个拷贝作为新Skill的。

			requireItems = new List<Item>();
			foreach (Item curItem in temp.requireItems)
			{
				requireItems.Add(curItem);
			}

			propertyRL = new int[4];
			for (int i = 0 ; i < 4 ; i++)
			{
				propertyRL[i] = temp.propertyRL[i];
			}
			accPropertyValue = new int[4];
			for (int i = 0 ; i < 4 ; i++)
			{
				accPropertyValue[i] = temp.accPropertyValue[i];
			}
			descrp = temp.descrp;
			info = temp.info;
		}

        public Skill(int _skillID,string _skill_name ,
			int _cost,int _damage,bool _isArmor,
			int _acc_opposite_fear,int _acc_opposite_anger,
			List<Item> _requireItems,
			int _fireRL,int _waterRL,int _sanRL,int _evilRL,
			int _accFire,int _accWater,int _accSan,int _accEvil,
			string _descrp,string _info)
        {
            skillID=_skillID;
            skill_name = _skill_name;
            cost = _cost;
            damage = _damage;
			isArmor = _isArmor;
            acc_opposite_fear = _acc_opposite_fear;
            acc_opposite_anger = _acc_opposite_anger;

			requireItems = _requireItems;

			propertyRL = new int[4];
			propertyRL[0] = _fireRL;
			propertyRL[1] = _waterRL;
			propertyRL[2] = _sanRL;
			propertyRL[3] = _evilRL;

			accPropertyValue = new int[4];
			accPropertyValue[0] = _accFire;
			accPropertyValue[1] = _accWater;
			accPropertyValue[2] = _accSan;
			accPropertyValue[3] = _accEvil;

			descrp = _descrp;
			info = _info;
        }
		#endregion

	}
}
