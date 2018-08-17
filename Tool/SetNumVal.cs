namespace Game_VSmode_verTest {
    static class SetNumVal {

        static public void SetNumValBy(Character chr,Skill settleSkill,OperateType type){ 
            switch (type){
                case OperateType.MinusHP:
                    ChangeNumByChecked(ref chr.HP,"-",settleSkill.damage,chr.Max_Limit["HP"],chr.isDead,true);
                break;
                case OperateType.AddAngerValue:
                    ChangeNumByChecked(ref chr.anger_value,"+",settleSkill.acc_opposite_anger,chr.Max_Limit["anger_value"]);
                break;
                case OperateType.AddFearValue:
                   ChangeNumByChecked(ref chr.fear_value,"+",settleSkill.acc_opposite_fear,chr.Max_Limit["fear_value"]); 
                break;
                case OperateType.CostMP:
                    ChangeNumByChecked(ref chr.MP,"-",settleSkill.cost,chr.Max_Limit["MP"]);
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
