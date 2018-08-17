using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class DescripPanel:Panel{
        //content
        public string shortContent;
        //public string configPath;

        public DescripPanel(string _title , Pos _startPos , Size _size)
		: base(_title , _startPos , _size)
		{
			type=PanelType.Descrp;
            shortContent="This is a test description.";
        }
    }
}
