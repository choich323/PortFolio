QuestManager{
    -- Quest 진행 상황을 관리하기 위한 코드. 각 변수가 ending에 영향을 줄 수 있음.
    Property:
        [Sync]
        boolean CherryQ = false
        [Sync]
        boolean CherryClear = false
        [Sync]
        boolean MartinQ = false
        [Sync]
        boolean MartinClear = false
        [Sync]
        boolean AlexaQ = false
        [Sync]
        boolean AlexaClear = false
        [Sync]
        boolean CherryHeal = false
        [Sync]
        boolean MartinFind = false
        [Sync]
        boolean AlexaMana = false
        [Sync]
        number leaf = 0
        [Sync]
        number mana = 0
    Function:
    EntityEventHandler:
}
