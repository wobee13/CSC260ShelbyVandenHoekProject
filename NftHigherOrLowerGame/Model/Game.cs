using NftHigherOrLowerGame.Components;
using NftHigherOrLowerGame.Model.DataBaseModels;
using System.Diagnostics;

namespace NftHigherOrLowerGame.Model
{
    public static class Game
    {
        public static GameTimer GameTime { get; set; }
        public static NFT NFTDataLeft { get; set; }
        public static NFT NFTDataRight { get; set; }
        public static NFT NFTDataAlt { get; set; } // Used to PreFetch Next NFT Data
        public static NFTImage NFTImageLeft { get; set; }
        public static NFTImage NFTImageRight { get; set; }
        public enum Side { Left, Right }

        public static async void StartTimer()
        {
            NFTDataLeft = await SupabaseNFT.FetchRandomNFT();
            NFTDataRight = await SupabaseNFT.FetchRandomNFT();
            NFTImageLeft.ChangeImage(NFTDataLeft);
            NFTImageRight.ChangeImage(NFTDataRight);
            GameTime.Start();
        }

        public static void StopTimer()
        {
            GameTime.Stop();
        }

        public static void Higher()
        {
            StartTimer();
            _ = SupabaseNFT.FetchRandomNFT();
            Debug.WriteLine("Higher");
        }

        public static void Lower()
        {
            //StopTimer();
            Debug.WriteLine("Lower");
        }

        // Functions Used to Get Refrence to Other Classes
        public static void RegisterTimer(GameTimer timer)
        {
            GameTime = timer;
        }

        public static void RegisterNFTImage(NFTImage nftimage, Side side)
        {
            switch (side)
            {
                case Side.Left:
                    NFTImageLeft = nftimage;
                    break;
                case Side.Right:
                    NFTImageRight = nftimage;
                    break;
            }
        }
    }
}
