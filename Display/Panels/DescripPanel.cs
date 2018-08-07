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

        public DescripPanel(string _title,int _startX,int _startY,int _panelRow,int _panelCol)
        :base(_title,_startX,_startY,_panelRow,_panelCol){
            type=PanelType.Descrp;
            content="This is a test description.";
        }
    }
}
