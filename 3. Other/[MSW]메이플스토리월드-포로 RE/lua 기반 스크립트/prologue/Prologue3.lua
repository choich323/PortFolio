Prologue3{
    Property:
    Function:
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Prologue3Text")
                _TalkManager:ShowNextText()
            end
        }
}