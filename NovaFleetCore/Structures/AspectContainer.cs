using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.Structures
{
    public interface IContainer
    {
        T AddAspect<T>(string key = null) where T : IAspect, new();
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

        public T AddAspect<T>(string key = null) where T : IAspect, new()
        {
            T aspect = new T();
            aspects.Add(string.Empty, aspect);
            return aspect;
        }

        public T AddAspect<T>(T aspect, string key = null) where T : IAspect
        {
            aspects.Add(key, aspect);
            return aspect;
        }

        public ICollection<IAspect> Aspects()
        {
            return aspects.Values;
        }

        public T GetAspect<T>(string key = null) where T : IAspect
        {
            throw new NotImplementedException();
        }
    }
}
