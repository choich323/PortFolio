Jail1{
    -- Day1 밤에 감옥1과의 상호작용 코드
    Property:
        [Sync]
        boolean isTalk = false

    Function:    
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false and self.isTalk == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Jail1Text")
                _TalkManager:ShowNextText()
                self.isTalk = true
            end
        }
}