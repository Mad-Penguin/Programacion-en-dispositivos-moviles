﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Listing.App.Models;
//
//    var token = Token.FromJson(jsonString);

namespace Listing.App.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Token
    {
        [JsonProperty("token")]
        public string TokenToken { get; set; }

        [JsonProperty("expiration")]
        public DateTimeOffset Expiration { get; set; }
    }

    public partial class Token
    {
        public static Token FromJson(string json) => JsonConvert.DeserializeObject<Token>(json, Listing.App.Models.Converter.Settings);
    }

    public static class SerializeToken
    {
        public static string ToJson(this Token self) => JsonConvert.SerializeObject(self, Listing.App.Models.Converter.Settings);
    }

    internal static class ConverterToken
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