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
		public Player npc;
		public Box box;
		public Item item;

		public Block() {}
		public Block(Pos _pos,char _style,BlockType _type)
		{
			pos = _pos;
			style = _style;
			type = _type;
		}
		public Block(Block block)
		{
			pos = block.pos;
			style = block.style;
			type = block.type;

			//DONT use ref ,use new(TODO)
			npc = new Player();
			item = block.item;
			box = block.box;
		}

		public void PickItem()
		{
			type = BlockType.Null;
		}
	}
}
