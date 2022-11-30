using NftHigherOrLowerGame.Model.DataBaseModels;
using Supabase;

namespace NftHigherOrLowerGame.Model
{
    public class SupabaseNFT
    {
        private const string url = "https://knddzfdbybulsywhyrvt.supabase.co";
        private const string anon_key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImtuZGR6ZmRieWJ1bHN5d2h5cnZ0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NDUyMDE1NDMsImV4cCI6MTk2MDc3NzU0M30.gruYB1hgInt484DqZ_0xP86cX0h0HzHc1HYFoYkrxU0";

        private Client client;


        public SupabaseNFT()
        {
            client = new Client(url, anon_key);
            Task.Run(async () => { await client.InitializeAsync(); });
        }

        public async Task<NFT> FetchNFT()
        {
            var response = await client.From<NFT>().Get();
            var nft = response.Models.First();
            return nft;
        }
    }
}
