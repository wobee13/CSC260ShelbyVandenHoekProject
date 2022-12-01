using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class GameTimer : ContentView
{
    private const uint _StartTime = 10;
    private uint _TimerValue = _StartTime;
    private bool _TimerRunning = false;
    private PeriodicTimer _TimerInstance;

    public uint TimeValue
    {
        get { return _TimerValue; }
        set
        {
            _TimerValue = value;
            TimerLabel.Text = $"{TimeValue} Seconds";
        }
    }

    public GameTimer()
    {
        InitializeComponent();
        Game.RegisterTimer(this);
    }

    public async void Start()
    {
        if (_TimerRunning == false)
        {
            _TimerRunning = true;
            _TimerInstance = new PeriodicTimer(TimeSpan.FromSeconds(1));
            TimeValue = _StartTime;
            TimerLabel.IsVisible = true;
            TimerBar.IsVisible = true;
            while (await _TimerInstance.WaitForNextTickAsync())
            {
                TimeValue -= 1;
                _ = TimerBar.ProgressTo(((double)TimeValue / (double)_StartTime), 1000, Easing.SinInOut);
                if (TimeValue == 0)
                {
                    _TimerInstance.Dispose();
                    _TimerRunning = false;
                    _TimerInstance = null;
                    break;
                }
            }
        }
    }

    public void Stop()
    {
        if (_TimerInstance != null)
        {
            TimeValue = 1;
            TimerLabel.IsVisible = false;
            TimerBar.IsVisible = false;
            TimerBar.Progress = 1;
        }
    }
}