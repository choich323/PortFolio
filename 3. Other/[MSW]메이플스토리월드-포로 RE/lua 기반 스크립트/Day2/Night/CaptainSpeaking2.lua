CaptainSpeaking2{
    -- Day2 밤에 경비대장과 상호작용하는 코드
    Property:
        [Sync]
        Entity portal = a1da609d-610c-4ac2-b72a-e0e131195d96

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
                _TalkManager.npcTalkData = _DataService:GetTable("CaptainSpeaking2Text")
                _TalkManager:ShowNextText()
            end
        }
}