using AssetPriceTrigger.DTO;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreasuryBondPrice.Core.Model;

namespace AssetPriceTrigger.Handler
{
    public class HandleCosmosDbMapping
    {

        private static string[] EXCLUDE_TABLE_ENTITY_KEYS = { "PartitionKey", "RowKey", "odata.etag", "Timestamp" };

        internal static TreasuryBondPriceDTO MapTableEntityToModel(TableEntity tableEntity)
        {
            var treasuryBond = new TreasuryBondPriceDTO();
            treasuryBond.Type = tableEntity.PartitionKey;
            treasuryBond.Date = tableEntity.RowKey;
            treasuryBond.Timestamp = tableEntity.Timestamp;
            treasuryBond.Etag = tableEntity.ETag.ToString();

            var items = tableEntity.Keys.Where(key => !EXCLUDE_TABLE_ENTITY_KEYS.Contains(key));
            foreach (var key in items)
            {
                treasuryBond[key] = tableEntity[key];
            }

            return treasuryBond;
        }

    }
}
