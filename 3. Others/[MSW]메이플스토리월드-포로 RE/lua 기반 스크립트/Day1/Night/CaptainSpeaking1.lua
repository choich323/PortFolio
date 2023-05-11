CaptainSpeaking1{
    -- Day1 밤에 경비대장 상호작용 코드
    Property:
        [Sync]
        Entity portal = 2a859406-0096-4636-8d67-a8693269b377
    
    Function:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd then
                self.portal.Enable = true
                _TalkManager.talkEnd = false
            end
        }

        [client only]
        void OnBeginPlay()
        {
            _TalkManager.talkEnd = false
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("CaptainSpeaking1Text")
                _TalkManager:ShowNextText()
            end
        }
}