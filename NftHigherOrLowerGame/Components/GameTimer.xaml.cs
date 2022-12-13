using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class GameTimer : ContentView
{
    private const uint _StartTime = 20;
    private uint _TimerValue = _StartTime;
    private bool _TimerRunning = false;
    private PeriodicTimer _TimerInstance;

    public uint TimeValue
    {
        get { return _TimerValue; }
        set
        {
            _TimerValue = value;
            TimerLabel.Text = $"{TimeValue} Seconds {TimeValue * 50} Points Available";
        }
    }

    public GameTimer()
    {
        InitializeComponent();
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
            TimeValue = _StartTime;
            _ = TimerLabel.FadeTo(1, 500);
            while (await _TimerInstance.WaitForNextTickAsync())
            {
                TimeValue -= 1;
                _ = TimerBar.ProgressTo(((double)TimeValue / (double)_StartTime), 1000, Easing.SinInOut);
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
        Game.Points = (int)TimeValue * 50;
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