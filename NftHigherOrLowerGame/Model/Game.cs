using NftHigherOrLowerGame.Components;
using NftHigherOrLowerGame.Model.DataBaseModels;

namespace NftHigherOrLowerGame.Model
{
    public static class Game
    {
        private static GameTimer GameTime { get; set; }
        private static ScoreBoard Score { get; set; }
        private static LivesBoard Lives { get; set; }
        public static int Points { get; set; }
        private static AnswerFeedBack AnswerDisplay { get; set; }
        private static NFT NFTDataLeft { get; set; }
        private static NFT NFTDataRight { get; set; }
        private static NFT NFTDataAlt { get; set; } // Used to PreFetch Next NFT Data
        private static NFTImage NFTImageLeft { get; set; }
        private static NFTImage NFTImageRight { get; set; }
        public enum Side { Left, Right }
        public enum AnswerOptions { Higher, Lower, OutOfTime }

        // Game State
        public static async void Start()
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

        private static async void Continue()
        {
            GameTime.Reset();
            NFTDataLeft = NFTDataRight;
            NFTDataRight = NFTDataAlt;
            NFTDataAlt = await SupabaseNFT.FetchRandomNFT();
            await Task.Delay(4000);
            AnswerDisplay.HideFeedBack();
            await Task.WhenAll( // This Runs Both at Same Time to Sync Fades
                NFTImageLeft.ChangeImage(NFTDataLeft),
                NFTImageRight.ChangeImage(NFTDataRight)
            );
            NFTImageLeft.ShowPrice();
            GameTime.Start();
        }

        private static void Stop() // Might Remove Functions
        {
            GameTime.Stop();
        }

        public static void GameOver()
        {
            throw new NotImplementedException();
        }

        // Buttons
        public static void Higher()
        {
            Stop();
            CheckAnswer(AnswerOptions.Higher);
        }

        public static void Lower()
        {
            Stop();
            CheckAnswer(AnswerOptions.Lower);
        }

        public static void OutOfTime()
        {
            CheckAnswer(AnswerOptions.OutOfTime);
        }

        public static void PauseMenu()
        {
            Start(); // Temp For Testing
        }

        // Answer Checking
        private static void CheckAnswer(AnswerOptions answer)
        {
            if (answer == AnswerOptions.OutOfTime)
            {
                NoAnswer();
            }
            else
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
                    // Something went wrong if this happens
                    WrongAnswer();
                }
            }
            Continue();
        }

        private static void CorrectAnswer()
        {
            NFTImageRight.ShowPrice();
            Score.ScoreValue += Points;
            AnswerDisplay.Correct($"Earned {Points} points");
        }

        private static void WrongAnswer()
        {
            NFTImageRight.ShowPrice();
            Points = 1000 - Points;
            Score.ScoreValue -= Points;
            Lives.LoseLife();
            AnswerDisplay.Wrong($"Lost 1 life and {Points} points");
        }

        private static void NoAnswer()
        {
            NFTImageRight.ShowPrice();
            Points = 1000 - Points;
            Score.ScoreValue -= Points;
            Lives.LoseLife();
            AnswerDisplay.OutOfTime($"Lost 1 life and {Points} points");
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

        public static void RegistererLivesBoard(LivesBoard livesBoard)
        {
            Lives = livesBoard;
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

        public static void RegisterAnswerDisplay(AnswerFeedBack answer)
        {
            AnswerDisplay = answer;
        }


    }
}
