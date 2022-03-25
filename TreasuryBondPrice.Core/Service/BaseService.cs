using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using TreasuryBondPrice.Core.Model;
using System.Text.Json;

namespace TreasuryBondPrice.Core.Service
{
    public abstract class BaseService
    {
        protected string BaseUrl { get; } = "https://www.tesourodireto.com.br/json/br/com/b3/tesourodireto/service/api/treasurybondsinfo.json";

        private readonly static HttpClient httpClient = new HttpClient();

        protected async Task<NationalTreasury> GetTreasuries()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            var response = await httpClient.GetStringAsync(BaseUrl);

            return NationalTreasury.FromJson(response);
        }

        protected abstract TreasuryBond GetTreasuryBond(NationalTreasury payload, string year);
    }
}
