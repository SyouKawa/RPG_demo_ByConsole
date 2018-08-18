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
		public List<Item> bag = new List<Item>();
        //public Career career = Career.INIT;

        public int HP = 100;
        public int MP = 100;//merge with Fatigue
		public int Armor = 0;
        public int anger_value = 0;
        public int fear_value = 0;
        public Dictionary<string,int>Max_Limit=new Dictionary<string, int>();//TO-DO Json init
        public List<Buff>buffs=new List<Buff>();
		public int[] Property=new int[4] { 1,1,1,1};

		public Skill settleTempSkill;
        public bool isDead = false;
        public Random random=new Random();

        public Character(){
			//default
			Max_Limit.Add("HP",200);
            Max_Limit.Add("MP",200);
            Max_Limit.Add("anger_value",100);
            Max_Limit.Add("fear_value",150);//de-buff could control

        }

        public virtual bool Get_isDead(){
            if(HP<=0){ 
                isDead=true;
                return isDead;
            }else{ 
                return isDead;
            }
             
        }

        public virtual Skill Attack(int skillID){
            GetSettleSkillPrototype(skillID);
            SkillSettlement(settleTempSkill);
			string RCCres = GetSkillRCC(settleTempSkill);
			if (RCCres[0] == '0')
			{
				Skill fail = new Skill();
				fail.skill_name = RCCres;
				return fail;
			}
            UpdateFightDataPositive(settleTempSkill);
			return settleTempSkill;
		}
        public virtual void BeHit(Skill changedSkill){
            SkillSettlement(changedSkill);
            UpdateFightDataPassive(changedSkill);
        }
		public virtual Skill UseItem(int skillID)//divde for display
		{
			return null;
		}

        #region Skill Release + beHit Implement

        public virtual void GetSettleSkillPrototype(int skillID){
            settleTempSkill=GlobalSkillsManager.Instance.GetSkill(skillID);
        }
        
        public virtual string GetSkillRCC(Skill newskill){
			//1.cost
			//2.condition
			
			//1.1 MP
			if (MP - newskill.cost<0) return "0|MP不足";
			//1.2 or 2.1 Item
			foreach (Item req in newskill.requireItems)
			{
				bool existItem = false;
				//= bag.Contains(req);
				foreach (Item exist in bag)
				{
					if (exist.name == req.name)
					{
						existItem = true;
						break;
					}
				}
				if (existItem == false) return "0|背包中没有"+req.name;
				//minus count -> after really true.
			}
			//2.2 propety limit check
			for(int i=0 ;i < Property.Length ;i++)
			{
				if (Property[i] - newskill.propertyRL[i] < 0)
				{
					string res="0|";
					switch (i)
					{
						case 0:
							return res + "力量属性不满足释放条件";
						case 1:
							return res + "智慧属性不满足释放条件";
						case 2:
							return res + "神圣属性不满足释放条件";
						case 3:
							return res + "洞察属性不满足释放条件";
					}
				}
			}
			return "1";
        }

        public virtual void UpdateFightDataPositive(Skill settleSkill){
			//cost
			SetNumVal.SetNumValBy(this,settleSkill,OperateType.CostMP);
			//TODO-minus the count of require Item
			//TODO AddBuffEffect 
        }
        public virtual void UpdateFightDataPassive(Skill settleSkill){
            SetNumVal.SetNumValBy(this,settleSkill,OperateType.AddAngerValue);
            SetNumVal.SetNumValBy(this,settleSkill,OperateType.AddFearValue);
            SetNumVal.SetNumValBy(this,settleSkill,OperateType.MinusHP);
        }

        public virtual void SkillSettlement(Skill curSetSkill){
			foreach (Buff curbuff in buffs)
			{

			}
			//if(buffDict["Normal"]){
            //    curSetSkill.damage+=0;
            //}
            //if(buffDict["Fatigue"]){
            //    curSetSkill.damage/=2;
            //    curSetSkill.cost+=10;
            //}
            //if(buffDict["Fear"]){
            //    int tempRate=random.Next(0,10);
            //    if(tempRate<=5){
            //        curSetSkill.cost+=20;
            //        if(tempRate<3){
            //            //TODO-SAN=0 // player cant move when return Map
            //            if(tempRate==1) curSetSkill.damage=0;
            //        }
            //    }
            //}
            //if(buffDict["Anger"]){
            //    curSetSkill.damage*=2;
            //}
            //if(buffDict["Poisoning"]){
            //    HP-=2;//TODO CLK
            //}
        }

		#endregion

		public string ConvertNum2Pic(int num)
		{
			int count = num / 10;
			string temp_ref = new string('>' , count);
			return temp_ref;
		}

		public virtual void PrintDataTeam()
		{
			int posX = Console.CursorLeft;
			int posY = Console.CursorTop;
			Console.WriteLine(name);
			Console.SetCursorPosition(posX , posY+1);
			Console.WriteLine("HP:" + ConvertNum2Pic(90));
			Console.SetCursorPosition(posX, posY+2);
			Console.WriteLine("MP:" + ConvertNum2Pic(70));
			Console.SetCursorPosition(posX , posY + 3);
			Console.WriteLine("Armor: " + Armor);

		}

		public virtual void PrintDataHostile()
		{
			int posX = Console.CursorLeft;
			int posY = Console.CursorTop;
			Console.WriteLine(name);
			Console.SetCursorPosition(posX , posY + 1);
			Console.WriteLine("HP:" + ConvertNum2Pic(HP));
			Console.SetCursorPosition(posX , posY + 2);
			Console.WriteLine("Fear:" + ConvertNum2Pic(fear_value));
			Console.SetCursorPosition(posX , posY + 3);
			Console.WriteLine("Anger:" + ConvertNum2Pic(anger_value));
		}

		public virtual List<string> GetChrDataTeam()
		{
			List<string> list = new List<string>();
			list.Add(name+"\n");
			list.Add("HP:" + ConvertNum2Pic(HP)+"\n");
			list.Add("MP:" + ConvertNum2Pic(MP)+"\n");
			list.Add("Armor: "+Armor+"\n");

			return list;
		}

        public virtual void CheckStatus(){}//self-check
    }
}
