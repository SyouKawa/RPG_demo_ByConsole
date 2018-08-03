using System;
using System.Collections.Generic;
using System.Text;
using Game_VSmode_verTest;
using Game_VSmode_verTest.Tool;

namespace Game_VSmode_verTest
{
    class Program
    {
        static void Main(string[] args){
            //Player player = new Player();
            //Monster slem = new Monster();//TO-DO Factory
            //FightScene fight_scene=new FightScene(player,slem);
            //fight_scene.FightOne2One(player,slem);

            string path=Environment.CurrentDirectory+"\\Config\\Skill_config.json";
            List<Skill>test=new List<Skill>(LoadManager.Instance.GetConfigFromFile(path)as List<Skill>);

            Console.ReadKey();
        }
    }
}
