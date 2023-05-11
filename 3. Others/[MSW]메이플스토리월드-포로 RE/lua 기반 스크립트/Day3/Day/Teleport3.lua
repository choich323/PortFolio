Teleport3{
    -- Day3의 낮에서 Day3 밤으로 이동시키는 코드
    Property:
        [None]
        Entity clear = 241e42cc-cf22-45e4-8fdc-ebf4bd5776f7
        [None]
        Entity ui = 50907267-26ca-4a34-926f-1595bb6ae8ea

    Function:
        [client only]
        void OnBeginPlay()
        {
            _TalkInputBlock.stop = true
                _UserService.LocalPlayer.PlayerControllerComponent.Enable = false
        }

        [client only]
        void OnUpdate(number delta)
        {
            if _TalkInputBlock.stop == true then
                _UserService.LocalPlayer.Visible = false
            end
        }

    EntityEventHandler:
        [self]
        HandleTriggerEnterEvent
        {
            -- Parameters
            local TriggerBodyEntity = event.TriggerBodyEntity
            
            if TriggerBodyEntity.Id ~= _GameManager.Car.Id then
                return
            end
            
            _GameManager.Car.CarCompnent:ResetCarVelocity()
            --------------------------------------------------------
            self.clear.Enable = true
            wait(2)
            self.clear.Enable = false
            _TalkInputBlock.stop = false
            _UserService.LocalPlayer.Visible = true
            self.ui.Enable = false
            _GameManager.JoyStick.Visible = true
            _TeleportService:TeleportToEntityPath(_UserService.LocalPlayer, "/maps/Night3_경비대장사무실/spawn")
        }
}