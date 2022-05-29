using UnityEngine;
using I2.Loc;

namespace PGSauce.Localization
{
	public static class ScriptLocalization
	{

		public static class AppUpdateErrorCode
		{
			public static string ErrorUserCanceled 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/ErrorUserCanceled", applyParameters : true); } }
			public static string ErrorDownloadNotPresent 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/ErrorDownloadNotPresent", applyParameters : true); } }
			public static string ErrorInternalError 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/ErrorInternalError", applyParameters : true); } }
			public static string ErrorAppNotOwned 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/ErrorAppNotOwned", applyParameters : true); } }
			public static string ErrorPlayStoreNotFound 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/ErrorPlayStoreNotFound", applyParameters : true); } }
			public static string ErrorUnknown 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/ErrorUnknown", applyParameters : true); } }
			public static string ErrorUpdateFailed 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/ErrorUpdateFailed", applyParameters : true); } }
			public static string ErrorUpdateUnavailable 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/ErrorUpdateUnavailable", applyParameters : true); } }
			public static string ErrorUpdateNotAllowed 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/ErrorUpdateNotAllowed", applyParameters : true); } }
			public static string NoError 		{ get{ return LocalizationManager.GetTranslation ("AppUpdateErrorCode/NoError", applyParameters : true); } }
		}

		public static class BoxingCategories
		{
			public static string Medium 		{ get{ return LocalizationManager.GetTranslation ("BoxingCategories/Medium", applyParameters : true); } }
			public static string Light 		{ get{ return LocalizationManager.GetTranslation ("BoxingCategories/Light", applyParameters : true); } }
			public static string Heavy 		{ get{ return LocalizationManager.GetTranslation ("BoxingCategories/Heavy", applyParameters : true); } }
		}

		public static string TryTitle 		{ get{ return LocalizationManager.GetTranslation ("TryTitle", applyParameters : true); } }
		public static string TryAd 		{ get{ return LocalizationManager.GetTranslation ("TryAd", applyParameters : true); } }
		public static string CoolDown 		{ get{ return LocalizationManager.GetTranslation ("CoolDown", applyParameters : true); } }
		public static string CurrentLanguage 		{ get{ return LocalizationManager.GetTranslation ("CurrentLanguage", applyParameters : true); } }

		public static class FormErrors
		{
			public static string AccountAlreadyLinked 		{ get{ return LocalizationManager.GetTranslation ("FormErrors/AccountAlreadyLinked", applyParameters : true); } }
			public static string EmailInUse 		{ get{ return LocalizationManager.GetTranslation ("FormErrors/EmailInUse", applyParameters : true); } }
			public static string GenericError 		{ get{ return LocalizationManager.GetTranslation ("FormErrors/GenericError", applyParameters : true); } }
			public static string InvalidEmail 		{ get{ return LocalizationManager.GetTranslation ("FormErrors/InvalidEmail", applyParameters : true); } }
			public static string InvalidPassword 		{ get{ return LocalizationManager.GetTranslation ("FormErrors/InvalidPassword", applyParameters : true); } }
			public static string Password 		{ get{ return LocalizationManager.GetTranslation ("FormErrors/Password", applyParameters : true); } }
			public static string ServiceUnavailable 		{ get{ return LocalizationManager.GetTranslation ("FormErrors/ServiceUnavailable", applyParameters : true); } }
		}

		public static class PlayerStatus
		{
			public static string Stun 		{ get{ return LocalizationManager.GetTranslation ("PlayerStatus/Stun", applyParameters : true); } }
		}
	}

    public static class ScriptTerms
	{

		public static class AppUpdateErrorCode
		{
		    public const string ErrorUserCanceled = "AppUpdateErrorCode/ErrorUserCanceled";
		    public const string ErrorDownloadNotPresent = "AppUpdateErrorCode/ErrorDownloadNotPresent";
		    public const string ErrorInternalError = "AppUpdateErrorCode/ErrorInternalError";
		    public const string ErrorAppNotOwned = "AppUpdateErrorCode/ErrorAppNotOwned";
		    public const string ErrorPlayStoreNotFound = "AppUpdateErrorCode/ErrorPlayStoreNotFound";
		    public const string ErrorUnknown = "AppUpdateErrorCode/ErrorUnknown";
		    public const string ErrorUpdateFailed = "AppUpdateErrorCode/ErrorUpdateFailed";
		    public const string ErrorUpdateUnavailable = "AppUpdateErrorCode/ErrorUpdateUnavailable";
		    public const string ErrorUpdateNotAllowed = "AppUpdateErrorCode/ErrorUpdateNotAllowed";
		    public const string NoError = "AppUpdateErrorCode/NoError";
		}

		public static class BoxingCategories
		{
		    public const string Medium = "BoxingCategories/Medium";
		    public const string Light = "BoxingCategories/Light";
		    public const string Heavy = "BoxingCategories/Heavy";
		}

		public const string TryTitle = "TryTitle";
		public const string TryAd = "TryAd";
		public const string CoolDown = "CoolDown";
		public const string CurrentLanguage = "CurrentLanguage";

		public static class FormErrors
		{
		    public const string AccountAlreadyLinked = "FormErrors/AccountAlreadyLinked";
		    public const string EmailInUse = "FormErrors/EmailInUse";
		    public const string GenericError = "FormErrors/GenericError";
		    public const string InvalidEmail = "FormErrors/InvalidEmail";
		    public const string InvalidPassword = "FormErrors/InvalidPassword";
		    public const string Password = "FormErrors/Password";
		    public const string ServiceUnavailable = "FormErrors/ServiceUnavailable";
		}

		public static class PlayerStatus
		{
		    public const string Stun = "PlayerStatus/Stun";
		}
	}
}