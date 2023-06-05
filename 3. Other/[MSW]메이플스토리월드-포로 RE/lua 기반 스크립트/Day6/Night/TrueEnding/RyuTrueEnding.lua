RyuTrueEnding{
    -- Day6 밤의 진엔딩 중에, 잠시 감옥에서 나와 친구 '류'를 기다리는 장면을 연출하는 코드. 류와 합류한 뒤 다음 맵으로 가는 포탈이 활성화.
    Property:
        [Sync]
        Entity portal = 4eaf6567-f8a3-4a57-a9a5-bd1ab3fed6b4

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            -- 특정 대사까지 진행되었으면 엔티티의 visible을 활성화
            if _TalkManager.count == 4 then
                self.Entity.Visible = true
            end
            -- 모든 대화를 마쳤으면 다음 맵으로 이동하는 포탈을 활성화
            if _TalkManager.talkEnd == true then
                self.portal.Enable = true
                _TalkManager.talkEnd = false
            end
        }

        [client only]
        void OnBeginPlay(){
            -- 맵에 진입하자마자 대화(독백) 시작.
            _TalkManager.npcTalkData = _DataService:GetTable("RyuTrueEndingText")
            _TalkManager:ShowNextText()
        }

    EntityEventHandler:
}