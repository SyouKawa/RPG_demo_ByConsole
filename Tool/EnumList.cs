namespace Game_VSmode_verTest {
    enum Career {
        UNEMPLOYED,
        KNIGHT,
        THIEF,
        SHAMAN,
        POET,
        INIT
    }
    enum BuffName
    {
        Normal,
        Fatigue,
        Anger,
        Poisoning,
        Blessing,
        Fear,
    }
    enum OperateType
    { 
        Null,
        MinusHP,
        CostMP,
        AddAngerValue,
        AddFatigueValue,
        AddFearValue
    }
    enum LoadMode
    {
        Null,
        Skill,
        Item,
        Player,
        Monster
    }
    enum PanelType{
        Null,
        Skill,
        Map,
        Bag,
        Menu,
        Act,
        Descrp
    }
    enum ItemType{
        Null,
        Consumable,
        Task,
        Heal,
        Material,
        Equipment
    }
}
