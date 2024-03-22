using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace ZyaelWeb_Services
{
    public class common
    {


        public static string PasswordEncription(string Password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1pwd = sha1.ComputeHash(Encoding.UTF8.GetBytes(Password));
            var hashedPassword = Convert.ToBase64String(sha1pwd).Replace('+', '-').Replace('/', '_');
            return hashedPassword;
        }
        //public static string PasswordDecryption(string Password)
        //{
            

        //}

        public static string contrainingString = GetConnectionKey("DefautConnection");
        public static string GetConnectionKey(string Key)
        {
            Configuration config = null;
            string ConfigValue = string.Empty;
            string exeConfigPath = typeof(common).Assembly.Location;
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(exeConfigPath);
            }
            catch(Exception ex)
            {
                return null;
            }
            if(config != null)
            {
                ConfigValue = GetAppSetting(config, Key);
            }
            return ConfigValue;
        }
        public static string GetAppSetting(Configuration Config,string key)
        {
            KeyValueConfigurationElement element = Config.AppSettings.Settings[key];
            if(element != null)
            {
                string value = element.Value;
                if (!string.IsNullOrEmpty(value))
                    return value;
            }
            return string.Empty;
        }

        public string replaceSingleQuote(string CompleteString)
        {
            CompleteString = CompleteString.Replace("''", "'");
            return CompleteString;
        }
        public string GetredirectionModule(string tablename)
        {
            Dictionary<string, string> RedirectionModuleList = new Dictionary<string, string>
            {
                {"checkout","checkout"},
            };
            return RedirectionModuleList[tablename];
        }


        public Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };

        }
    }
}
