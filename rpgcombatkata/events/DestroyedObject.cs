namespace rpgcombatkata.events {
    public class DestroyedObject {
        public int Id { get; }

        public DestroyedObject(int id) {
            Id = id;
        }
    }
}