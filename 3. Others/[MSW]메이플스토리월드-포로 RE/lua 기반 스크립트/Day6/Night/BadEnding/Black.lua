Black{
    -- bad ending 연출 코드. 특별한 컨트롤을 요구하지 않고 연출만 보도록 설정.
    Property:
        [Sync]
        Entity energy = 04ee805f-7d50-4654-b739-d4ef74f60661
        [Sync]
        Entity normalA = ef45ad1c-e03f-4979-a859-3cfbb4264480
        [Sync]
        Entity spellA = 0d2f2ff0-4549-4356-9a0e-b82ea552ad96
        [Sync]
        Entity effect = 552e3b92-9345-4e48-819e-23634d0cca39
        [Sync]
        boolean open = false

    Function:
        [client only]
        void OnBeginPlay()
        {
            -- 플레이어의 시점을 고정하고, 컨트롤하지 못하게 설정한 후 스토리 재생.
            _TalkInputBlock.stop = true
            _UserService.LocalPlayer.PlayerControllerComponent.LookDirectionX = 1
            _UserService.LocalPlayer.PlayerControllerComponent.Enable = false
            _TalkManager.npcTalkData = _DataService:GetTable("BadEnding")
            _TalkManager:ShowNextText()
        }

        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd == true and self.open == false then
            -- 첫 독백이 끝나면 화면을 가리던 검은 커튼의 알파 값을 천천히 감소시켜서 페이드 인으로 연출
                self.Entity.SpriteRendererComponent.Color.a = self.Entity.SpriteRendererComponent.Color.a - 0.01
                if self.Entity.SpriteRendererComponent.Color.a <= 0 then -- 장막이 완전히 사라지면 스토리 재생
                    _TalkManager.npcTalkData = _DataService:GetTable("BadEnding2")
                    _TalkManager:ShowNextText()
                    self.open = true
                    self.Entity.Visible = false
                    self.Entity.SpriteRendererComponent.Color.a = 1
                end
            -- 두 번째 대화가 끝나면 아카이럼 마법진이 서서히 모습을 드러냄.
            elseif _TalkManager.talkEnd == true and self.energy.SpriteRendererComponent.Color.a < 1.2 then
                self.energy.SpriteRendererComponent.Color.a = self.energy.SpriteRendererComponent.Color.a + 0.0065
                if self.effect.Enable == false then
                    self.effect.Enable = true
                end
                if self.energy.SpriteRendererComponent.Color.a >= 1.2 then
                    _TalkManager.talkEnd = false
                end
            -- 마지막 대화까지 끝나면 검은 커튼을 다시 펼치고 엔딩 크레딧으로 이동
            elseif _TalkManager.talkEnd == true then
                self.Entity.Visible = true
                _TalkManager.uiMessageEntity.TextComponent.FontColor = Color.white
                _TeleportService:TeleportToEntityPath(_UserService.LocalPlayer, "/maps/EndingCredits/spawn")
            -- 아카이럼의 마법진이 모습을 완전히 갖추면 아카이럼의 외형을 변경하고 마지막 대화 시작.
            elseif self.energy.SpriteRendererComponent.Color.a >= 1 and _TalkManager.uiTalkPanel.Enable == false and self.Entity.Visible == false then
                self.normalA.Enable = false
                self.spellA.Enable = true
                _TalkManager.npcTalkData = _DataService:GetTable("BadEnding3")
                _TalkManager:ShowNextText()
                _TalkManager.uiMessageEntity.TextComponent.FontColor = Color.red
            end
        }

    EntityEventHandler:
}