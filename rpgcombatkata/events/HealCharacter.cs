namespace rpgcombatkata.events {
    public class HealCharacter {
        
        public int SourceCharacterId { get; }
        public int TargetCharacterId { get; }
        public int Points { get; }
        
        public HealCharacter(int sourceCharacterId, int targetCharacterId, int points) {
            SourceCharacterId = sourceCharacterId;
            TargetCharacterId = targetCharacterId;
            Points = points;
        }
    }
}