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
                    cur_atk_chr.NormalAttack(0,ref cur_hostile_chr);//TO-DO GetKeyDown re-write param[0]                 
                    isYourTurn=false;
                    Console.WriteLine("{0} was attacked by {1}'s {2}.Lost {3} HP",cur_atk_chr.name,cur_hostile_chr.name,cur_hostile_chr.skill[0].skill_name,cur_hostile_chr.skill[0].damage);
                    System.Threading.Thread.Sleep(1000);
                }
                else {
                    cur_hostile_chr.NormalAttack(0,ref cur_atk_chr);
                    Console.WriteLine("{0} was attacked by {1}'s {2}.Lost {3} HP",cur_hostile_chr.name,cur_atk_chr.name,cur_atk_chr.skill[0].skill_name,cur_atk_chr.skill[0].damage);
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
