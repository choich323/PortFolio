JailEntrance4{
    -- Day4 밤의 감옥에서 나가는 코드. 콘의 생존여부에 상관없이 두 개의 맵에 동일한 스크립트를 적용. 포탈을 타면 Day5 낮 스테이지로 이동
    Property:
        [Sync]
        Entity Portal_alive = 541bc3ed-6ae6-4d3b-b02f-78553d252370
        [Sync]
        Entity Portal_dead = 3477488f-e3a8-4320-9adf-e5382a323654

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.noticeEnd == true then -- notice 대화창이 사라지면 포탈 활성화
                if _QuestManager.CherryClear == true then
                    self.Portal_alive.Enable = true
                elseif _QuestManager.CherryClear == false then
                    self.Portal_dead.Enable = true
                end
                _TalkManager.noticeEnd = false
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            if _TalkManager.uiTalkPanel.Enable == false then -- 대화창이 닫혀있을 때만
                _TalkManager.isNotice = true
                _TalkManager:Notice("할 일이 없다면 나가는 게 좋겠지.")
            end
        }
}