MartinQ1{
    -- 마틴의 퀘스트를 클리어하기 위해 상호작용하는 코드.
    Property:
        [Sync]
        boolean Q1 = false

    Fucntion:
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if self.Q1 == false then
                _TalkManager.npcTalkData = _DataService:GetTable("MartinQ1Text")
                _TalkManager:ShowNextText()
                self.Q1 = true
            end
        }
}