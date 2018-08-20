using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RougelikeRPG.Tool;
using RougelikeRPG.Character;

namespace RougelikeRPG.GameObject
{
	class Item
	{
		public string name
		{
			get;
		}
		public string descrp;
		public string useEffectInfo;//init after Use()

		public char pic;
		public int maxHold;
		public int curHold;
		public bool isUnique;

		//Custom Data

		#region Contructure
		public Item() {}
		public Item(string _name,string _descrp,char _pic,int _maxHold,int _curHold)
		{
			name = _name;
			descrp = _descrp;
			pic = _pic;
			maxHold = _maxHold;
			curHold = _curHold;

			//Default-Init-Variable
			if (maxHold == 1)
			{
				isUnique = true;
			}
			else
			{
				isUnique = false;
			}

			useEffectInfo = "default useEffectInfo";
		}
		public Item(Item item)
		{
			name = item.name;
			descrp = item.descrp;
			pic = item.pic;
			maxHold = item.maxHold;
			curHold = item.curHold;
			isUnique = item.isUnique;

			useEffectInfo = "default useEffectInfo";
		}
		#endregion

		#region Override Base Func

		public override bool Equals(object obj)
		{
			Item compare = obj as Item;
			if (name == compare.name)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region Method

		public void Use()
		{
			if (!isUnique)
			{
				curHold -= 1;
			}
		}

		public void PrintInFormat(string data,Pos pos)
		{

		}

		public void UseEffect(Player player)//kinds re-write
		{

		}

		public void UseEffect(List<Player> allteam)
		{

		}
		#endregion

	}
}
