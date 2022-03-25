using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TreasuryBondPrice.Core.Model
{

    public partial class NationalTreasury
    {
        [JsonProperty("responseStatus")]
        public long ResponseStatus { get; set; }

        [JsonProperty("responseStatusText")]
        public string ResponseStatusText { get; set; }

        [JsonProperty("statusInfo")]
        public string StatusInfo { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("BdTxTp")]
        public BdTxTp BdTxTp { get; set; }

        [JsonProperty("TrsrBondMkt")]
        public TrsrBondMkt TrsrBondMkt { get; set; }

        [JsonProperty("TrsrBdTradgList")]
        public List<TrsrBdTradgList> TrsrBdTradgList { get; set; }

        [JsonProperty("BizSts")]
        public BizSts BizSts { get; set; }
    }

    public partial class BdTxTp
    {
        [JsonProperty("cd")]
        public long Cd { get; set; }
    }

    public partial class BizSts
    {
        [JsonProperty("cd")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Cd { get; set; }

        [JsonProperty("dtTm")]
        public DateTimeOffset DtTm { get; set; }
    }

    public partial class TrsrBdTradgList
    {
        [JsonProperty("TrsrBd")]
        public TrsrBd TrsrBd { get; set; }
    }

    public partial class TrsrBd
    {
        [JsonProperty("cd")]
        public long Cd { get; set; }

        [JsonProperty("nm")]
        public string Nm { get; set; }

        [JsonProperty("featrs")]
        public string Featrs { get; set; }

        [JsonProperty("mtrtyDt")]
        public DateTimeOffset MtrtyDt { get; set; }

        [JsonProperty("minInvstmtAmt")]
        public double MinInvstmtAmt { get; set; }

        [JsonProperty("untrInvstmtVal")]
        public double BuyValue { get; set; }

        [JsonProperty("invstmtStbl")]
        public string InvstmtStbl { get; set; }

        [JsonProperty("semiAnulIntrstInd")]
        public bool SemiAnulIntrstInd { get; set; }

        [JsonProperty("rcvgIncm")]
        public string RcvgIncm { get; set; }

        [JsonProperty("anulInvstmtRate")]
        public double AnulInvstmtRate { get; set; }

        [JsonProperty("anulRedRate")]
        public double AnulRedRate { get; set; }

        [JsonProperty("minRedQty")]
        public double MinRedQty { get; set; }

        [JsonProperty("untrRedVal")]
        public double SellValue { get; set; }

        [JsonProperty("minRedVal")]
        public double MinRedVal { get; set; }

        [JsonProperty("isinCd")]
        public string IsinCd { get; set; }

        [JsonProperty("FinIndxs")]
        public FinIndxs FinIndxs { get; set; }

        [JsonProperty("wdwlDt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? WdwlDt { get; set; }
    }

    public partial class FinIndxs
    {
        [JsonProperty("cd")]
        public long Cd { get; set; }

        [JsonProperty("nm")]
        public Nm Nm { get; set; }
    }

    public partial class TrsrBondMkt
    {
        [JsonProperty("opngDtTm")]
        public DateTimeOffset OpngDtTm { get; set; }

        [JsonProperty("clsgDtTm")]
        public DateTimeOffset ClsgDtTm { get; set; }

        [JsonProperty("qtnDtTm")]
        public DateTimeOffset QtnDtTm { get; set; }

        [JsonProperty("stsCd")]
        public long StsCd { get; set; }

        [JsonProperty("sts")]
        public string Sts { get; set; }
    }

    public enum Nm { IgpM, Ipca, Prefixado, Selic };

    public partial class NationalTreasury
    {
        public static NationalTreasury FromJson(string json) => JsonConvert.DeserializeObject<NationalTreasury>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this NationalTreasury self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                NmConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class NmConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Nm) || t == typeof(Nm?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "IGP-M":
                    return Nm.IgpM;
                case "IPCA":
                    return Nm.Ipca;
                case "PREFIXADO":
                    return Nm.Prefixado;
                case "SELIC":
                    return Nm.Selic;
            }
            throw new Exception("Cannot unmarshal type Nm");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Nm)untypedValue;
            switch (value)
            {
                case Nm.IgpM:
                    serializer.Serialize(writer, "IGP-M");
                    return;
                case Nm.Ipca:
                    serializer.Serialize(writer, "IPCA");
                    return;
                case Nm.Prefixado:
                    serializer.Serialize(writer, "PREFIXADO");
                    return;
                case Nm.Selic:
                    serializer.Serialize(writer, "SELIC");
                    return;
            }
            throw new Exception("Cannot marshal type Nm");
        }

        public static readonly NmConverter Singleton = new NmConverter();
    }
}
