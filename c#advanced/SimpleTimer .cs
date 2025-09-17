using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    public class SimpleTimer
    {private readonly int _intervalSeconds;
            private int _elapsedSeconds = 0;
            private System.Timers.Timer _timer;

            // Declare Events
            public event Action<int> Tick;         
            public event Action Completed;        

            public SimpleTimer(int intervalSeconds)
            {
                _intervalSeconds = intervalSeconds;
            }

            public void Start()
            {
                _timer = new System.Timers.Timer(1000); // كل ثانية
                _timer.Elapsed += (s, e) =>
                {
                    _elapsedSeconds++;
                    OnTick(_elapsedSeconds);

                    if (_elapsedSeconds >= _intervalSeconds)
                    {
                        _timer.Stop();
                        OnCompleted();
                    }
                };
                _timer.Start();
            }

            // Helper methods for raising events
            protected virtual void OnTick(int seconds)
            {
                Tick?.Invoke(seconds);
            }

            protected virtual void OnCompleted()
            {
                Completed?.Invoke();
            }
        }


    public class TimerMonitor
    {
        public void Subscribe(SimpleTimer timer)
        {
            timer.Tick += OnTick;
            timer.Completed += OnCompleted;
        }

        private void OnTick(int seconds)
        {
            Console.WriteLine($"Tick: {seconds} Second Passed");
        }

        private void OnCompleted()
        {
            Console.WriteLine("Completed!");
        }
    }
}
