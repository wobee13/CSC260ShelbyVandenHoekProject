using NftHigherOrLowerGame.Model;

namespace NftHigherOrLowerGame.Components;

public partial class GameTimer : ContentView
{
    private const uint _StartTime = 10;
    private uint _TimerValue = _StartTime;
    private bool _TimerRunning = false;

    public uint TimeValue
    {
        get { return _TimerValue; }
        set
        {
            _TimerValue = value;
            Update();
        }
    }

    public GameTimer()
    {
        InitializeComponent();
        Game.RegisterTimer(this);
    }

    public void Update()
    {
        TimerLabel.Text = $"{TimeValue} Seconds";
    }

    public async void Start()
    {
        if (_TimerRunning == false)
        {
            _TimerRunning = true;
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            TimeValue = _StartTime;
            _ = TimerBar.ProgressTo(0, TimeValue * 1000, Easing.SinInOut); // Maybe Change Easing
            while (await timer.WaitForNextTickAsync())
            {
                TimeValue -= 1;
                if (TimeValue == 0)
                {
                    timer.Dispose();
                    _TimerRunning = false;
                }
            }
        }
    }
}