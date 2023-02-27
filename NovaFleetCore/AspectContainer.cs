using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.Structures
{
    public interface IContainer
    {
        // T CreateNewAspect<T>(string key = null) where T : IAspect, new();
        T AddAspect<T>(T aspect, string key = null) where T : IAspect;
        T GetAspect<T>(string key = null) where T : IAspect;
        ICollection<IAspect> Aspects();
    }

    public interface IAspect
    {
        IContainer container { get; set; }
    }

    public class Container : IContainer
    {
        Dictionary<string, IAspect> aspects = new Dictionary<string, IAspect>();

        public T CreateNewAspect<T>(string key = null) where T : IAspect, new()
        {
            return AddAspect(new T(), key);
        }

        public T AddAspect<T>(T aspect, string key = null) where T : IAspect
        {
            key = key ?? typeof(T).Name;
            aspects.Add(key, aspect);
            aspect.container = this;
            return aspect;
        }

        public ICollection<IAspect> Aspects ()
        { 
            return aspects.Values; 
        }

        public T GetAspect<T>(string key = null) where T : IAspect
        {
            key = key ?? typeof(T).Name;
            T aspect = aspects.ContainsKey(key) ? (T)aspects[key] : default(T);
            return aspect;
        }
    }

    public class Aspect : IAspect
    {
        public IContainer container { get; set; }
    }
}
