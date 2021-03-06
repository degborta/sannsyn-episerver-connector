﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sannsyn.Episerver.Commerce.Models
{
    public class SannsynMipRecommendModel
    {
        [JsonProperty(PropertyName = "service")]
        public string Service { get; set; }

        [JsonProperty(PropertyName = "recommender")]
        public string Recommender { get; set; }

        [JsonProperty(PropertyName = "externalIds")]
        public List<string> ExternalIds { get; set; }

        [JsonProperty(PropertyName = "n")]
        public int Number { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; }
    }
}
