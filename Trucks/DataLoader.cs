using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Trucks
{
    public class DataLoader
    {
        public List<Truck> Load(string path)
        {
            try
            {
                string content = File.ReadAllText(path);
                var tasks = JsonConvert.DeserializeObject<List<Truck>>(content);
                return tasks;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Truck>();
            }
        }

        private static string _path;

        public DataLoader(string path) => _path = path;
        
        private static void SaveFile(string content, string path)
        {
            try
            {
                File.WriteAllText(_path, content);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(path);
                SaveFile(content, _path);
            }
        }
        
        public void Save(List<Truck> trucks)
        {
            // string json = JsonConvert.SerializeObject(tasks);
            // SaveFile(json, _path);
            
            var jsonFormatter = new DataContractJsonSerializer(typeof(List<Truck>));
            using var file = new FileStream(_path, FileMode.OpenOrCreate);
            jsonFormatter.WriteObject(file, trucks);
        }
    }
}