using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NovaFleetCore.AbilitySystem
{
    public static class AbilityLoader
    {
        static string nameRegex = "Name:(.*)";
        static string descriptionRegex = "Description:(.*)";
        static string typeRegex = "Type:(.*)";
        static string costRegex = "Cost:(.*)";

        public static ModuleCard LoadAbility(string moduleString)
        {
            string name = Regex.Match(moduleString, nameRegex).Groups[0].Value;
            string description = Regex.Match(moduleString, descriptionRegex).Groups[0].Value;
            AbilityType abilityType;

            switch (Regex.Match(moduleString, typeRegex).Groups[1].Value)
            {
                case "A":
                    abilityType = AbilityType.Attack;
                    break;
                case "M":
                    abilityType = AbilityType.Movement;
                    break;
                case "U":
                    abilityType = AbilityType.Upgrade;
                    break;
                default:
                    return null;
            }

            int cost;
            if(int.TryParse(Regex.Match(moduleString, costRegex).Groups[0].Value, out int parsedCost))
            {
                cost = parsedCost;
            }
            else
            {
                return null;
            }

            ModuleCard loadedModuleCard = new ModuleCard(name, description, abilityType, cost);


            
            return loadedModuleCard;
        }
    }
}
