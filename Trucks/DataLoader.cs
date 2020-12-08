using System;
using System.Collections.Generic;
using System.IO;
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
    }
}