using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class Panel{
        //frame-init
            //panel-name
        public string title;
            //position
        public int startX;
        public int startY;
            //size
        public int panelRow;
        public int panelCol;
            //Mode
        public PanelType type;
            //content
            /*in different inherit class*/ 
        public Panel(string _title,int _startX,int _startY,int _panelRow,int _panelCol){
            title=_title;
            startX=_startX;//usual: activemapCol+1
            startY=_startY;//usual: last panel +1 line to start
            panelRow=_panelRow;
            panelCol=_panelCol;
        }
        public virtual void Operate(){
            
        }
        
    }
}
