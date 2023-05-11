TalkInputBlock{
    -- 대화 UI가 켜져있을 때 유저의 입력을 막아서 예외 상황을 방지하는 코드
    Property:
        [None]
        Entity talkPanel = 27eb2a9d-3db8-4616-b260-a09ce7d8df48
        [Sync]
        boolean stop = false
    Function:
        [client only]
        void OnUpdate(number delta){
            if self.talkPanel.Enable == true then -- 대화 중이면 플레이어의 입력을 정지시키기
                _UserService.LocalPlayer.PlayerControllerComponent.Enable = false
            elseif self.talkPanel.Enable == false and self.stop == false then
                _UserService.LocalPlayer.PlayerControllerComponent.Enable = true
            end
        }
    EntityEventHandler:
}


