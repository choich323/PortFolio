TalkContinue{
    -- 대화 UI에서 "다음" 버튼을 눌렀을 때 다음 대화가 나오도록 하는 코드
    Property:
    Function:
    EntityEventHandler:
        [self]
        HandleButtonClickEvent(ButtonClickEvent event){
            -- Parameters
            local Entity = event.Entity
            --------------------------------------------------------
            if _TalkManager.isNotice == true then
	            _TalkManager:Notice("")
            else
	            _TalkManager:ShowNextText()
            end
        }
}