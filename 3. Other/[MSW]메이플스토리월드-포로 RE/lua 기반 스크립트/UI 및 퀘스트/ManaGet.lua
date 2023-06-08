ManaGet{
    -- Day6 낮 진행 중 마나를 얻기 위한 코드
    Property:
        [Sync]
        number particleID = 0

    Function:
        [client only]
        void OnBeginPlay()
        {
            if _QuestManager.AlexaQ == false then
                self.Entity.Enable = false
            else
                local option = {["Color"] = Color(1,0,1,1)}	
                self.particleID = _ParticleService:PlayBasicParticle(
                    BasicParticleType.Nova, 
                    self.Entity, 
                    self.Entity.TransformComponent.Position + Vector3(0.2, 0.2, 0),
                    0, Vector3(0.3,0.3,0.3), true,option)
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            _QuestManager.mana = _QuestManager.mana + 1
            if _QuestManager.mana == 3 then
                _QuestManager.AlexaMana = true
            end
            self.Entity.Enable = false
            _ParticleService:RemoveParticle(self.particleID)
        }
}