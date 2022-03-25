using System;
using System.Collections.Generic;
using System.Text;

namespace AssetPriceTrigger.DTO
{
    internal class TreasuryBondPriceDTO
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public string Type { get; set; }

        public string Title { get; set; }
        public string Price { get; set; }

        public string Date { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public string Etag { get; set; }

        public object this[string name]
        {
            get => (ContainsProperty(name)) ? _properties[name] : null;
            set => _properties[name] = value;
        }

        public ICollection<string> PropertyNames => _properties.Keys;

        public int PropertyCount => _properties.Count;

        public bool ContainsProperty(string name) => _properties.ContainsKey(name);
    }
}
