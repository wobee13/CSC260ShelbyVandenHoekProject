using NftHigherOrLowerGame.Components;
using NftHigherOrLowerGame.Model.DataBaseModels;
using NftHigherOrLowerGame.Model.Enums;

namespace NftHigherOrLowerGame.Model
{
    public static class Game
    {
        private static GameTimer GameTime { get; set; }
        public static ScoreBoard Score { get; set; }
        private static LivesBoard Lives { get; set; }
        public static int Points { get; set; }
        public static Result Results { get; set; } = new();
        private static bool TimerRunning { get; set; } = false;
        private static AnswerFeedBack AnswerDisplay { get; set; }
        private static NFT NFTDataLeft { get; set; }
        private static NFT NFTDataRight { get; set; }
        private static NFT NFTDataAlt { get; set; } // Used to PreFetch Next NFT Data
        private static NFTImage NFTImageLeft { get; set; }
        private static NFTImage NFTImageRight { get; set; }



        // Game State
        public static async void Start()
        {
            NFTDataLeft = await SupabaseClient.FetchNFT();
            NFTDataRight = await SupabaseClient.FetchNFT();
            NFTDataAlt = await SupabaseClient.FetchNFT();
            await Task.WhenAll( // This Runs Both at Same Time to Sync Fades
                NFTImageLeft.ChangeImage(NFTDataLeft),
                NFTImageRight.ChangeImage(NFTDataRight)
            );
            NFTImageLeft.ShowPrice();
            GameTime.Start();
            TimerRunning = true;
        }

        private static async void Continue()
        {
            GameTime.Reset();
            NFTDataLeft = NFTDataRight;
            NFTDataRight = NFTDataAlt;
            NFTDataAlt = await SupabaseClient.FetchNFT();
            await Task.Delay(4000);
            AnswerDisplay.HideFeedBack();
            await Task.WhenAll( // This Runs Both at Same Time to Sync Fades
                NFTImageLeft.ChangeImage(NFTDataLeft),
                NFTImageRight.ChangeImage(NFTDataRight)
            );
            NFTImageLeft.ShowPrice();
            GameTime.Start();
            TimerRunning = true;
        }

        private static void Stop()
        {
            TimerRunning = false;
            GameTime.Stop();
        }

        private static async void GameOver()
        {
            SupabaseClient.InsertHighScore(Results, Score.ScoreValue);
            await Shell.Current.GoToAsync("GameOver");
        }

        // Buttons
        public static void Higher()
        {
            if (TimerRunning)
            {
                Stop();
                CheckAnswer(AnswerOption.Higher);
            }
        }

        public static void Lower()
        {
            if (TimerRunning)
            {
                Stop();
                CheckAnswer(AnswerOption.Lower);
            }
        }

        public static void OutOfTime()
        {
            CheckAnswer(AnswerOption.OutOfTime);
            TimerRunning = false;
        }

        // Answer Checking
        private static void CheckAnswer(AnswerOption answer)
        {
            Results.TotalAnswered += 1;
            if (answer == AnswerOption.OutOfTime)
            {
                NoAnswer();
            }
            else
            {
                if (Preferences.Default.Get("currency", "USD") == "USD")
                {
                    if (NFTDataLeft.PriceUSD == NFTDataRight.PriceUSD)
                    {
                        // If Same Price then Higher and Lower both Correct
                        CorrectAnswer();
                    }
                    else if (NFTDataLeft.PriceUSD > NFTDataRight.PriceUSD)
                    {
                        if (answer == AnswerOption.Lower)
                        {
                            CorrectAnswer();
                        }
                        else
                        {
                            WrongAnswer();
                        }
                    }
                    else if (NFTDataLeft.PriceUSD < NFTDataRight.PriceUSD)
                    {
                        if (answer == AnswerOption.Higher)
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
                else // Etherium Prices
                {
                    if (NFTDataLeft.PriceETH == NFTDataRight.PriceETH)
                    {
                        // If Same Price then Higher and Lower both Correct
                        CorrectAnswer();
                    }
                    else if (NFTDataLeft.PriceETH > NFTDataRight.PriceETH)
                    {
                        if (answer == AnswerOption.Lower)
                        {
                            CorrectAnswer();
                        }
                        else
                        {
                            WrongAnswer();
                        }
                    }
                    else if (NFTDataLeft.PriceETH < NFTDataRight.PriceETH)
                    {
                        if (answer == AnswerOption.Higher)
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
            }
            if (Lives.LivesValue > 0)
            {
                Continue();
            }
            else
            {
                GameOver();
            }
        }

        private static void CorrectAnswer()
        {
            NFTImageRight.ShowPrice();
            Score.ScoreValue += Points;
            AnswerDisplay.Correct($"Earned {Points} points");
            Results.TotalCorrect += 1;
        }

        private static void WrongAnswer()
        {
            NFTImageRight.ShowPrice();
            Points = (GameTime.PointsPerSecond * (int)GameTime.StartTime) - Points;
            Score.ScoreValue -= Points;
            Results.TotalWrong += 1;
            Lives.LoseLife();
            AnswerDisplay.Wrong($"Lost 1 life and {Points} points");
        }

        private static void NoAnswer()
        {
            NFTImageRight.ShowPrice();
            Points = (GameTime.PointsPerSecond * (int)GameTime.StartTime) - Points;
            Score.ScoreValue -= Points;
            Results.TotalWrong += 1;
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
