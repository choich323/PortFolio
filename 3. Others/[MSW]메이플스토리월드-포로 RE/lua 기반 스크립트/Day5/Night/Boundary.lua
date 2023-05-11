Boundary{
    -- Day5 밤에 알렉사가 있는 감옥에 입장하면 바로 대사가 나오도록 하는 코드.
    Property:
        [Sync]
        boolean istalked = false

    Fucntion:
    EntityEventHandler:
        [client only] [self]
        HandleTriggerStayEvent(TriggerStayEvent event)
        {
            -- Parameters
            local TriggerBodyEntity = event.TriggerBodyEntity
            --------------------------------------------------------
            if self.istalked == false then
                _TalkManager.npcTalkData = _DataService:GetTable("BoundaryText")
                _TalkManager:ShowNextText()
                self.istalked = true
            end
        }
}