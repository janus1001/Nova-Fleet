using NovaFleetCore.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.AbilitySystem
{
    /// <summary>
    /// Module cards are equipped on units and held in hand and deck.
    /// </summary>
    public class ModuleCard : IContainer
    {
        public string name;
        public string description;
        public AbilityType type;
        public int cost;

        Dictionary<string, IAspect> moduleComponents = new Dictionary<string, IAspect>();

        public ModuleCard(string name, string description, AbilityType type, int cost)
        {
            this.name = name;
            this.description = description;
            this.type = type;
            this.cost = cost;
        }

        //public T CreateNewAspect<T>(string key = null) where T : IAspect, new()
        //{
        //    return AddAspect(new T());
        //}

        public T AddAspect<T>(T aspect, string key = null) where T : IAspect
        {
            moduleComponents.Add(key, aspect);
            return aspect;
        }

        public ICollection<IAspect> Aspects()
        {
            return moduleComponents.Values;
        }

        public T GetAspect<T>(string key = null) where T : IAspect
        {
            key = key ?? typeof(T).Name;
            T aspect = moduleComponents.ContainsKey(key) ? (T)moduleComponents[key] : default(T);
            return aspect;
        }

        public override string ToString()
        {
            return $"[{type}] {name} ({cost})\n{moduleComponents.Count} effect(s)\n{description}";
        }
    }
}
