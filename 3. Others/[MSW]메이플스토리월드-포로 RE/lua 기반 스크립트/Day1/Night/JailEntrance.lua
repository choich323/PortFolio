JailEntrance{
    -- Day1 밤에 출구 포탈이 생성되도록 하는 코드
    Property:
        [Sync]
        Entity Portal = 283a27ed-bbc3-4b09-86f8-6ead7f22a767
        [Sync]
        Component jail1 = 467368b5-abd7-4b4f-9a9e-a19a44f59abe:Jail1
        [Sync]
        Component jail2 = cf39d9d4-3628-44c0-8d84-26bdc4230b8b:Jail2
        [Sync]
        Component jail3 = 82cbb8db-b033-4039-a0cd-73be793395f0:Jail3
        [Sync]
        boolean exit = false

    Function:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd and self.exit then -- 독백이 끝나면 포탈 활성화
                self.Portal.Enable = true
                _TalkManager.talkEnd = false
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if self.jail1.isTalk == true and self.jail2.isTalk == true and self.jail3.isTalk == true and _TalkManager.uiTalkPanel.Enable == false and self.Portal.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("JailEntranceText")
                _TalkManager:ShowNextText()
                self.exit = true
            elseif _TalkManager.uiTalkPanel.Enable == false and self.Portal.Enable == false then -- 아직 대화 하지 않은 감옥이 있으면
                _TalkManager.isNotice = true
                _TalkManager:Notice("감옥의 포로들을 더 살펴보자.")
            end
        }
}