UIon{
    -- 낮 시간대 시작시 life UI를 켜고, 3초의 카운트 후 게임 시작
    Property:
        [None]
        Entity life1 = 7fbab4e2-2df6-4c99-86db-9a9f0e7c67b1
        [None]
        Entity life2 = nil
        [None]
        Entity life3 = nil
        [None]
        Entity count = 2aaf34d5-940d-49f1-bdd5-7205272b1c64
        [None]
        Entity start = 2a9ce2bb-5885-45f7-af72-accc81224b4c
        [None]
        number sec = 0
        [None]
        Entity ui = 50907267-26ca-4a34-926f-1595bb6ae8ea

    Function:
        [client only]
        void OnBeginPlay()
        {
            self.ui.Enable = true
            wait(1.5)
            self.count.TextComponent.Text = "3"
            wait(1)
            self.count.TextComponent.Text = "2"
            wait(1)
            self.count.TextComponent.Text = "1"
            wait(1)
            self.count.TextComponent.Text = ""
            self.start.Enable = true
            wait(1.2)
            self.start.Enable = false
            
            _GameManager.Car.CarCompnent:SetCarVelocity()
        }

    EntityEventHandler:
}