using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class Item {
        public string name;
        public string description;
        public int maxHold;
        public int curHold;

		public Item(){}
		public Item(string _name,string _description,int _maxHold,int _curHold){
            name=_name;
            description=_description;
            maxHold=_maxHold;
            curHold=_curHold;
        }
        public Item(string _findname){//TO-DO
            
        }
        public void UseItem(){
            if(curHold!=0){
                curHold--;
            }else{
                curHold=0;
            }
        }

    }
}
