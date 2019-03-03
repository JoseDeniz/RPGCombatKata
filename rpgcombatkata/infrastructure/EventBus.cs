using System;
using System.Collections.Concurrent;
using System.Linq;

namespace rpgcombatkata.infrastructure {
        public class EventBus {
            private static ConcurrentDictionary<int, Subscriber> subscribers = new ConcurrentDictionary<int, Subscriber>();
    
            private EventBus() { }
            
            public static void Subscribe<T>(Action<T> handler){
                var anonymousSubscriber = new AnonymousSubscriber<T>(handler);
                subscribers.TryAdd(anonymousSubscriber.GetHashCode(), anonymousSubscriber);
            }

            public static void Unsubscribe<T>(Action<T> handler){
                Subscriber anonymousSubscriber = new AnonymousSubscriber<T>(handler);
                subscribers.TryRemove(anonymousSubscriber.GetHashCode(),
                    out anonymousSubscriber);
            }
    
            public static void Raise<T>(T domainEvent){
                Exception anyExceptionRaisedByASubscriber = null;
                foreach (var handlerOfT in subscribers.Values.OfType<Subscriber<T>>()) {
                    try{
                        handlerOfT.Handle(domainEvent);
                    }
                    catch (Exception ex){
                        anyExceptionRaisedByASubscriber = ex;
                    }
                }
                if (anyExceptionRaisedByASubscriber != null){
                    throw new ListenerException(anyExceptionRaisedByASubscriber);
                }
            }
    
            public static void Clean() {
                subscribers = null;
                subscribers = new ConcurrentDictionary<int, Subscriber>();
            }
            
            private class AnonymousSubscriber<T> : Subscriber<T>{
                private readonly Action<T> handler;

                public AnonymousSubscriber(Action<T> handler){
                    this.handler = handler;
                }
                public void Handle(T domainEvent){
                    handler(domainEvent);
                }

                public override bool Equals(object obj){
                    return handler == ((AnonymousSubscriber<T>)obj).handler;
                }

                public override int GetHashCode() {
                    return (handler != null ? handler.GetHashCode() : 0);
                }
            }
        }
    }