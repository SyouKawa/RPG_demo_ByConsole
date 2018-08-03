using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest.Controller {
    class LoadManager {
        private static LoadManager _instance;
            public static LoadManager Instance{
                get{
                    if(_instance==null)_instance=new LoadManager();
                    return _instance;
                }
            }
        public void LoadConfig(string file_text){
            
        }
    }
}
