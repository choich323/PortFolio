Curtain{
    -- Day6 밤, 노멀 엔딩을 위해 알렉사의 감옥에서 나온 후 스토리 연출을 담당하는 코드.
    Property:
        [Sync]
        Entity effect = 83ace0ea-b10e-415b-81a2-112e60f509ea
        [Sync]
        Entity effect2 = 3e4248c9-b5df-4b4e-a925-a28f351852bf

    Fucntion:
        [client only]
        void OnBeginPlay()
        {
            -- 플레이어의 컨트롤을 통제 후 검은 커튼이 화면을 가린채로 대화만 보여주며 독백으로 스토리 재생.
            _TalkInputBlock.stop = true
            _UserService.LocalPlayer.PlayerControllerComponent.Enable = false
            _TalkManager.npcTalkData = _DataService:GetTable("CurtainText")
            _TalkManager:ShowNextText()
        }

        [client only]
        void OnUpdate(number delta)
        {
            -- 스토리를 재생 중 커튼을 잠시 펼쳐서 알렉사 일행이 감옥에서 탈출했음을 알려주는 이펙트를 보여주고, 잠시 후 커튼을 치고 나머지 대사 재생.
            if _TalkManager.talkEnd == true and self.effect2.Enable == false then
                self.Entity.Visible = false
                if self.effect.BasicParticleComponent.Color.a < 1 then
                    self.effect.BasicParticleComponent.Color.a = self.effect.BasicParticleComponent.Color.a + 0.005
                else
                    self.effect2.Enable = true
                    self.effect.Enable = false
                    _TalkManager.talkEnd = false
                    wait(2)
                    self.Entity.Visible = true
                    _TalkManager.npcTalkData = _DataService:GetTable("CurtainText2")
                    _TalkManager:ShowNextText()
                end
            -- 모든 대사가 끝나면 맵 이동.
            elseif _TalkManager.talkEnd == true then
                _TeleportService:TeleportToEntityPath(_UserService.LocalPlayer, "/maps/Night6_NormalEnding3/SpawnLocation")
            end
        }

    EntityEventHandler:
}