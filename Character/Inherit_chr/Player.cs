using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game_VSmode_verTest{
class Player : Character {
        //For Test
        public Player(){
            name = "player";
            career = Career.UNEMPLOYED;
            skill[0] = new Skill("Speak",false,true,0,10,0,20,BuffName.NULL);
            skill[1] = new Skill("Sing",true,false,18,30,0,40,BuffName.NULL);
        }
        //For Input
        public Player(string _name,Career _career,Skill[] _skills){ 
            
        }
        //For FileInput
        public Player(string path){ 
            StreamReader sr = new StreamReader(path,Encoding.Default);
            String line;
            //string[] =new string[6];
            while ((line = sr.ReadLine()) != null) {
                //name=line.ToString();
            }
        }
        List<Item> bag = new List<Item>();
        int exp = 0;
        int Coin = 0;

        public override void NormalAttack(int skill_index, ref Character chr) {
            base.NormalAttack(skill_index, ref chr);
            //other code eg.buff
            //deifferent career control
        }

        private void GetDropItems(Character chr){

        }
        private void BuyItem(NPC npc,Item item) {
            
        }
        private void SellItem(NPC noc, Item item) {

        }
    }
}
