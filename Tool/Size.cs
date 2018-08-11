using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class Size
	{
		public int widthCol;
		public int heightRow;

		public Size() { }
		public Size(int _width,int _height)
		{
			widthCol = _width;
			heightRow = _height;
		}
		public Size(Size size)
		{
			widthCol = size.widthCol;
			heightRow = size.heightRow;
		}
	}
}
