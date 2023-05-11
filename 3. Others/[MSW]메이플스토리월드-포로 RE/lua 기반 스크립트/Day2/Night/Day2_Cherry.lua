Day2_Cherry{
    -- Day2 밤에 npc 체리와 상호작용하는 코드. 체리 퀘스트를 활성화해서 Day3 낮 맵에 약초가 등장하도록 한다.
    Property:
    Fucntion:
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false and _QuestManager.CherryQ == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Day2_CherryText")
                _TalkManager:ShowNextText()
                _QuestManager.CherryQ = true
            end
        }
}