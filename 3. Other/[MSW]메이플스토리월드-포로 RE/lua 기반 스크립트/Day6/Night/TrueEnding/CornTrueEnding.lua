CornTrueEnding{
    -- Day6 밤의 진엔딩 중에, 감옥에 갇혀 있는 콘과 체리를 구해주는 대화를 보여주는 상호작용 코드. 대화 기록을 ExitTrueEnding 코드로 넘겨줘서 다음 맵으로 이동하는 포탈을 활성화한다.
    Property:
        [Sync]
        boolean corn = false

    Fucntion:
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false and self.corn == false then
                _TalkManager.npcTalkData = _DataService:GetTable("CornTrueEndingText")
                _TalkManager:ShowNextText()
                self.corn = true
            end
        }
}