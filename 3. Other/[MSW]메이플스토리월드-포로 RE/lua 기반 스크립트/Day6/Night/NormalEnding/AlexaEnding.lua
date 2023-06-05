AlexaEnding{
    -- Day6 밤에 노멀 엔딩을 위해 알렉사와 상호작용하는 코드.
    Property:
        [Sync]
        Entity portal = 5e0b50d4-9b19-4a58-a7fe-89d71ea4d15b

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
                _TalkManager.npcTalkData = _DataService:GetTable("AlexaEndingText")
                _TalkManager:ShowNextText()
                _QuestManager.AlexaClear = true
            end
        }
}