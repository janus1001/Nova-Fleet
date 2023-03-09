using NovaFleetCore.Structures;
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

            int startIndex = moduleString.IndexOf(moduleBlockStartChar) + 1;
            int endIndex = moduleString.IndexOf(moduleBlockEndChar, startIndex);

            System.IO.StringReader stringReader = new System.IO.StringReader(moduleString.Substring(startIndex, endIndex - startIndex));

            List<AbilityAspect> abilityAspects = new List<AbilityAspect>();

            string currentLabel = null;
            List<string> abilityDescription = new List<string>();
            while (stringReader.Peek() != -1)
            {
                string lineText = stringReader.ReadLine();

                if (lineText.StartsWith("--"))
                {
                    // If we encounter a new label, add the previous ability (if any) and start a new one
                    if (currentLabel != null)
                    {
                        abilityAspects.Add(StringToAbility(currentLabel, abilityDescription));
                        abilityDescription.Clear();
                    }

                    currentLabel = lineText.TrimStart('-').TrimEnd('-').Trim();
                }
                else
                {
                    abilityDescription.Add(lineText.Trim());
                }
            }

            // Add the last ability (if any)
            if (currentLabel != null)
            {
                abilityAspects.Add(StringToAbility(currentLabel, abilityDescription));
            }

            return loadedModuleCard;
        }

        static AbilityAspect StringToAbility(string aspectName, List<string> aspectDetails)
        {
            AbilityAspect abilityAspect = new AbilityAspect();
            abilityAspect.aspectName = aspectName;
            abilityAspect.aspectEffects = GetEffects(aspectDetails);

            return new AbilityAspect();
        }

        const char moduleBlockStartChar = '{';
        const char moduleBlockEndChar = '}';
    }
}
