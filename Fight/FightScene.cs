using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest{
class FightScene {
        bool isYourTurn=true;
        Character cur_atk_chr;
        Character cur_hostile_chr;

        public FightScene(Character _cur_atk_chr,Character _cur_hostile_chr){
            cur_atk_chr = _cur_atk_chr;
            cur_hostile_chr = _cur_hostile_chr;
        }
        
        void PriorityChoose(Character cur_atk_chr,Character cur_hostile_chr) {
            if (cur_hostile_chr.HP < cur_atk_chr.HP) isYourTurn = false;
            else isYourTurn = true;
        }

        void SpawnARound(Character cur_atk_chr,Character cur_hostile_chr){ 
            
        }

        public void FightOne2One(Character cur_atk_chr,Character cur_hostile_chr) {
            PriorityChoose(cur_atk_chr,cur_hostile_chr);
            while (!cur_atk_chr.Get_isDead() && !cur_hostile_chr.Get_isDead()) {
                if (isYourTurn)
                {
                    //TO-DO Display SkillList
                    //TO-DO Choose a Skill_index

                    //TO-DO read from skillpanel
                    cur_atk_chr.Attack(1001);                
                    cur_hostile_chr.BeHit(1001);
                    isYourTurn=false;
                    System.Threading.Thread.Sleep(1000);
                }
                else {
                    cur_hostile_chr.CheckStatus();
                    cur_hostile_chr.Attack(1002);
                    cur_atk_chr.BeHit(1002);
                    isYourTurn=true;
                    System.Threading.Thread.Sleep(1000);
                }
            }
            if(!cur_atk_chr.Get_isDead()) Console.WriteLine("You win.");
            else Console.WriteLine("You Lose.");
            Console.ReadLine();
        }
        //Character[] left_camp = new Character[4];
        //Character[] right_camp = new Character[4];

    }
}
