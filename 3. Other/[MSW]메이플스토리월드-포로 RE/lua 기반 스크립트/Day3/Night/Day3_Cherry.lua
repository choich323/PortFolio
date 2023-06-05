Day3_Cherry{
    -- Day3 밤에 체리 퀘스트를 완료하기 위한 상호작용 코드
    Property:
        [Sync]
        Entity Martin = f0bc36a3-006a-487f-9fe3-c9b4b9aa690d
        [Sync]
        Entity Cherry = 6328da08-4be4-4ab6-9c85-e0c3286cc95b

    Fucntion:
        [client only]
        void OnBeginPlay()
        {
            if _QuestManager.CherryHeal == false then -- 약초를 충분히 모으지 못했다면(3개 미만) 체리 감옥 비활성화, 마틴 감옥 바로 활성화 (퀘스트 여부에 따른 분기)
                self.Martin.Enable = true
                self.Cherry.Enable = false
            end
        }

        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd then
                _QuestManager.CherryClear = true -- 체리 퀘스트 클리어
                self.Martin.Enable = true -- 마틴과 대화 가능하게 감옥 활성화
                _TalkManager.talkEnd = false
                self.Cherry.Enable = false -- 자기 자신 off
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Day3_CherryText")
                _TalkManager:ShowNextText()
            end
        }
}