using System;

namespace rpgcombatkata.infrastructure {
    public class ListenerException : Exception{
        public ListenerException(Exception innerException): base("", innerException){}
    }
}