using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
namespace NAGN_V1.Service
{
    public class JsonService
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto, 
            Formatting = Formatting.Indented,          
            NullValueHandling = NullValueHandling.Ignore
        };
        public static void SaveToJson(Model.Flow flow,string filePath)
        {
            string jsonString=JsonConvert.SerializeObject(flow, JsonSettings);
            File.WriteAllText(filePath, jsonString);
        }
        public static Model.Flow? LoadFormJson(string filePath)
        {
            if(string.IsNullOrEmpty(filePath)||!File.Exists(filePath)) return null;
            try
            {
                string jsonString = File.ReadAllText(filePath);
                var flow = JsonConvert.DeserializeObject<Model.Flow>(jsonString);
                return flow;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Cannot open file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
