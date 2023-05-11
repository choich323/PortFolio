CaptainSpeaking3{
    -- Day3 밤에 경비대장과 상호작용하는 코드
    Property:
        [Sync]
        Entity portal = f49fbe0e-48bf-4251-9d76-5d9beec7b0af

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
                _TalkManager.npcTalkData = _DataService:GetTable("CaptainSpeaking3Text")
                _TalkManager:ShowNextText()
            end
        }
}