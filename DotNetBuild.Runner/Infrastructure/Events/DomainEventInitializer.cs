using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotNetBuild.Runner.Infrastructure.Events
{
    public interface IDomainEventInitializer
    {
        T Initialize<T>(T obj) where T : class;
    }

    public class DomainEventInitializer
        : IDomainEventInitializer
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public DomainEventInitializer(IDomainEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        public T Initialize<T>(T obj) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var seen = new HashSet<object>();

            InitializeObject(obj, seen, @event => _domainEventDispatcher.Dispatch(@event));
            return obj;
        }

        private void InitializeObject<TClass>(TClass obj, HashSet<object> seen, DomainEvent eventHandler)
            where TClass : class
        {
            Set(obj, eventHandler, seen);
            Dig(obj, eventHandler, seen);
        }

        private static void Set<TClass>(TClass obj, DomainEvent @delegate, ISet<object> seen)
            where TClass : class
        {
            if (obj == null)
                return;

            if (seen.Contains(obj))
                return;

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            var events = obj.GetType().GetEvents(bindingFlags);
            var selectedEvents = events.Where(@event => @event.EventHandlerType == typeof(DomainEvent));
            var fields = GetAllFields(obj.GetType());
            var selectedFields = selectedEvents.Select(e => fields.SingleOrDefault(f => string.Equals(f.Name, e.Name, StringComparison.OrdinalIgnoreCase))).ToList();
            selectedFields.ForEach(x =>
                {
                    if (x == null)
                    {
                        return;
                    }

                    x.SetValue(obj, @delegate);
                });

            seen.Add(obj);
        }

        private static IEnumerable<FieldInfo> GetAllFields(Type t)
        {
            if (t == null)
                return Enumerable.Empty<FieldInfo>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            return t.GetFields(flags).Concat(GetAllFields(t.BaseType));
        }

        private void Dig<TClass>(TClass obj, DomainEvent eventHandler, HashSet<object> seen)
            where TClass : class
        {
            if (obj == null)
                return;

            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (IsCollectionType(prop.PropertyType))
                {
                    InitializeItemsInCollection(obj, eventHandler, seen, prop);
                }

                if (!IsClassWithDomainEvents(prop))
                    continue;

                var objectWithEvents = prop.GetValue(obj, null);

                if (objectWithEvents == null)
                    continue;

                InitializeObject(objectWithEvents, seen, eventHandler);
            }
        }

        private void InitializeItemsInCollection<TClass>(TClass obj, DomainEvent eventHandler, HashSet<object> seen, PropertyInfo prop)
            where TClass : class
        {
            var collection = (IEnumerable) prop.GetValue(obj, null);

            if (collection == null)
                return;

            foreach (var item in collection)
            {
                if (seen.Contains(item))
                    return;

                InitializeObject(item, seen, eventHandler);
            }
        }

        private static bool IsCollectionType(Type propertyType)
        {
            return typeof (IEnumerable).IsAssignableFrom(propertyType) && propertyType != typeof (string);
        }

        private bool IsClassWithDomainEvents(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.GetEvents().Any(@event => @event.EventHandlerType == typeof(DomainEvent));
        }
    }
}