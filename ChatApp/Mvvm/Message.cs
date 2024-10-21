using System;
using System.Collections.Generic;

namespace ChatApp.Mvvm
{
    public static class Messenger
    {
        private static readonly Dictionary<Type, List<object>> _subscribers = new Dictionary<Type, List<object>>();

        public static void Register<TMessage>(object subscriber, Action<TMessage> action)
        {
            if (!_subscribers.ContainsKey(typeof(TMessage)))
            {
                _subscribers[typeof(TMessage)] = new List<object>();
            }
            _subscribers[typeof(TMessage)].Add(new Subscription<TMessage>(subscriber, action));
        }

        public static void Unregister<TMessage>(object subscriber)
        {
            if (_subscribers.ContainsKey(typeof(TMessage)))
            {
                _subscribers[typeof(TMessage)].RemoveAll(s => ((Subscription<TMessage>)s).Subscriber == subscriber);
            }
        }

        public static void Send<TMessage>(TMessage message)
        {
            if (_subscribers.ContainsKey(typeof(TMessage)))
            {
                foreach (var subscription in _subscribers[typeof(TMessage)])
                {
                    ((Subscription<TMessage>)subscription).Action(message);
                }
            }
        }

        private class Subscription<TMessage>
        {
            public object Subscriber { get; }
            public Action<TMessage> Action { get; }

            public Subscription(object subscriber, Action<TMessage> action)
            {
                Subscriber = subscriber;
                Action = action;
            }
        }
    }
}