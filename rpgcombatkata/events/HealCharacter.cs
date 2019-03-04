using rpgcombatkata.entities;

namespace rpgcombatkata.events {
    public class HealCharacter {
        
        public int SourceCharacterId { get; }
        public int TargetCharacterId { get; }
        
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