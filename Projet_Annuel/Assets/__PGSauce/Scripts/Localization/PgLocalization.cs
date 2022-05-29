using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using I2.Loc;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using Sirenix.OdinInspector;

namespace PGSauce.Localization
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "PG Localization")]
    public class PgLocalization : SOSingleton<PgLocalization>
    {
        #region Public And Serialized Fields

        [SerializeField] private LanguageSourceAsset i2Localization;
        #endregion

        #region Private Fields

        #endregion

        #region Properties
        public static string CurrentLanguageCode{
            get => LocalizationManager.CurrentLanguageCode;
            set => LocalizationManager.CurrentLanguageCode = value;
        }

        public static string CurrentLanguage
        {
            get => LocalizationManager.CurrentLanguage;
            set => LocalizationManager.CurrentLanguage = value;
        }

        [ShowInInspector, CalledByOdin] public static List<string> Terms => Instance.GetTerms();
        [ShowInInspector, CalledByOdin] public static List<string> TermCategories => Instance.GetTermCategories();

        #endregion

        #region Unity Functions
        #endregion

        #region Public Methods
        public List<string> GetTerms(string category = null)
        {
            if (!i2Localization)
            {
                return new List<string>();
            }
            return i2Localization.SourceData.GetTermsList(category);
        }
        
        public List<string> GetTermCategories()
        {
            return i2Localization.SourceData.GetCategories();
        }
        
        public string GetTranslation(string term)
        {
            return LocalizationManager.GetTranslation(term);
        }
        #endregion

        #region Private Methods

        #endregion

        
    }
}
