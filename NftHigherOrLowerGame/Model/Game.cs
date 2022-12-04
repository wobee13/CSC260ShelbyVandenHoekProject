using NftHigherOrLowerGame.Components;
using NftHigherOrLowerGame.Model.DataBaseModels;
using System.Diagnostics;

namespace NftHigherOrLowerGame.Model
{
    public static class Game
    {
        public static GameTimer GameTime { get; set; }
        public static ScoreBoard Score { get; set; }
        public static NFT NFTDataLeft { get; set; }
        public static NFT NFTDataRight { get; set; }
        public static NFT NFTDataAlt { get; set; } // Used to PreFetch Next NFT Data
        public static NFTImage NFTImageLeft { get; set; }
        public static NFTImage NFTImageRight { get; set; }
        public enum Side { Left, Right }
        public enum AnswerOptions { Higher, Lower }

        private static async void StartTimer()
        {
            NFTDataLeft = await SupabaseNFT.FetchRandomNFT();
            NFTDataRight = await SupabaseNFT.FetchRandomNFT();
            NFTDataAlt = await SupabaseNFT.FetchRandomNFT();
            await Task.WhenAll( // This Runs Both at Same Time to Sync Fades
                NFTImageLeft.ChangeImage(NFTDataLeft),
                NFTImageRight.ChangeImage(NFTDataRight)
            );
            NFTImageLeft.ShowPrice();
            GameTime.Start();
        }

        private static void StopTimer() // Might Remove Functions
        {
            GameTime.Stop();
        }

        // Buttons
        public static void Higher()
        {
            StopTimer();
            CheckAnswer(AnswerOptions.Higher);
        }

        public static void Lower()
        {
            StopTimer();
            CheckAnswer(AnswerOptions.Lower);
        }

        public static void PauseMenu()
        {
            StartTimer(); // Temp For Testing
        }

        // Answer Checking
        private static void CheckAnswer(AnswerOptions answer)
        {
            if (NFTDataLeft.priceUSD == NFTDataRight.priceUSD)
            {
                // If Same Price then Higher and Lower both Correct
                CorrectAnswer();
            }
            else if (NFTDataLeft.priceUSD > NFTDataRight.priceUSD)
            {
                if (answer == AnswerOptions.Lower)
                {
                    CorrectAnswer();
                }
                else
                {
                    WrongAnswer();
                }
            }
            else if (NFTDataLeft.priceUSD < NFTDataRight.priceUSD)
            {
                if (answer == AnswerOptions.Higher)
                {
                    CorrectAnswer();
                }
                else
                {
                    WrongAnswer();
                }
            }
            else
            {
                // SomeThing Went Wrong if This Happens
                WrongAnswer();
            }

        }

        private static void CorrectAnswer()
        {

        }

        private static void WrongAnswer()
        {

        }


        // Functions Used to Get Refrence to Other Classes
        public static void RegisterTimer(GameTimer timer)
        {
            GameTime = timer;
        }

        public static void RegistererScoreBoard(ScoreBoard scoreBoard)
        {
            Score = scoreBoard;
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
