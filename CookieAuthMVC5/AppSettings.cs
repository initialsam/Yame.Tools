﻿using System;
using System.Configuration;

namespace CookieAuthMVC5
{
    /// <summary>
    /// This class was generated by the AppSettings T4 template
    /// </summary>
    public static class AppSettings
    {
        public static string webpages_Version { get { return GetConfigSettingItem("webpages:Version"); } }
        public static string webpages_Enabled { get { return GetConfigSettingItem("webpages:Enabled"); } }
        public static string ClientValidationEnabled { get { return GetConfigSettingItem("ClientValidationEnabled"); } }
        public static string UnobtrusiveJavaScriptEnabled { get { return GetConfigSettingItem("UnobtrusiveJavaScriptEnabled"); } }
    
        private const string MISSING_CONFIG = "Invalid configuration. Required AppSettings section is missing";
        private const string INVALID_CONFIG_SETTING = "Invalid configuration setting name: {0}";

        private static string GetConfigSettingItem(string name)
        {
            if (ConfigurationManager.AppSettings == null)
                throw new ConfigurationErrorsException(MISSING_CONFIG);

            string value = null;
            if (ConfigurationManager.AppSettings.Count != 0)
            {
                try
                {
                    value = ConfigurationManager.AppSettings.Get(name);
                }
                catch (Exception exception)
                {
                    throw new ConfigurationErrorsException(SettingItemErrorMessage(name, exception));
                }
            }
            return value;
        }

        private static string SettingItemErrorMessage(string name)
        {
            return string.Format(INVALID_CONFIG_SETTING, name);
        }

        private static string SettingItemErrorMessage(string name, Exception exception)
        {
            return string.Format(INVALID_CONFIG_SETTING, name) + exception.Message;
        }
    }
}
