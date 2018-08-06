using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Game_VSmode_verTest{
    class MapManager {
        const int row=25;
        const int column=activemap_col+datapanel_col;
        const int datapanel_col=30;
        const int activemap_col=60;
        const int bagpanel_col=30;
        int player_pos_x;
        int player_pos_y;
        
        Box targetBox;
        Timer timer;
        Player player;
        DataPanel dataPanel;//count gameover HP,MP show
        Random seed=new Random();//random position seed

        Point[,] gameMap=new Point[row,column];

        StringBuilder mapBuffer=new StringBuilder();

        public void GetPlayerCurPos(){
            char cur_key=Console.ReadKey(true).KeyChar;
            switch(cur_key){//TO-DO check the limit condition.
                case 'w':
                    if(player_pos_x>1&&gameMap[player_pos_x-1,player_pos_y]==' ')
                        player_pos_x-=1;
                break;
                case 'a':
                    if(player_pos_y>1&&gameMap[player_pos_x,player_pos_y-1]==' ')
                        player_pos_y-=1;
                break;
                case 's':
                    if(player_pos_x<59&&gameMap[player_pos_x+1,player_pos_y]==' ')
                        player_pos_x+=1;
                break;
                case 'd':
                    if(player_pos_y<59&&gameMap[player_pos_x,player_pos_y+1]==' ')
                        player_pos_y+=1;
                break;
            }
        }
        
        public void GameStart(){
            InitTargetBox();
            InitPlayer();
            while(true){
                GetPlayerCurPos();
                DrawAllPanel();
            }
        }
        public void InitGameMap(){
            for(int i=0;i<row;i++){
                for(int j=0;j<column;j++){
                    gameMap[i,j]=' ';
                }
            }
        }

        public string ConvertNum2Pic(int num){
            int count=num/10;
            string temp_ref=new string('>',count);
            return temp_ref;
        }
        public void SetAString(int x_index,int y_index,char[] str){
            for(int i=0;i<str.Length;i++){
                gameMap[y_index,x_index+i]=str[i];
            }
        }

        public void InitMapPanel(){
            for(int i=0;i<activemap_col;i++) gameMap[0,i]='-';
            for (int lr_limit = 1; lr_limit < row; lr_limit++) {
                gameMap[lr_limit, 0] = '|';
                gameMap[lr_limit, activemap_col - 1] = '|';
            }
            for (int i=0;i<activemap_col;i++) gameMap[row-1,i]='-';
            
        }
        public void InitTargetBox(){
            int pos_x=seed.Next(1,row);
            int pos_y=seed.Next(1,activemap_col);
            gameMap[pos_x,pos_y]='o';
        }
        public void InitPlayer(){
            player_pos_x=seed.Next(1,row);
            player_pos_y=seed.Next(1,activemap_col);
            gameMap[player_pos_x,player_pos_y]='v';
        }
        //char[,] dataPanel=new char[,]{"Name:Alan".ToCharArray(),{},{},{},{}}
        public void InitDataPanel(){
            int x_start=activemap_col+1;
            int panel_row=10;
            int panel_col=28;
            //content-set
            SetAString(x_start+1,1,"Name:Alan".ToCharArray());
            SetAString(x_start+1,2,("HP:"+ConvertNum2Pic(90)).ToCharArray());
            SetAString(x_start+1,3,("MP:"+ConvertNum2Pic(70)).ToCharArray());
            SetAString(x_start+1,4,"Fatigue:0 Fear:0 Anger:0".ToCharArray());
            SetAString(x_start+1,5,"Skills:".ToCharArray());
            //bound-set
            for(int i=x_start;i<panel_col+x_start;i++) gameMap[0,i]='-';
            for (int lr_limit = 1; lr_limit < panel_row; lr_limit++) {
                gameMap[lr_limit, x_start] = '|';
                gameMap[lr_limit, x_start + panel_col - 1] = '|';
            }
            for(int i=x_start;i<panel_col+x_start;i++) gameMap[panel_row-1,i]='-';
        }

        public void InitBagPanel(){
            int x_start=activemap_col+1;
            int y_start=10;
            int panel_row=10;
            int panel_col=28;
            //content-set
            SetAString(x_start+1,panel_row+1,"    Bag".ToCharArray());
            SetAString(x_start+panel_col/2+1,panel_row+1,"    Box".ToCharArray());
            //bound-set
            for(int i=x_start;i<x_start+panel_col;i++) gameMap[y_start,i]='-';
            for (int i = y_start+1; i < y_start + panel_row; i++) {
                gameMap[i, x_start] = '|';
                gameMap[i,x_start+panel_col/2]='|';
                gameMap[i, x_start + panel_col - 1] = '|';
            }
            for (int i=x_start;i<x_start+panel_col;i++) gameMap[y_start+panel_row,i]='-';
        }

        public void DrawAllPanel(){
            mapBuffer.Clear();
            //Console.Clear();
            
            InitGameMap();
            InitMapPanel();
            gameMap[player_pos_x,player_pos_y]='v';
            //SConsole.SetCursorPosition
            //gameMap[10,10]='■';
             /*TODO-if(OpenState())*/InitDataPanel();
            /*TODO-if(OpenBag())*/InitBagPanel();
            
            for(int i=0;i<row;i++){
                for(int j=0;j<column;j++){
                    mapBuffer.Append(gameMap[i,j]);
                }
                mapBuffer.Append("\n");
            }
            Console.WriteLine(mapBuffer);
            Console.WriteLine("use WASD to move.");
        }

    }
}
