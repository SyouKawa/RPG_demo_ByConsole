using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class DescripPanel:Panel{
        //content
        public string content;
        //public string configPath;

        public DescripPanel(string _title , Pos _startPos , Size _size,string _content)
		: base(_title , _startPos , _size)
		{
			type=PanelType.Descrp;
            content=_content;
        }

		public override void UpdateOptions()
		{
			string[] str=content.Split('\n');
			for (int i=0;i<str.Length;i++)
			{
				Console.SetCursorPosition(startPos.x + 2 , startPos.y + 2+i);
				Console.Write(str[i]);
			}
		}

		public override int OperateOption()
		{
			if (!isTop) return -1;
			ConsoleKey cur_key = Console.ReadKey(true).Key;
			if (cur_key == ConsoleKey.Enter || cur_key == ConsoleKey.Escape||cur_key==ConsoleKey.Spacebar)
			{
				controller.CloseCurPanel();
				return -1;
			}
			return -1;
		}
	}
}
