namespace Game_VSmode_verTest {
    static class Calculate {
        static public void GetParameters(Character chr,int[] param){
            
        }

        static public void ValueChangedBy(object left_operate_obj,object right_operate_obj,ChangeValue value){ 
            Character cur_chr=left_operate_obj as Character;
            Character hostile_chr=right_operate_obj as Character;
            switch (value){
                case ChangeValue.MinusHP:
                    ChangeNumByChecked(ref cur_chr.HP,"-",hostile_chr.skill[hostile_chr.cur_skill_index].damage,cur_chr.Max_Limit["HP"],cur_chr.isDead,true);
                break;
                case ChangeValue.AddAngerValue:
                    ChangeNumByChecked(ref cur_chr.anger_value,"+",hostile_chr.skill[hostile_chr.cur_skill_index].acc_opposite_anger,cur_chr.Max_Limit["anger_value"]);
                break;
                case ChangeValue.AddFearValue:
                   ChangeNumByChecked(ref cur_chr.fear_value,"+",hostile_chr.skill[hostile_chr.cur_skill_index].acc_opposite_fear,cur_chr.Max_Limit["fatigue_value"]); 
                break;
                case ChangeValue.CostMP:
                    ChangeNumByChecked(ref cur_chr.MP,"-",cur_chr.skill[cur_chr.cur_skill_index].cost,cur_chr.Max_Limit["MP"]);
                break;
                case ChangeValue.AddFatigueValue:
                    ChangeNumByChecked(ref cur_chr.fatigue_value,"+",cur_chr.skill[cur_chr.cur_skill_index].cost,cur_chr.Max_Limit["fatigue_value"]);
                break;
            }
        }

        static public void ChangeNumByChecked(ref int src_value,string change,int change_value,int MaxValue,bool need2be_set_status,bool _status) {
            switch (change) {
                case "+":
                    if (src_value + change_value > MaxValue){
                        src_value = MaxValue;
                        need2be_set_status = _status;
                    }else{
                        src_value += change_value;
                    }
                    break;
                case "-":
                    if (src_value + change_value < 0){
                        src_value = 0;
                        need2be_set_status = _status;
                    }else{
                        src_value -= change_value;
                    }
                    break;
            }
        }

        static public void ChangeNumByChecked(ref int src_value,string change,int change_value,int MaxValue)
        {
            switch (change) {
                case "+":
                    if (src_value + change_value > MaxValue){
                        src_value = MaxValue;
                    }else{
                        src_value += change_value;
                    }
                    break;
                case "-":
                    if (src_value + change_value < 0){
                        src_value = 0;
                    }else{
                        src_value -= change_value;
                    }
                    break;
            }
        }
    }
}
