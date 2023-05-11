Acairum_normal{
    -- Day6 밤에, 노멀 엔딩의 마지막 스토리 연출을 담당하는 코드.
    Property:
        [Sync]
        boolean tEnd = false

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            -- 커튼의 알파값 조정을 통해 페이드인하고, 아카이럼의 독백 시작.
            if self.Entity.SpriteRendererComponent.Color.a > 0.001 then
                self.Entity.SpriteRendererComponent.Color.a = self.Entity.SpriteRendererComponent.Color.a - 0.003
            elseif self.Entity.SpriteRendererComponent.Color.a <= 0.001 and self.tEnd == false then
                _TalkManager.npcTalkData = _DataService:GetTable("AcairumText")
                _TalkManager:ShowNextText()
                self.tEnd = true
            end
            -- 아카이럼의 독백이 끝나면 엔딩 크레딧으로 이동
            if _TalkManager.talkEnd == true and self.tEnd == true then
                _TeleportService:TeleportToEntityPath(_UserService.LocalPlayer, "/maps/EndingCredits/spawn")
            end
        }
        
        [client only]
        void OnBeginPlay()
        {
            -- 플레이어의 모습은 레이어 설정을 통해 배경 뒤에 가렸지만, 이름은 가려지지 않으므로 이름표가 보이지 않도록 설정.
            _UserService.LocalPlayer.NameTagComponent.Enable = false
        }

    EntityEventHandler:
}