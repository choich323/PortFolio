CaptainSpeaking5{
    -- Day5 밤에 경비대장과 상호작용하는 코드
    Property:
        [Sync]
        Entity portal = 81a70404-6db3-4e2e-a270-bc39ae37d56e

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
                _TalkManager.npcTalkData = _DataService:GetTable("CaptainSpeaking5Text")
                _TalkManager:ShowNextText()
            end
        }
}