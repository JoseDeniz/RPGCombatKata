using rpgcombatkata.entities.character;

namespace rpgcombatkata.events {
    public class AttackCharacter {
        public Character SourceCharacter { get; }
        public Character TargetCharacter { get; }
        public int Points { get; }
        public int Range { get; }
        
        public AttackCharacter(Character sourceCharacter, Character targetCharacter, int points, int range) {
            SourceCharacter = sourceCharacter;
            TargetCharacter = targetCharacter;
            Points = points;
            Range = range;
        }
    }
}