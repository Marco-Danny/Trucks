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

        public void Save(List<Truck> trucks)
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(List<Truck>));
            using var file = new FileStream(_path, FileMode.OpenOrCreate);
            jsonFormatter.WriteObject(file, trucks);
        }
    }
}