﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game_VSmode_verTest{
    class Player : Character {
        //TO-DO Json存档可读取以下
        Box bag;
        GrowTree growTree;
        Equipment equipment;
        int exp;

        public Player(){
            name = "player";
            career = Career.UNEMPLOYED;
            
        }
        public override void NormalAttack(int skill_index, ref Character chr) {
            base.NormalAttack(skill_index, ref chr);
            //other code eg.buff
            //deifferent career control
        }
        public override void CheckBuff() {
            base.CheckBuff();
            if(bag.items.Contains(new Item("Rosary cross")) ){
                buffDict["Blessing"]=true;
            }
        }
        public override void EnterBuff() {
            base.EnterBuff();
            if(buffDict["Blessing"]){
                //TODO san+10;
                if(buffDict["Poisoning"]){
                    buffDict["Poisoning"]=false;
                    //TODO-Panel display no-effect
                }
            }
        }

        private void GetDropItems(Character chr){

        }
        private void BuyItem(NPC npc,Item item) {
            
        }
        private void SellItem(NPC noc, Item item) {

        }
    }
}
