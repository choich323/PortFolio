Prologue2{
    Property:
    Function:
    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Prologue2Text")
                _TalkManager:ShowNextText()
            end
        }
}