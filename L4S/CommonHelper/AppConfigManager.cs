using System.Collections.Generic;
using System.Configuration;

namespace CommonHelper
{
    public class AppConfigManager
    {
       

        public string ReadSetting(string key)
        {
            string result;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? string.Empty;

            }
            catch (ConfigurationErrorsException)
            {
                
                result = "Error";
            }
            return result;
        }

        public bool AddUpdateAppSettings(string key, string value)
        {
            bool result;
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                result = true;
            }
            catch (ConfigurationErrorsException)
            {
                result = false;
            }
            return result;
        }
        public Dictionary<string, string>  ReadAllSettings()
        {

            var appParams = new Dictionary<string, string>();
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    appParams["empty"] = string.Empty;
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        appParams[key] = ConfigurationManager.AppSettings[key]; 
                    }
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                appParams["exeption"] = ex.Message;
            }
            return appParams;
        }

    }
}
