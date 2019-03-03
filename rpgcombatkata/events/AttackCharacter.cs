namespace rpgcombatkata.events {
    public class AttackCharacter {
        public int CharacterId { get; }
        public int Points { get; }

        public AttackCharacter(int characterId, int points) {
            CharacterId = characterId;
            Points = points;
        }
    }
}