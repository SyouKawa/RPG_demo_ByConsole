using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest
{
	class OptionObject
	{
		//be used convert object to string to display.
		public int index;
		public Pos startPos;
		public Size size;
		public List<string> content=new List<string>();

		public OptionObject() {}
		public OptionObject(Pos _startPos,Size _size,List<string> _content)
		{
			startPos = _startPos;
			size = _size;//depends on content length.
			content = _content;
		}

		public void DrawVertical(int curPointer)
		{
			//Move to Correct Position
			Console.SetCursorPosition(startPos.x , startPos.y);

			//Normal output
			for (int i=0;i<content.Count;i++)
			{
				//(Selected)Change Background
				if (curPointer == index)
				{
					Pos prePosition = new Pos(Console.CursorLeft , Console.CursorTop);
					Console.BackgroundColor = ConsoleColor.Magenta;
					for (int line = 0 ; line < size.heightRow ; line++)
					{
						for (int col = 0 ; col < size.widthCol ; col++)
						{
							Console.Write(" ");
						}
					}
					//Cursor-Position changed require to be reset.
					Console.SetCursorPosition(prePosition.x , prePosition.y);
					Console.Write(content[i]);
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					continue;
				}
				Console.Write(content[i]);
			}
		}

		public void DrawHorizon(int curPointer)
		{
			//Move to Correct Position
			Console.SetCursorPosition(startPos.x , startPos.y);

			//for content print
			Pos prePosition = new Pos(Console.CursorLeft , Console.CursorTop);
			//for background print
			Pos tempPosition = new Pos(Console.CursorLeft , Console.CursorTop);

			//(Selected)Change Background
			if (curPointer == index)
			{
				Console.BackgroundColor = ConsoleColor.Magenta;
				for (int line = 0 ; line < content.Count ; line++)//size.heightRow
				{
					for (int col = 0 ; col < content[line].Length-1 ; col++)
					{
						Console.Write(" ");
					}
					Console.SetCursorPosition(tempPosition.x , tempPosition.y+line+1);
				}
			}

			//Normal output
			Console.SetCursorPosition(prePosition.x , prePosition.y);
			for (int i = 0 ; i < content.Count ; i++)
			{
				if(curPointer != index) Console.BackgroundColor = ConsoleColor.Black;
				Console.Write(content[i]);
				Console.SetCursorPosition(prePosition.x , prePosition.y + i+1);
			}
		}
	}
}
