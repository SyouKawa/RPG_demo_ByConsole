using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Game_VSmode_verTest{
    class MapManager {
        const int row=13;
        const int column=activemap_col+datapanel_col+20;
        const int datapanel_col=15;
        const int activemap_col=30;
        const int bagpanel_col=15;
        int player_pos_x;
        int player_pos_y;
        
        Box targetBox;
        Timer timer;
        Player player;
        //Panel dataPanel;//count gameover HP,MP show
        Random seed=new Random();//random position seed

        //Point[,] gameMap=new Point[row,column];

        StringBuilder mapBuffer=new StringBuilder();

        public MapManager(){
            NewGameMap();
        }

        public void NewGameMap(){
            for(int i=0;i<row;i++){
                for(int j=0;j<column;j++){
                    gameMap[i,j]=new Point();
                }
            }
        }

        public void GetPlayerCurPos(){
            char cur_key=Console.ReadKey(true).KeyChar;
            switch(cur_key){//TO-DO check the limit condition.
                case 'w':
                    if(player_pos_x>1&&gameMap[player_pos_x-1,player_pos_y].display=="  ")
                        player_pos_x-=1;
                break;
                case 'a':
                    if(player_pos_y>1&&gameMap[player_pos_x,player_pos_y-1].display=="  ")
                        player_pos_y-=1;
                break;
                case 's':
                    if(player_pos_x<59&&gameMap[player_pos_x+1,player_pos_y].display=="  ")
                        player_pos_x+=1;
                break;
                case 'd':
                    if(player_pos_y<59&&gameMap[player_pos_x,player_pos_y+1].display=="  ")
                        player_pos_y+=1;
                break;
            }
        }
        
        public void GameStart(){
            InitTargetBox();
            InitPlayer();
            while(true){
                GetPlayerCurPos();
                //DrawAllPanel();
                DrawDataPanel();
            }
        }
        public void InitGameMap(){
            for(int i=0;i<row;i++){
                for(int j=0;j<column;j++){
                    gameMap[i,j].display="  ";
                }
            }
        }

        public string ConvertNum2Pic(int num){
            int count=num/10;
            string temp_ref=new string('>',count);
            return temp_ref;
        }
        public void SetAString(int x_index,int y_index,string str){
            gameMap[y_index,x_index].display=str;
            int temp_len=0;
            if(str.Length/2!=0) temp_len+=1;
            for(int i=y_index+1;i<=temp_len+2;i++){//
                gameMap[i,x_index].display="";
            }
        }
        public void InitMapPanel(){
            for(int i=0;i<activemap_col;i++) gameMap[0,i].display="一";
            for (int lr_limit = 1; lr_limit < row; lr_limit++) {
                gameMap[lr_limit, 0].display = "| ";
                gameMap[lr_limit, activemap_col - 1].display = "| ";
            }
            for (int i=0;i<activemap_col;i++) gameMap[row-1,i].display="一";
            
        }
        public void InitTargetBox(){
            int pos_x=seed.Next(1,row);
            int pos_y=seed.Next(1,activemap_col);
            gameMap[pos_x,pos_y].display="★";
        }
        public void InitPlayer(){
            player_pos_x=seed.Next(1,row);
            player_pos_y=seed.Next(1,activemap_col);
            gameMap[player_pos_x,player_pos_y].display="♀";
        }
        
        public void DrawDataPanel(){
            int x_start=activemap_col+1;//30+1
            int panel_row=8;//set to const in class
            int panel_col=30;
            List<string>showdata=new List<string>();
            showdata.Add("Name:Alan");
            showdata.Add("Name:Alan");//in DataPanel index begin from 1
            showdata.Add("HP:"+ConvertNum2Pic(90));
            showdata.Add("MP:"+ConvertNum2Pic(70));
            showdata.Add("Fatigue:0 Fear:0 Anger:0");
            showdata.Add("Skills:");
            for(int p_line=0;p_line<panel_row;p_line++){
                Console.SetCursorPosition(x_start,p_line);
                for(int i=panel_col;(p_line==0||p_line==panel_row-1)&&i>0;i-=2){
                    Console.Write("一");//the-first-line and the-last-line
                }
                if(p_line!=0){
                    Console.Write("|");
                    if(p_line<showdata.Count){//still exist content
                        Console.Write(showdata[p_line]);
                        string left_space=new string(' ',panel_col-showdata[p_line].Length-1);
                        Console.Write(left_space);
                        Console.Write("|");
                    }else if(p_line!=panel_row-1){//Content over but not the last-line
                        string left_space=new string(' ',panel_col-1);
                        Console.Write(left_space);
                        Console.Write("|");
                    }
                }
            }
        }
        public void DrawPanel(Panel panel){//TODO-Create a Class to save the former 4-args 
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
        public void DrawAllPanel(){
            mapBuffer.Clear();
            //Console.Clear();
            
            InitGameMap();
            InitMapPanel();
            gameMap[player_pos_x,player_pos_y].display="♀";
            //SConsole.SetCursorPosition
            gameMap[10,10].display="■";
             /*TODO-if(OpenState())*/ //InitDataPanel();
            /*TODO-if(OpenBag())*/ //InitBagPanel();
            
            for(int i=0;i<row;i++){
                for(int j=0;j<column;j++){
                    mapBuffer.Append(gameMap[i,j].display);
                }
                mapBuffer.Append("\n");
            }
            Console.WriteLine(mapBuffer);
            Console.WriteLine("use WASD to move.");
        }

    }
}
