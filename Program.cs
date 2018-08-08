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
            Monster slem = new Monster();
            FightScene fight_scene=new FightScene(player,slem);
            fight_scene.FightOne2One(player,slem);
            
            //Console.CursorVisible=false;
            //DisplayController.Instance.OpenNewPanel(new SkillPanel("Skill",62,1,10,21));
            //Console.Write(GlobalSkillsManager.Instance.allSkills[0].skill_name);
            //while(true){
            //    Object temp = DisplayController.Instance.OperateCurPanel();
            //    if(temp!=null){
            //        //TO-DO different Panel Effect.
            //        Console.WriteLine("You have used "+((Skill)temp).skill_name);
            //        break;
            //    }
            //}
            Console.ReadKey();
        }
    }
}
