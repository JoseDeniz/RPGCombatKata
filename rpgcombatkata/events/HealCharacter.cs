using rpgcombatkata.entities.character;

namespace rpgcombatkata.events {
    public class HealCharacter {
        public Character SourceCharacter { get; }
        public Character TargetCharacter { get; }
        public int Points { get; }
        
        public HealCharacter(Character sourceCharacter, Character targetCharacter, int points) {
            SourceCharacter = sourceCharacter;
            TargetCharacter = targetCharacter;
            Points = points;
        }
    }
}