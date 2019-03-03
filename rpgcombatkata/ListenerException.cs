using System;

namespace rpgcombatkata {
    public class ListenerException : Exception{
        public ListenerException(Exception innerException): base("", innerException){}
    }
}