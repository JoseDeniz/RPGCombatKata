namespace rpgcombatkata.events {
    public class HealCharacter {
        
        public int CharacterId { get; }
        public int Points { get; }
        
        public HealCharacter(int characterId, int points) {
            CharacterId = characterId;
            Points = points;
        }
    }
}