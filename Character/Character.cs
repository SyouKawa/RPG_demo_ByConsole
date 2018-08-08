using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest {
class Character {
        public int cur_skill_index{get;set;}

        //Require input to Init
        public string name = "";
        public List<Skill> skill=new List<Skill>();
        public Career career = Career.INIT;

        public int HP = 100;
        public int MP = 100;
        public int anger_value = 0;
        public int fear_value = 0;
        public int fatigue_value=0;
        public Dictionary<string,int>Max_Limit=new Dictionary<string, int>();//TO-DO Json init
        public Dictionary<string,bool>buffDict=new Dictionary<string, bool>();
        
        public bool isDead = false;
        public Random random=new Random();
        public float sumDamage;

        public Character(){
            Max_Limit.Add("HP",200);
            Max_Limit.Add("MP",1000);
            Max_Limit.Add("anger_value",100);
            Max_Limit.Add("fatigue_value",100);
            Max_Limit.Add("fear_value",150);//de-buff could control

            buffDict.Add("Normal",true);
            buffDict.Add("Fatigue",false);
            buffDict.Add("Anger",false);
            buffDict.Add("Poisoning",false);
            buffDict.Add("Blessing",false);//Player init to true
            buffDict.Add("Fear",false);
        }

        public virtual void NormalAttack(int skill_index,ref Character hostile_chr) {
            
            cur_skill_index=skill_index;
            
            //cur_change_value: fatigue cur_buff
            
            //TO-DO Skill-RCC
            //TO-DO Settlement -Damage
            //if(skill[skill_index]) Calculate.ValueChangedBy(this,hostile_chr,ChangeValue.CostMP);
            //else Calculate.ValueChangedBy(this,hostile_chr,ChangeValue.AddFatigueValue);
            //TO-DO buff_change (for override)
            
            //hostile_change_value: HP anger fear cur_buff
            Calculate.ValueChangedBy(this,hostile_chr,ChangeValue.AddAngerValue);
            Calculate.ValueChangedBy(this,hostile_chr,ChangeValue.AddFearValue);
            Calculate.ValueChangedBy(this,hostile_chr,ChangeValue.MinusHP);
            //TO-DO buff_change (for override)
        }
        public virtual bool Get_isDead(){
            if(0==HP){ 
                isDead=true;
                return isDead;
            }else{ 
                return isDead;    
            }
             
        }
        public virtual void CheckStatus(){}//self-check

        public virtual void CheckBuff(){
            if(fatigue_value==Max_Limit["fatigue_value"]){
                buffDict["Fatigue"]=true;
            }
            if(fatigue_value==Max_Limit["fear_value"]){
                buffDict["Fear"]=true;
            }
            if(fatigue_value==Max_Limit["anger_value"]){
                buffDict["Anger"]=true;
            }
            //+ Other Inherit Buff Choices
        }
        public virtual void EnterBuff(){
            if(buffDict["Normal"]){
                sumDamage=0f;//TO-DO Normal Sum of Damage Formula
            }
            if(buffDict["Fatigue"]){
                sumDamage/=2;
            }
            if(buffDict["Fear"]){
                int tempRate=random.Next(0,10);
                if(tempRate<=5) sumDamage=0f;
            }
            if(buffDict["Anger"]){
                sumDamage*=1.2f;
            }
            if(buffDict["Poisoning"]){
                HP-=2;//TODO CLK
            }
        }


    }
}
