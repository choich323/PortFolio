ExitTrueEnding{
    -- Day6 밤의 진엔딩 중에, 감옥에서 콘, 체리, 마틴과의 대화를 모두 마치면 다음 맵으로 이동하는 포탈을 활성화.
    Property:
        [Sync]
        Component martin = 530eca91-2dfc-4f22-85f9-9b59a86c5b0c:MartinTrueEnding
        [Sync]
        Component corn = b2c6ae77-4554-42b1-ab6f-14591e68e85e:CornTrueEnding
        [Sync]
        Entity portal = 247501f3-2563-4f24-8acc-5aa77e788524

    Fucntion:
        [client only]
        void OnUpdate(number delta)
        {
            if _TalkManager.talkEnd == true and self.martin.martin == true and self.corn.corn == true then
                self.portal.Enable = true
                _TalkManager.talkEnd = false
            end
        }

    EntityEventHandler:
}