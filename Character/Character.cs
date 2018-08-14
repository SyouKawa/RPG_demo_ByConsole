using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_VSmode_verTest {
class Character {

        //Require input to Init
        public string name = "";
        public List<int> ownSkillID=new List<int>();
        public Career career = Career.INIT;

        public int HP = 100;
        public int MP = 100;
        public int anger_value = 0;
        public int fear_value = 0;
        public int fatigue_value=0;
        public Dictionary<string,int>Max_Limit=new Dictionary<string, int>();//TO-DO Json init
        public Dictionary<string,bool>buffDict=new Dictionary<string, bool>();
        
        public Skill settle_skill_instance;
        public bool isDead = false;
        public Random random=new Random();

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

        public virtual bool Get_isDead(){
            if(0==HP){ 
                isDead=true;
                return isDead;
            }else{ 
                return isDead;    
            }
             
        }

        public virtual void Attack(int skillID){
            GetSettleSkillPrototype(skillID);
            SkillSettlement(ref settle_skill_instance);
            if(!GetSkillRCC(settle_skill_instance))return;
            UpdateFightDataPositive(settle_skill_instance);
        }
        public virtual void BeHit(int  skillID){
            GetSettleSkillPrototype(skillID);
            SkillSettlement(ref settle_skill_instance);
            UpdateFightDataPassive(settle_skill_instance);
        }
		public virtual void UseItem(int itemID)
		{

		}

        #region Skill Release + beHit Implement

        public virtual void GetSettleSkillPrototype(int skillID){
            settle_skill_instance=GlobalSkillsManager.Instance.GetSkill(skillID);
        }
        
        public virtual bool GetSkillRCC(Skill newskill){
            return true;
        }

        public virtual void UpdateFightDataPositive(Skill settleSkill){
            if(settleSkill.type.isMagic) SetNumVal.SetNumValBy(this,settleSkill,OperateType.CostMP);
            else SetNumVal.SetNumValBy(this,settleSkill,OperateType.AddFatigueValue);
        }
        public virtual void UpdateFightDataPassive(Skill settleSkill){
            SetNumVal.SetNumValBy(this,settleSkill,OperateType.AddAngerValue);
            SetNumVal.SetNumValBy(this,settleSkill,OperateType.AddFearValue);
            SetNumVal.SetNumValBy(this,settleSkill,OperateType.MinusHP);
        }

        public virtual void SkillSettlement(ref Skill curSetSkill){
            if(buffDict["Normal"]){
                curSetSkill.damage+=0;
            }
            if(buffDict["Fatigue"]){
                curSetSkill.damage/=2;
                curSetSkill.cost+=10;
            }
            if(buffDict["Fear"]){
                int tempRate=random.Next(0,10);
                if(tempRate<=5){
                    curSetSkill.cost+=20;
                    if(tempRate<3){
                        //TODO-SAN=0 // player cant move when return Map
                        if(tempRate==1) curSetSkill.damage=0;
                    }
                }
            }
            if(buffDict["Anger"]){
                curSetSkill.damage*=2;
            }
            if(buffDict["Poisoning"]){
                HP-=2;//TODO CLK
            }
        }

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
		#endregion

		public string ConvertNum2Pic(int num)
		{
			int count = num / 10;
			string temp_ref = new string('>' , count);
			return temp_ref;
		}

		public virtual void PrintData()
		{
			int posX = Console.CursorLeft;
			int posY = Console.CursorTop;
			Console.WriteLine(name);
			Console.SetCursorPosition(posX , posY+1);
			Console.WriteLine("HP:" + ConvertNum2Pic(90));
			Console.SetCursorPosition(posX, posY+2);
			Console.WriteLine("MP:" + ConvertNum2Pic(70));
			Console.SetCursorPosition(posX , posY + 3);
			Console.WriteLine(career.ToString());

		}

		public virtual List<string> GetChrData()
		{
			List<string> list = new List<string>();
			list.Add(name+"\n");
			list.Add("HP:" + ConvertNum2Pic(90)+"\n");
			list.Add("MP:" + ConvertNum2Pic(70)+"\n");
			list.Add(career.ToString()+"\n");

			return list;
		}

        public virtual void CheckStatus(){}//self-check
    }
}
