using System;
using System.Collections.Generic;

namespace Plugins.Injection.Interfaces
{
    public interface IInjectionContainer
    {
        public void Register<T>(T instance) where T : class;
        public void Register<T>(T instance, Type type) where T : class;
        public T Get<T>() where T : class;
        public T New<T>() where T : class, new();
        public T NewAndRegister<T>() where T : class, new();
        public T1 NewAndRegister<T1, T2>() where T1 : class, T2, new() where T2 : class;
        public Dictionary<Type, object> GetRegistrations();
        public void CopyRegistrationsFrom(IInjectionContainer otherContainer);
    }
}