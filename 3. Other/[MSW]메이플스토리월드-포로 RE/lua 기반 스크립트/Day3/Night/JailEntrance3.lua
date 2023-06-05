JailEntrance3{
    -- Day3 밤의 감옥에서 나가는 코드. 포탈을 타면 Day4 낮 스테이지로 이동
    Property:
        [Sync]
        Entity Portal = 6eff5876-728a-45a4-a55e-02ab713c5a5a

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.noticeEnd == true then -- 대화창이 사라지면 포탈 활성화
                self.Portal.Enable = true
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