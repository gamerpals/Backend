using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Mongo;

namespace GamerPalsBackend.Other
{
    public class PalsConfiguration
    {
        public static Dictionary<string, string> SystemSettings = new Dictionary<string, string>();

        public static void UpdateSystemConfiguration()
        {
            var helper = new MongoHelper<SystemSetting>(new MongoContext());
            var settings = helper.GetAll().Result;
            lock (SystemSettings)
            {
                foreach (var setting in settings)
                {
                    SystemSettings[setting.SettingsName] = setting.SettingsValue;
                }
            }
        }
    }
}
