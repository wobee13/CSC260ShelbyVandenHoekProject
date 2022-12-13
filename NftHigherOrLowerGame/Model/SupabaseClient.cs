using NftHigherOrLowerGame.Model.DataBaseModels;
using Supabase;

namespace NftHigherOrLowerGame.Model
{
    public static class SupabaseClient
    {
        private const string url = "https://knddzfdbybulsywhyrvt.supabase.co";
        private const string anon_key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImtuZGR6ZmRieWJ1bHN5d2h5cnZ0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NDUyMDE1NDMsImV4cCI6MTk2MDc3NzU0M30.gruYB1hgInt484DqZ_0xP86cX0h0HzHc1HYFoYkrxU0";

        private static Client _Client;

        private static int _NFTCount = 0; // Fetch Actual Count Later 


        static SupabaseClient()
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

        public static async void InsertHighScore(Result result, int points)
        {
            var newHighScore = new HighScore
            {
                Id = Guid.NewGuid().ToString(),
                Name = Preferences.Default.Get("username", ""),
                CreatedAt = DateTime.UtcNow.ToString(),
                Correct = result.TotalRight,
                Wrong = result.TotalWrong,
                Total = result.TotalAnswered,
                Points = points,
                Mode = Preferences.Default.Get("currency", "USD")
            };

            await _Client.From<HighScore>().Insert(newHighScore);
        }
        public static async Task<List<HighScore>> FetchTop100Scores()
        {
            var response = await _Client.From<HighScore>().Order("points", Postgrest.Constants.Ordering.Descending).Limit(100).Get();
            var scores = response.Models;
            return scores;
        }
    }
}
