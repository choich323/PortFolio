Ryu{
    -- Day4 밤에 감옥에 들어가기 전, 친구인 류와의 상호작용 코드. 체리 퀘스트 클리어 여부에 따라 대화와 포탈이 달라진다.
    Property:
        [Sync]
        Entity portal_dead = 90278022-9b5b-4ae1-97d3-d0f781e9b837
        [Sync]
        Entity ryu = 3e5e7626-52b7-4b86-a9bc-583a57de459c
        [Sync]
        Entity portal_alive = a5df86e5-f56e-49e3-9d9d-75d69607bbb4

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd == true then
                if _QuestManager.CherryClear == false then
                    self.portal_dead.Enable = true
                elseif _QuestManager.CherryClear == true then
                    self.portal_alive.Enable = true
                end
                _TalkManager.talkEnd = false
                self.ryu.Enable = false
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
            if _TalkManager.uiTalkPanel.Enable == false and _QuestManager.CherryClear == false then
                _TalkManager.npcTalkData = _DataService:GetTable("RyuText_dead")
                _TalkManager:ShowNextText()
            elseif _TalkManager.uiTalkPanel.Enable == false and _QuestManager.CherryClear == true then
                _TalkManager.npcTalkData = _DataService:GetTable("RyuText_alive")
                _TalkManager:ShowNextText()
            end
        }
}