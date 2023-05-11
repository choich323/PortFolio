TalkManager{
    -- 대화 UI의 진행을 위한 코드
    -- ShowNextText는 인물과의 대화에서 사용
    -- Notice는 유저에게 간단하게 알림을 줄 때 사용. 이름, 초상화 등을 할당하는 과정이 생략되며, 데이터 파일의 로드 없이 string 값만 매개변수로 넘겨주는 것으로 사용가능.
    Property:
        [None]
        number count = 1
        [None]
        any npcTalkData = nil
        [None]
        Entity uiNameEntity = ab23ad69-2f37-431c-89c5-3d7d3d662f0e -- Entity의 경로
        [None]
        Entity uiMessageEntity = 25e05e22-ff4f-4a30-b582-cc3cf46f2e04
        [None]
        Entity uiTalkPanel = 27eb2a9d-3db8-4616-b260-a09ce7d8df48
        [None]
        Entity uiPortraitEntity = 1a2794c5-ea87-42d3-83d4-d2c2de7bcdb6
        [Sync]
        string player1 = ""
        [Sync]
        boolean talkEnd = false
        [Sync]
        boolean isNotice = false
        [Sync]
        boolean noticeEnd = false

    Function:
        [client only]
        void ShowNextText()
        {
            local isNameEnable = false
            local isPortraitEnable = false
    
            local message = self.npcTalkData:GetCell(self.count, "text") -- count번째 줄의 데이터를 기준으로 함
    
            local portrait = self.npcTalkData:GetCell(self.count, "portrait")
            if portrait ~= "" then
                isPortraitEnable = true
                self.uiPortraitEntity.SpriteGUIRendererComponent.ImageRUID = portrait
            end
    
            if message == nil then -- 대화 종료;
                self.uiTalkPanel.Enable = false
                self.talkEnd = true
                self.count = 1 -- 데이터 인덱스 초기화
                return
            else
                self.uiTalkPanel.Enable = true
                self.talkEnd = false
                self.uiMessageEntity.TextComponent.Text = message -- 텍스트 컴포넌트에 텍스트 데이터를 넣는다
            end
    
            local name = self.npcTalkData:GetCell(self.count, "name")
    
            if name ~= "" then -- 받아온 이름이 nil이 아니면
                isNameEnable = true
                if name == "player1" then -- 플레이어 대화 파트는 실제 유저의 id를 가져온다
                    self.player1 = _UserService.LocalPlayer.NameTagComponent.Name
                    name = self.player1
                end
                self.uiNameEntity.TextComponent.Text = name
            end
    
            self.uiNameEntity.Enable = isNameEnable
            self.uiPortraitEntity.Enable = isPortraitEnable
    
            self.count = self.count + 1
        }

        [client only]
        void Notice(string message)
        {
            if self.uiTalkPanel.Enable == true then -- 대화창이 열려있으면 끄기
                self.uiTalkPanel.Enable = false
                self.isNotice = false -- 다음 버튼 누를 때 함수 결정 용도
                self.noticeEnd = true -- 출구 포탈 활성화용
                return
            end
        
            self.uiTalkPanel.Enable = true -- 대화창 켜고 이름, 초상화, 텍스트 설정
            self.uiPortraitEntity.Enable = false
            self.uiNameEntity.Enable = false
            self.uiMessageEntity.TextComponent.Text = message
        }
        
    EntityEventHandler:
}