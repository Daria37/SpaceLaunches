using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Dtos.Launches
{
    public class SpaceDevsResponce
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<SpaceDevsLaunches> Results { get; set; }
    }
}