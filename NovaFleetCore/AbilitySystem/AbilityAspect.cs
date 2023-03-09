using NovaFleetCore.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaFleetCore.AbilitySystem
{
    /// <summary>
    /// Class that holds data about 
    /// </summary>
    public class AbilityAspect : IAspect
    {
        internal string aspectName;

        // Parent ability container
        public IContainer parentContainer { get; set; }


    }
}
