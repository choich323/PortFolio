CaptainSpeaking6{
    -- Day6 밤에 경비대장과 상호작용하는 코드. 모든 엔딩에 대해 대화 내용은 같지만, 퀘스트 클리어 상태에 따라 다른 맵으로 가는 포탈이 생성된다.
    -- 알렉사 퀘스트를 완료하지 못한 경우: bad ending
    -- 알렉사 퀘스트는 완료했으나, 체리, 마틴의 퀘스트를 하나라도 실패한 경우: normal ending
    -- 3명의 퀘스트를 모두 완료한 경우: true ending
    Property:
        [Sync]
        Entity portal_bad = 2b464deb-eabd-4acc-b0de-4ac6811288c5
        [Sync]
        Entity portal_true = 75384243-c3c4-4ab8-b965-f48b1ab46dac
        [Sync]
        Entity portal_normal = f155c6e5-f919-4393-a385-699fa6a134fe

    Function:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd then
                if _QuestManager.CherryClear == true and _QuestManager.MartinClear == true and _QuestManager.AlexaMana == true then -- 진엔딩
                    self.portal_true.Enable = true
                elseif _QuestManager.AlexaMana == true then -- 노멀 엔딩
                    self.portal_normal.Enable = true
                else -- bad 엔딩
                    self.portal_bad.Enable = true
                end
                _TalkManager.talkEnd = false
            end
        }

        [client only]
        void OnBeginPlay()
        {
            _TalkManager.talkEnd = false
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false then
                _TalkManager.npcTalkData = _DataService:GetTable("CaptainSpeaking6Text")
                _TalkManager:ShowNextText()
            end
        }
}