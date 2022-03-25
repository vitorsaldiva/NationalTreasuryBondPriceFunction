using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreasuryBondPrice.Core.Model;
using TreasuryBondPrice.Core.Repository;
using Azure.Data.Tables;
using System.Linq;
using AssetPriceTrigger.Handler;
using AssetPriceTrigger.DTO;
using System.Threading;
using System.Web;

namespace AssetPriceTrigger.Repository
{
    public class TreasuryRepository : IRepository
    {
        private readonly TableClient tableClient;
        public TreasuryRepository()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=asset-trg;AccountKey=yhYb5tE9v2wKA893BWkIHIymkv2DixvgtjxKgvCzqloHur32vQh1LfgzxTKhCSD0N582ZiJXcUa9BtjJJ0EVAA==;TableEndpoint=https://asset-trg.table.cosmos.azure.com:443/;";
            tableClient = new TableClient(connectionString, "TreasuryBondPriceHistory");
        }
        public Task<IEnumerable<TreasuryBond>> GetAllAsync()
        {
            var treasuryBonds = tableClient.Query<TableEntity>();

            return Task.FromResult(treasuryBonds.Select(e => HandleCosmosDbMapping.MapTableEntityToModel(e)).Cast<TreasuryBond>());

        }

        public Task<IEnumerable<TreasuryBond>> GetByTypeAsync(string treasuryType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TreasuryBond>> GetByYearAsync(string year)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(TreasuryBond treasuryBond)
        {
            TableEntity entity = new TableEntity();
            entity.PartitionKey = HttpUtility.HtmlDecode(treasuryBond.Name);
            entity.RowKey = Guid.NewGuid().ToString();
            entity["BondName"] = HttpUtility.HtmlDecode(treasuryBond.Name);
            entity["SalePrice"] = treasuryBond.SaleValue;
            entity["BuyPrice"] = treasuryBond.BuyValue;
            entity["Date"] = DateTime.UtcNow.AddHours(-3).ToString("yyyy-MM-dd HH:mm");

            // The other values are added like a items to a dictionary
            //entity["Temperature"] = model.Temperature;
            //entity["Humidity"] = model.Humidity;
            //entity["Barometer"] = model.Barometer;
            //entity["WindDirection"] = model.WindDirection;
            //entity["WindSpeed"] = model.WindSpeed;
            //entity["Precipitation"] = model.Precipitation;

            await tableClient.AddEntityAsync(entity);
        }
    }
}
