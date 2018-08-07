using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
    class DisplayController {
        private static DisplayController _instance;
        public static DisplayController Instance{
            get{
                if(_instance==null) _instance=new DisplayController();
                return _instance;
            }
        }

        public Stack<Panel> panel=new Stack<Panel>();

        public enum PanelMode{
            Skill,
            Map,
            Bag,
            Menu
        }

        public static void OpenNewPanel(PanelMode panelMode){
            switch(panelMode){
                case PanelMode.Skill:
                break;
                case PanelMode.Map:
                break;
                case PanelMode.Bag:
                break;
                case PanelMode.Menu:
                break;
            }
        }

        public static void CloseCurPanel(){
            _instance.panel.Pop();
            Console.Clear();
            foreach(Panel p in _instance.panel){
                DrawPanel(p);
            }
        }

        public static void DrawPanel(Panel panel){//TODO-Create a Class to save the former 4-args 
            List<string>showdata=new List<string>();//read from JsonFile
            showdata.Add("Ice Storm");
            showdata.Add("Shadow of Gloom");
            showdata.Add("Hit with MagicWand");
            showdata.Add("Curse of BloodMoon");
            Console.SetCursorPosition(panel.startX,panel.startY);
            
            int simple_space_count=(panel.panelCol-panel.title.Length)/2/2;
            string temp_space1=new string('-',simple_space_count);
            string temp_space2=new string(' ',simple_space_count);
            Console.Write(temp_space1+temp_space2+panel.title+temp_space2+temp_space1);

            for(int p_line=panel.startY+1,data_line=0;p_line<panel.startY+panel.panelRow;p_line++,data_line++){
                Console.SetCursorPosition(panel.startX,p_line);
                if(p_line!=panel.startY+panel.panelRow-1) Console.Write("|");//except the last line
                if(data_line<showdata.Count){//still exist content

                    Console.BackgroundColor=ConsoleColor.DarkYellow;
                    Console.ForegroundColor=ConsoleColor.Black;
                    Console.Write(showdata[data_line]);
                    Console.BackgroundColor=ConsoleColor.Black;
                    Console.ForegroundColor=ConsoleColor.White;
                    string left_space=new string(' ',panel.panelCol-showdata[data_line].Length-1);
                    Console.Write(left_space);
                    Console.Write("|");
                }else if(p_line!=panel.startY+panel.panelRow-1){//Content over but not the last-line
                    string left_space=new string(' ',panel.panelCol-1);
                    Console.Write(left_space);
                    Console.Write("|");
                }else if(p_line==panel.startY+panel.panelRow-1){
                    for(int i=panel.panelCol;i>0;i--){
                        Console.Write("-");//the-last-line
                    }
                }
            }
        }
    }
}
