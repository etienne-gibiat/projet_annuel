using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.Utilities;

namespace PGSauce.Core.Strings
{
    public static class MenuPaths
    {
        public const string MenuBase = "Big Catto/";
        public const string GamePath = PGStrings.GameName + "/";
        public const string Vendor = MenuBase + "Vendor/";
        public const string Settings = MenuBase + "Settings/";
        public const string Saves = MenuBase + "Saves/";
        public const string SavedData = Saves + "Saved Data/";
        public const string GameSavedData = GamePath + "Saved Data/";
        public const string NoDevSuite = MenuBase + "NoDevSuite/";
        public const string FsmExample = MenuBase + "Fsm/Example/";
        public const string SelectObject = MenuBase + "Select/";
        public const string Publish = MenuBase + "Publish/";
        public const string Remote = MenuBase + "Remote/";
        public const string RemoteCampaigns = Remote + "Campaigns/";
        public const string ABTest = Remote + "AB Tests/";
        public const string PublishSettings = Publish + "Build Targets/";
        public const string PublishDeployers = Publish + "Deployers/";
        public const string DataProviders = MenuBase + "Data Providers/";
        public const string DataProvidersGame = GamePath + "Data Providers/";
        public const string DataProvidersBool = DataProviders + "Bool/";
        public const string DataProvidersFloat = DataProviders + "Float/";
        public const string DataProvidersFloat01Game = DataProvidersGame + "Float 01/";
        public const string UI = MenuBase + "UI/";
        public const string UIAnimations = UI + "Animations/";
        public const string Iap = MenuBase + "IAP/";
        public const string Audio = MenuPaths.MenuBase + "Audio/";
        public const string PlayFab = MenuBase + "PlayFab/";
        public const string InputValidators = MenuBase + "Input Validators/";
        public const string InputValidatorsConditions = InputValidators + "Conditions/";
        public const string InputValidatorsConditionsEmails = InputValidatorsConditions + "Email/";
        public const string Inventory = GamePath + "Inventory/";
    }
}
