EndingCredits{
    -- 어떤 엔딩인지 보여준 후, 엔딩 크레딧을 보여주는 코드.
    Property:
        [None]
        Entity endingName = nil -- 엔딩 이름. 맵에 진입할 때 퀘스트 상황에 맞게 할당
        [None]
        Entity teamName = 249f4b5d-bc8d-41ec-be5f-b701d2bb9b68
        [None]
        Entity development = a4f7cc52-41d6-47da-928e-19fc8e4ae5a3
        [None]
        Entity developer = 770de319-7774-4167-b1c4-a4c766aa36e4 -- 개발자 이름 나열
        [None]
        Entity design = c91ee704-6270-48d4-ad11-c1b07d369268
        [None]
        Entity designer = 0eb597f9-7c60-4fe7-8bb5-a5bf72b35272 -- 디자이너 이름 나열
        [None]
        Entity support = 6d070a80-5f4d-4c41-b466-b6e8ddb176b2
        [None]
        Entity spon1 = d08a659a-9ee9-42f2-b2e6-f3d1cdaac2d8 -- 넥슨
        [None]
        Entity spon2 = 22c7c74a-15c9-46a9-b55c-f854cf160605 -- 메이플스토리 월드
        [None]
        Entity spon3 = 557aefb1-18cd-44f5-8675-7a6b92f78c5a -- 멋쟁이사자처럼
        [None]
        Entity thank = c13a77e8-7b3b-4832-8107-c286df756221 -- 감사인사
        [Sync]
        boolean max = false
        [Sync]
        Entity map = 84952085-c83b-416e-9562-456e75ad533e

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            -- 어떤 엔딩인지 자막을 천천히 출력
            if self.endingName.Visible == true then
                if self.max == false then
                    self.endingName.TextComponent.FontColor.a = self.endingName.TextComponent.FontColor.a + 0.01
                    if self.endingName.TextComponent.FontColor.a >= 2 then
                        self.max = true
                    end
                -- 최대치에 도달하면(일정 시간 후) 자막을 리게 만들다가 비활성화
                elseif self.max == true then
                    self.endingName.TextComponent.FontColor.a = self.endingName.TextComponent.FontColor.a - 0.01
                    if self.endingName.TextComponent.FontColor.a <= 0 then
                        self.endingName.Visible = false
                        self.max = false
                    end
                end
            -- 팀 이름 출력
            elseif self.teamName.Visible == true then
                if self.max == false then
                    self.teamName.TextComponent.FontColor.a = self.teamName.TextComponent.FontColor.a + 0.01
                    if self.teamName.TextComponent.FontColor.a >= 1.2 then
                        self.max = true
                    end
                -- 최대치에 도달하면(일정 시간 후) 흐리게 만들고 비활성화
                elseif self.max == true then
                    self.teamName.TextComponent.FontColor.a = self.teamName.TextComponent.FontColor.a - 0.01
                    if self.teamName.TextComponent.FontColor.a <= 0 then
                        self.teamName.Visible = false
                        self.max = false
                    end
                end
            -- 개발진, 디자이너 출력
            elseif self.development.Visible == true then
                if self.max == false then
                    self.development.TextComponent.FontColor.a = self.development.TextComponent.FontColor.a + 0.01
                    self.developer.TextComponent.FontColor.a = self.developer.TextComponent.FontColor.a + 0.01
                    self.design.TextComponent.FontColor.a = self.design.TextComponent.FontColor.a + 0.01
                    self.designer.TextComponent.FontColor.a = self.designer.TextComponent.FontColor.a + 0.01
                    if self.development.TextComponent.FontColor.a >= 1.2 then
                        self.max = true
                    end
                -- 최대치에 도달하면(일정 시간 후) 흐리게 만들고 비활성화
                elseif self.max == true then
                    self.development.TextComponent.FontColor.a = self.development.TextComponent.FontColor.a - 0.01
                    self.developer.TextComponent.FontColor.a = self.developer.TextComponent.FontColor.a - 0.01
                    self.design.TextComponent.FontColor.a = self.design.TextComponent.FontColor.a - 0.01
                    self.designer.TextComponent.FontColor.a = self.designer.TextComponent.FontColor.a - 0.01
                    if self.development.TextComponent.FontColor.a <= 0 then
                        self.development.Visible = false -- 하나만 해도 되지만 아래는 안전장치 삼아 설정
                        self.developer.Visible = false
                        self.design.Visible = false
                        self.designer.Visible = false
                        self.max = false
                    end
                end
            -- 후원 출력
            elseif self.support.Visible == true then
                if self.max == false then
                    self.support.TextComponent.FontColor.a = self.support.TextComponent.FontColor.a + 0.01
                    self.spon1.TextComponent.FontColor.a = self.spon1.TextComponent.FontColor.a + 0.01
                    self.spon2.TextComponent.FontColor.a = self.spon2.TextComponent.FontColor.a + 0.01
                    self.spon3.TextComponent.FontColor.a = self.spon3.TextComponent.FontColor.a + 0.01
                    if self.support.TextComponent.FontColor.a >= 1.2 then
                        self.max = true
                    end
                -- 최대치에 도달하면(일정 시간 후) 흐리게 만들고 비활성화
                elseif self.max == true then
                    self.support.TextComponent.FontColor.a = self.support.TextComponent.FontColor.a - 0.01
                    self.spon1.TextComponent.FontColor.a = self.spon1.TextComponent.FontColor.a - 0.01
                    self.spon2.TextComponent.FontColor.a = self.spon2.TextComponent.FontColor.a - 0.01
                    self.spon3.TextComponent.FontColor.a = self.spon3.TextComponent.FontColor.a - 0.01
                    if self.support.TextComponent.FontColor.a <= 0 then
                        self.support.Visible = false -- 하나만 해도 되지만 아래는 안전장치 삼아 설정
                        self.spon1.Visible = false
                        self.spon2.Visible = false
                        self.spon3.Visible = false
                        self.max = false
                    end
                end
            -- 감사 인사 출력(게임 종료)
            elseif self.thank.Visible == true then
                if self.max == false then
                    self.thank.TextComponent.FontColor.a = self.thank.TextComponent.FontColor.a + 0.01
                    if self.thank.TextComponent.FontColor.a >= 4 then
                        self.max = true
                    end
                end
            end
        }

        [client only]
        void OnBeginPlay()
        {
            if _QuestManager.AlexaClear == true and _QuestManager.MartinClear == true and _QuestManager.CherryClear == true then -- 진엔딩 자막 출력. 진엔딩 음악을 기본 음악으로 재생.
                self.endingName = _EntityService:GetEntityByPath("/ui/UIGroup/Ending/TrueEnding")
            elseif _QuestManager.AlexaClear == true then -- 노멀엔딩 자막을 출력하고, 노멀엔딩에 맞는 음악 재생
                self.map.SoundComponent.Enable = false
                self.map.SoundComponent.AudioClipRUID = "ac30e4669c3e445783a0ded36cfa52e2"
                self.map.SoundComponent.Enable = true
                self.endingName = _EntityService:GetEntityByPath("/ui/UIGroup/Ending/NormalEnding")
            else -- 배드엔딩 자막을 출력하고, 배드엔딩에 맞는 음악 재생
                self.map.SoundComponent.Enable = false
                self.map.SoundComponent.AudioClipRUID = "af128afb31b1484e9592abbaae663406"
                self.map.SoundComponent.Enable = true
                self.endingName = _EntityService:GetEntityByPath("/ui/UIGroup/Ending/BadEnding")
            end
        }

    EntityEventHandler:
}