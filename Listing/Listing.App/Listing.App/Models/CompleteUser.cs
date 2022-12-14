// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Listing.App.Models;
//
//    var completeUser = CompleteUser.FromJson(jsonString);

namespace Listing.App.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CompleteUser
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("registrationDate")]
        public DateTimeOffset RegistrationDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public long Age { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("urlImage")]
        public string UrlImage { get; set; }

        [JsonProperty("institution")]
        public string Institution { get; set; }
    }

    public partial class CompleteUser
    {
        public static List<CompleteUser> FromJson(string json) => JsonConvert.DeserializeObject<List<CompleteUser>>(json, Listing.App.Models.Converter.Settings);
    }

    public static class SerializeCU
    {
        public static string ToJson(this List<CompleteUser> self) => JsonConvert.SerializeObject(self, Listing.App.Models.Converter.Settings);
    }

    internal static class ConverterCU
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
