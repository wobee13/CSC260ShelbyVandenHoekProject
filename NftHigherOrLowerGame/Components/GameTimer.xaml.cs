using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class GameTimer : ContentView
{
    public uint StartTime { get; set; }
    private uint _TimerValue;
    public int PointsPerSecond { get; set; }
    private bool _TimerRunning = false;
    private PeriodicTimer _TimerInstance;

    public uint TimeValue
    {
        get { return _TimerValue; }
        set
        {
            _TimerValue = value;
            TimerLabel.Text = $"{TimeValue} Seconds {TimeValue * PointsPerSecond} Points Available";
        }
    }

    public GameTimer()
    {
        InitializeComponent();
        if (Preferences.Default.Get("difficulty", "Easy") == "Easy")
        {
            StartTime = 20;
            PointsPerSecond = 50;
        }
        else if (Preferences.Default.Get("difficulty", "Normal") == "Normal")
        {
            StartTime = 10;
            PointsPerSecond = 200;
        }
        else
        {
            StartTime = 5;
            PointsPerSecond = 500;
        }
        Game.RegisterTimer(this);
    }

    public async void Reset()
    {
        await Task.WhenAll(
            TimerBar.ProgressTo(1, 500, Easing.SinInOut),
            TimerLabel.FadeTo(0, 500)
        );
    }

    public async void Start()
    {
        if (_TimerRunning == false)
        {
            _TimerRunning = true;
            _TimerInstance = new PeriodicTimer(TimeSpan.FromSeconds(1));
            TimeValue = StartTime;
            _ = TimerLabel.FadeTo(1, 500);
            while (await _TimerInstance.WaitForNextTickAsync())
            {
                TimeValue -= 1;
                _ = TimerBar.ProgressTo(((double)TimeValue / (double)StartTime), 1000, Easing.SinInOut);
                if (TimeValue == 0)
                {
                    Stop();
                    Game.OutOfTime();
                    break;
                }
            }
        }
    }

    public void Stop()
    {
        Game.Points = (int)(TimeValue) * PointsPerSecond;
        if (_TimerInstance != null)
        {
            _TimerInstance.Dispose();
            _TimerRunning = false;
            _TimerInstance = null;
            //TimerLabel.IsVisible = false; // Switch to Fade
            //TimerBar.IsVisible = false;
            //TimerBar.Progress = 1;
        }
    }
}