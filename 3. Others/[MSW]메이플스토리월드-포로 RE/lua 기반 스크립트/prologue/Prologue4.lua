Prologue4{
    Property:
        [Sync]
        Entity portal = "f23dd659-ad5f-41d7-a101-716eb8e06d4f"
        [Sync]
        boolean isTalk = "false"

    Function:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd and self.isTalk then
                self.portal.Enable = true
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Prologue4Text")
                _TalkManager:ShowNextText()
            end
        }
}