﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sannsyn.Episerver.Commerce.Models
{
    public class SannsynUpdateModel
    {
        [JsonProperty(PropertyName = "service")]
        public string Service { get; set; }
        [JsonProperty(PropertyName = "updates")]
        public List<SannsynUpdateEntityModel> Updates { get; set; }
    }
}
