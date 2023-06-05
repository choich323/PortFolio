Prologue1{
    Property:
    Function:
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Prologue1Text")
                _TalkManager:ShowNextText()
            end
        }
}