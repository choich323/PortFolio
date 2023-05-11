AlexaTrueEnding{
    -- Day6 밤에 진엔딩을 위해 알렉사와 상호작용하는 코드. 대화 이후 체리, 마틴, 콘이 있는 감옥으로 이동
    Property:
        [Sync]
        Entity portal = 57788ea2-abbe-4caf-90a2-62a80f916648

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd == true then
                self.portal.Enable = true
                _TalkManager.talkEnd = false
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false and self.portal.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("AlexaTrueEndingText")
                _TalkManager:ShowNextText()
                _QuestManager.AlexaClear = true
            end
        }
}