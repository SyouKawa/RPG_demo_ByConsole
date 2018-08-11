using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class Panel{
        public string title;
		public Pos startPos;
		public Size size;
        public PanelType type;
		public List<OptionObject> options;
		public int pointer;
		public bool isHorizon;

		public Panel(string _title,Pos _startPos,Size _size)
		{
            title=_title;
			startPos=_startPos;
			size = _size;

			//Default
			type = PanelType.Null;
			options = new List<OptionObject>();
			pointer = 2;
			isHorizon = false;
        }

		public void Draw()
		{
			DrawFrame();
			FillOptionContent();
			UpdateOptions();
		}
		#region Draw Implement 
		public void DrawFrame()
		{
			Console.SetCursorPosition(startPos.x + 1 , startPos.y);//panel.startX+1 : prevent the first '-' beyond width
			int simple_space_count = (size.widthCol * 2 - title.Length) / 2 / 2;
			string temp_space1 = new string('━' , simple_space_count);
			string temp_space2 = new string(' ' , simple_space_count);
			Console.Write(temp_space1 + temp_space2 + title + temp_space2 + temp_space1);

			for (int p_line = startPos.y + 1 ; p_line < startPos.y + size.heightRow ; p_line++)
			{
				Console.SetCursorPosition(startPos.x , p_line);
				if (p_line != startPos.y + size.heightRow - 1)
				{
					Console.Write("‖");//except the last line
					string space = new string(' ' , size.widthCol * 2 - 2);
					Console.Write(space);
					Console.Write("‖");
				}
				else if (p_line == startPos.y + size.heightRow - 1)
				{
					Console.CursorLeft += 1;
					for (int i = size.widthCol * 2 - 1 ; i > 0 ; i--)
					{
						Console.Write("━");//the-last-line
					}
				}
			}
		}

		public virtual void UpdateOptions()
		{
			foreach (OptionObject opt in options)
			{
				opt.Draw(pointer);
			}
		}
		#endregion

		public virtual void MoveBetweenOptions() {}

		public virtual void OperateOption() {}

		public virtual void FillOptionContent() {}
    }
}
