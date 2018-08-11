using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class Pos
	{
		public int x;
		public int y;

		public Pos() {}

		public Pos(int _x,int _y)
		{
			x = _x;
			y = _y;
		}

		public Pos(Pos pos)
		{
			x = pos.x;
			y = pos.y;
		}

		public static Pos operator +(Pos pos1,Pos pos2)
		{
			return new Pos(pos1.x + pos2.x , pos1.y + pos2.y);
		}

		public static Pos operator -(Pos pos1 , Pos pos2)
		{
			return new Pos(pos1.x - pos2.x , pos1.y - pos2.y);
		}

		public static bool operator ==(Pos pos1,Pos pos2)
		{
			return (pos1.x == pos2.x && pos1.y == pos2.y);
		}

		public static bool operator !=(Pos pos1 , Pos pos2)
		{
			return (pos1.x != pos2.x || pos1.y != pos2.y);
		}
	}
}

