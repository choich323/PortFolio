Day3_Martin{
    -- Day3 밤에 마틴과의 상호작용 코드. 첫 대화에서 유저에게 말을 걸고, 두 번째 대화에서 퀘스트를 의뢰. 옆 방에서 퀘스트를 수행하고 돌아오면 퀘스트 완료 대화를 할 수 있다.
    Property:
        [Sync]
        Entity portal = 57c1d3ad-5e60-4fc0-b363-abd557d3b4a0
        [Sync]
        number count = 1
        [Sync]
        Component Q1 = b17e6e57-1a22-4028-b24e-eff2402fd616:MartinQ1
        [Sync]
        Component Q2 = b92f3b2c-e5eb-4aa4-9c28-2ccc98aeff7a:MartinQ2
        [Sync]
        Component Q3 = aff8f9e1-ee07-40ee-bbbf-af68ce0304f9:MartinQ3

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd and _QuestManager.MartinQ == true then
                self.portal.Enable = true
                _TalkManager.talkEnd = false
            end
            
            if self.Q1.Q1 == true and self.Q2.Q2 == true and self.Q3.Q3 == true then
                _QuestManager.MartinFind = true
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false and self.count == 1 then
                _TalkManager.npcTalkData = _DataService:GetTable("Day3_MartinText")
                _TalkManager:ShowNextText()
                self.count = 2
            elseif _TalkManager.uiTalkPanel.Enable == false and self.count == 2 and _QuestManager.MartinQ == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Day3_MartinText2")
                _TalkManager:ShowNextText()
                _QuestManager.MartinQ = true
                self.count = 3
            elseif _TalkManager.uiTalkPanel.Enable == false and self.count == 3 and _QuestManager.MartinFind == true and _QuestManager.MartinClear == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Day3_MartinText3")
                _TalkManager:ShowNextText()
                _QuestManager.MartinClear = true
            end
        }

}