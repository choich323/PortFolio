Acairum_True{
    -- Day6 밤의 진엔딩 중 마지막 장면. 모두와 함께 탈출하는 데 성공한 플레이어를 보며 당황하는 아카이럼의 모습 연출을 담당하는 코드.
    Property:
        [Sync]
        boolean tEnd = false

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            -- 시작하면 커튼이 옅어지며 페이드인. 이후 아카이럼의 독백 시작.
            if self.Entity.SpriteRendererComponent.Color.a > 0.001 then
                self.Entity.SpriteRendererComponent.Color.a = self.Entity.SpriteRendererComponent.Color.a - 0.003
            elseif self.Entity.SpriteRendererComponent.Color.a <= 0.001 and self.tEnd == false then
                _TalkManager.npcTalkData = _DataService:GetTable("Acairum_TrueText")
                _TalkManager:ShowNextText()
                self.tEnd = true
            end
            -- 아카이럼의 독백이 끝나면 엔딩 크레딧으로 이동.
            if _TalkManager.talkEnd == true and self.tEnd == true then
                _TeleportService:TeleportToEntityPath(_UserService.LocalPlayer, "/maps/EndingCredits/spawn")
            end
        }

    EntityEventHandler:
}