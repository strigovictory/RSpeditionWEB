namespace RSpeditionWEB.Components.Shared.Timers
{
    public class BlazorTimer : AuthenticationStateBaseClass
    {
        private System.Timers.Timer timer;

        public bool IsTimerEnabled => this.timer?.Enabled ?? false;
        public int Counter { get; private set; }

        public void StartTimer()
        {
            this.Counter = 1;
            timer = new System.Timers.Timer(1000);

            if (timer != null)
            {
                timer.Elapsed += CountDownTimer;
                timer.Enabled = true;
            }
        }

        private void CountDownTimer(Object source, ElapsedEventArgs e)
        {
            if (this.Counter < 3600)
                this.Counter += 1;
            else
                this.ResetTimer();

            InvokeAsync(StateHasChanged);
        }

        public void ResetTimer()
        {
            this.Counter = 1;
            if(timer != null)
                timer.Enabled = false;
        }
    }
}
