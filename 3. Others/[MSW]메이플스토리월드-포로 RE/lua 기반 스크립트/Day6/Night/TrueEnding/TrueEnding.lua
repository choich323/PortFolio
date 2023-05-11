TrueEnding{
    -- Day6 밤의 진엔딩 중에, 탈출하는 장면 연출을 담당하는 코드.
    Property:
        [Sync]
        Entity playerSpell = c187e0c3-72d2-4ecd-b482-63c3aaf87409 -- 텔레포트 대기 중 플레이어에게 보이는 이펙트
        [Sync]
        Entity spell = 2f552d7f-ebbb-4fc4-9454-28344d0cebe6 -- 텔레포트 대기 중 알렉사에게 보이는 이펙트
        [Sync]
        Entity spell2 = b990e7ed-6947-4cc7-b62c-9df9774c39f5 -- 텔레포트 대기 중 류에게 보이는 이펙트
        [Sync]
        Entity playerWarp = 10f003ab-8de1-4948-9a40-704b65450f8e -- 텔레포트 시 플레이어에게 보이는 이펙트
        [Sync]
        Entity warp = e74a7b76-ca7c-4282-a5bf-7c2300d8f65e -- -- 텔레포트 시 알렉사, 류에게 보이는 이펙트
        [Sync]
        Entity curtain = 8c852af7-2a6e-4484-a9d6-f1634ae99c5e -- 텔레포트 이후 화면을 검은 색으로 가리기 위한 커튼
        [Sync]
        Entity orb = b6082afd-45d6-4ceb-b4c0-e3b8ca27c01d -- 알렉사가 마법을 위해 에너지를 모은 구체
        [Sync]
        Entity magic = 119a9ca3-da39-44a2-a456-d280fe160938 -- 마법진
        [Sync]
        Entity spark = ea1df2a3-ea98-446b-a4ac-515ac5da7dd5 -- 텔레포트시 오브에서 발생하는 스파크 이펙트
        [Sync]
        Entity ryu = d15c4c97-7030-4fb8-8bb5-967648443e48 -- 류 컴포넌트. 텔레포트시 이미지를 사라지게 만들기 위해 할당.
        [Sync]
        boolean talked = false -- 텔레포트 이후 독백 대사 타이밍을 맞추기 위한 boolean

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            -------------- 표기된 진행 순서대로 연출이 진행 -----------------
            -- 1)알렉사와의 대화가 끝나면 텔레포트 대기 상태로 진입. 이펙트를 켠다.
            if _TalkManager.talkEnd == true and self.playerSpell.Enable == false then
                self.playerSpell.Enable = true
                self.spell.Enable = true
            -- 2)텔레포트 대기 이펙트가 켜지면 이펙트의 속도를 점점 빠르게 증가시켜서 곧 텔레포트가 된다는 것을 연출.
            elseif self.playerSpell.Enable == true and self.playerSpell.BasicParticleComponent.PlaySpeed < 4.5 then
                self.playerSpell.BasicParticleComponent.PlaySpeed = self.playerSpell.BasicParticleComponent.PlaySpeed + 0.02
                self.spell.BasicParticleComponent.PlaySpeed = self.spell.BasicParticleComponent.PlaySpeed + 0.02
                self.spell2.BasicParticleComponent.PlaySpeed = self.spell2.BasicParticleComponent.PlaySpeed + 0.02
            -- 4) 텔레포트 이후 검은색 커튼의 알파값을 증가시켜서 화면을 가림
            elseif self.orb.Enable == false and self.curtain.SpriteRendererComponent.Color.a < 1 then
                self.curtain.SpriteRendererComponent.Color.a = self.curtain.SpriteRendererComponent.Color.a + 0.005
            -- 5) 커튼이 화면을 가리면 독백 시작.
            elseif self.curtain.SpriteRendererComponent.Color.a >= 1 and self.talked == false then
                _TalkManager.npcTalkData = _DataService:GetTable("TrueEndingText2") -- 독백
                _TalkManager:ShowNextText()
                self.talked = true
            -- 6)독백이 종료되면 마지막 장면으로 맵 이동
            elseif self.curtain.SpriteRendererComponent.Color.a >= 1 and _TalkManager.talkEnd == true then
                _TeleportService:TeleportToEntityPath(_UserService.LocalPlayer, "/maps/Night6_TrueEnding5/SpawnLocation")
            -- 3-1)텔레포트 대기 이펙트가 일정 속도를 넘어가면 텔레포트 대기 이펙트와 플레이어, 류, 알렉사의 이미지를 끄고, 오브와 마법진을 비활성화.
            -- 3-2)또한 스파크 이펙트와 텔레포트 시전 이펙트를 활성화해서 텔레포트를 연출.
            elseif self.playerSpell.BasicParticleComponent.PlaySpeed >= 4.5 and self.playerSpell.Enable == true then
                self.playerSpell.Visible = false
                self.spell.Enable = false
                self.spell2.Enable = false
                self.ryu.Enable = false
                _UserService.LocalPlayer.Visible = false
                self.Entity.Visible = false
                self.orb.Enable = false
                self.magic.Enable = false
                self.spark.Enable = true
                self.playerWarp.Enable = true
                self.warp.Enable = true
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            if _TalkManager.uiTalkPanel.Enable == false then
                _TalkInputBlock.stop = true -- 대화 이후 플레이어 입력 방지
                -- 현재 플레이어의 위치에 맞게 파티클 이펙트의 위치 조정
                self.playerSpell.TransformComponent.Position.x = _UserService.LocalPlayer.TransformComponent.Position.x
                self.playerWarp.TransformComponent.Position.x = _UserService.LocalPlayer.TransformComponent.Position.x
                _TalkManager.npcTalkData = _DataService:GetTable("TrueEndingText") -- 알렉사와 대화 시작
                _TalkManager:ShowNextText()
            end
        }
}