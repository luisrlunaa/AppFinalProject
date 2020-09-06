using System;
using Newtonsoft.Json;

namespace Proyectofinal
{
    public class Album
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("id")]
        public int AlbumId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
