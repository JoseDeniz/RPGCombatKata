using rpgcombatkata.entities;

namespace rpgcombatkata.events {
    public class AttackCharacter {
        public Character SourceCharacter { get; }
        public Character TargetCharacter { get; }
        public int Points { get; }
        
        public AttackCharacter(Character sourceCharacter, Character targetCharacter, int points) {
            SourceCharacter = sourceCharacter;
            TargetCharacter = targetCharacter;
            Points = points;
        }
    }
}