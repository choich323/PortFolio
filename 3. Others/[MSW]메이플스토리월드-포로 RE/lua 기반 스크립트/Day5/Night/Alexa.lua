Alexa{
    -- 
    Property:
        [Sync]
        Entity portal = 0e85b2b6-7b7d-4e2d-a7f9-8ca8f032cf50

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd and _QuestManager.AlexaQ == true then
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
            if _TalkManager.uiTalkPanel.Enable == false and _QuestManager.AlexaQ == false then
                _TalkManager.npcTalkData = _DataService:GetTable("AlexaText")
                _TalkManager:ShowNextText()
                _QuestManager.AlexaQ = true
            end
        }
}