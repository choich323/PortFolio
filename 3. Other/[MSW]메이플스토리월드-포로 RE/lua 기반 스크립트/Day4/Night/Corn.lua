Corn{
    -- 체리 퀘스트를 완료한 상태에서, Day4 밤에 npc 콘과 대화할 수 있도록 해주는 코드.
    Property:
        [Sync]
        Entity corn = c67a89ab-3455-4372-90fe-e33da17d2a1a

    Function:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd == true then
                self.corn.Enable = false
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("CornText")
                _TalkManager:ShowNextText()
            end
        }
}