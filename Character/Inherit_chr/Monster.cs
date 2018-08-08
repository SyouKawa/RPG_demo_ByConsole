using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class Monster : Character {
        Item[] dropitems = new Item[5];
        enum Status{
            Null,
            Waitting,
            Escape,
            Dead,
            Follow,
            Patrol,
            Attack
        }
        private Status curStatus;
        
        public Monster(){}
        public override void CheckStatus() {
            switch(curStatus){
                case Status.Null:
                    
                break;
            }
        }
    }
}
