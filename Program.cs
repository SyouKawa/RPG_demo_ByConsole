using System;
using System.Collections.Generic;
using System.Text;
using Game_VSmode_verTest;

namespace Game_VSmode_verTest
{
    class Program
    {
        static void Main(string[] args){
            Player player = new Player();
            Monster slem = new Monster();//TO-DO Factory
            FightScene fight_scene=new FightScene(player,slem);
            fight_scene.FightOne2One(player,slem);
        }
    }
}
