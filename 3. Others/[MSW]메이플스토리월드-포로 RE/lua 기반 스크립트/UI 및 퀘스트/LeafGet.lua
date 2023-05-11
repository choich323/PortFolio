LeafGet{
    -- Day3 낮 진행 중 약초를 얻기 위한 코드
    Property:
        [Sync]
        number particleID = 0

    Function:
        [client only]
        void OnBeginPlay()
        {
            if _QuestManager.CherryQ == false then
                self.Entity.Enable = false
            else
                local option = {["Color"] = Color(1,0,1,1)}	
                self.particleID = _ParticleService:PlayBasicParticle(
                    BasicParticleType.Nova, self.Entity, 
                    self.Entity.TransformComponent.Position, 0, 
                    Vector3(0.3,0.3,0.3), true,option)
            end
        }

    EntityEventHandler:
        [self]
        HandleTouchEvent(TouchEvent event)
        {
            -- Parameters
            --------------------------------------------------------
            _QuestManager.leaf = _QuestManager.leaf + 1
            if _QuestManager.leaf == 3 then
                _QuestManager.CherryHeal = true
            end
            self.Entity.Enable = false
            _ParticleService:RemoveParticle(self.particleID)
        }
}