using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace GCalculator.Domain
{
    public class GameImporter
    {
        public static ImportedGame Import(Stream gameData)
        {
            using (var reader = new StreamReader(gameData))
            {
                string data = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ImportedGame>(data);
            }
        }
    }
}