using rpgcombatkata.entities.character;
using rpgcombatkata.entities.objects;

namespace rpgcombatkata.events {
    public class AttackObject {
        public Character SourceCharacter { get; }
        public AttackableObject TargetCharacter { get; }
        public int Points { get; }

        public AttackObject(Character sourceCharacter, AttackableObject targetCharacter, int points) {
            SourceCharacter = sourceCharacter;
            TargetCharacter = targetCharacter;
            Points = points;
        }
    }
}