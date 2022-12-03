using NftHigherOrLowerGame.Model.DataBaseModels;
using Supabase;
using System.Diagnostics;

namespace NftHigherOrLowerGame.Model
{
    public static class SupabaseNFT
    {
        private const string url = "https://knddzfdbybulsywhyrvt.supabase.co";
        private const string anon_key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImtuZGR6ZmRieWJ1bHN5d2h5cnZ0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NDUyMDE1NDMsImV4cCI6MTk2MDc3NzU0M30.gruYB1hgInt484DqZ_0xP86cX0h0HzHc1HYFoYkrxU0";

        private static Client _Client;

        private static int _NFTCount = 10000; // Fetch Actual Count Later 


        static SupabaseNFT()
        {
            _Client = new Client(url, anon_key);
            InitClient();
        }

        private static async void InitClient()
        {
            await _Client.InitializeAsync();
        }

        private static async Task<bool> FetchNFTCount()
        {
            _NFTCount = await _Client.From<NFT>().Count(Postgrest.Constants.CountType.Estimated);
            return true;
        }

        public static async Task<NFT> FetchRandomNFT()
        {
            if (_NFTCount == 0) { await FetchNFTCount(); }
            int id = Random.Shared.Next(_NFTCount);
            var response = await _Client.From<NFT>().Filter("id", Postgrest.Constants.Operator.Equals, id).Get();
            var nft = response.Models.First();
            return nft;
        }

        public static async Task<NFT> FetchNFTId(int id)
        {
            if (_NFTCount == 0) { await FetchNFTCount(); }
            if (id > _NFTCount || id < 1) { id = Random.Shared.Next(_NFTCount); }
            var response = await _Client.From<NFT>().Filter("id", Postgrest.Constants.Operator.Equals, id).Get();
            var nft = response.Models.First();
            return nft;
        }
    }
}
