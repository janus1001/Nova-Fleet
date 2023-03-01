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
            string nameMatch = Regex.Match(moduleString, nameRegex).Groups[1].Value;
            string descriptionMatch = Regex.Match(moduleString, descriptionRegex).Groups[1].Value;
            AbilityType abilityType;

            string typeMatch = Regex.Match(moduleString, typeRegex).Groups[1].Value;
            switch (typeMatch)
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

            string costMatch = Regex.Match(moduleString, costRegex).Groups[1].Value;
            int cost;
            if(int.TryParse(costMatch, out int parsedCost))
            {
                cost = parsedCost;
            }
            else
            {
                return null;
            }

            ModuleCard loadedModuleCard = new ModuleCard(nameMatch, descriptionMatch, abilityType, cost);

            System.IO.StringReader stringReader = new System.IO.StringReader(moduleString);

            while (stringReader.Peek() != -1)
            {
                string label = stringReader.ReadLine();
                string match = Regex.Match(label, @"--(\w+)--").Groups[1].Value;
            }

            return loadedModuleCard;
        }
    }
}
