using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class Block
	{
		public Pos pos;
		public char style;
		public BlockType type;

		//opt-values
		public Character chr;
		public Box box;
		public Item item;

		public Block() {}
		public Block(Pos _pos,char _style,BlockType _type)
		{
			pos = _pos;
			style = _style;
			type = _type;
		}

		public void PickItem()
		{
			type = BlockType.Null;
		}
	}
}
