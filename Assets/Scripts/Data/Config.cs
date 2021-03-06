// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var config = Config.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Config
    {
        [JsonProperty("start_food")]
        public long StartFood { get; set; }

        [JsonProperty("craft_food")]
        public long CraftFood { get; set; }

        [JsonProperty("tower")]
        public Tower Tower { get; set; }

        [JsonProperty("fontan")]
        public Fontan Fontan { get; set; }

        [JsonProperty("units")]
        public List<Unit> Units { get; set; }
    }

    public partial class Fontan
    {
        [JsonProperty("attack_tower")]
        public long AttackTower { get; set; }

        [JsonProperty("time_capture")]
        public float TimeCapture { get; set; }
    }

    public partial class Tower
    {
        [JsonProperty("hp")]
        public long Hp { get; set; }

        [JsonProperty("attack")]
        public long Attack { get; set; }

        [JsonProperty("speed_attack")]
        public double SpeedAttack { get; set; }
    }

    public partial class Unit
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("hp")]
        public long Hp { get; set; }

        [JsonProperty("attack")]
        public long Attack { get; set; }

        [JsonProperty("speed_attack")]
        public double SpeedAttack { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }
    }

    public partial class Config
    {
        public static Config FromJson(string json) => JsonConvert.DeserializeObject<Config>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Config self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
