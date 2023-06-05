Day4_Jail1{
    -- 체리 퀘스트를 클리어하지 못한 상태에서, Day4 밤에 감옥의 포로들과 대화하는 코드. 대화 외의 상호작용은 존재하지 않음.
    Property:
        [Sync]
        boolean talked = false

    Fucntion:
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false and self.talked == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Day4_Jail1_Text")
                _TalkManager:ShowNextText()
                self.talked = true
            end
        }
}