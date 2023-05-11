MartinQ2{
    -- 마틴의 퀘스트를 클리어하기 위해 상호작용하는 코드.
    Property:
        [Sync]
        boolean Q2 = false

    Fucntion:
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if self.Q2 == false then
                _TalkManager.npcTalkData = _DataService:GetTable("MartinQ2Text")
                _TalkManager:ShowNextText()
                self.Q2 = true
            end
        }
}