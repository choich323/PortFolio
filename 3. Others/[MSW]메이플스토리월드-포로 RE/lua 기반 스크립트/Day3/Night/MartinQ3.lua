MartinQ3{
    -- 마틴의 퀘스트를 클리어하기 위해 상호작용하는 코드.
    Property:
        [Sync]
        boolean Q3 = false

    Fucntion:
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if self.Q3 == false then
                _TalkManager.npcTalkData = _DataService:GetTable("MartinQ3Text")
                _TalkManager:ShowNextText()
                self.Q3 = true
            end
        }
}