namespace rpgcombatkata.infrastructure {
    public interface Subscriber { }

    public interface Subscriber<in T> : Subscriber {
        void Handle(T domainEvent);
    }
}