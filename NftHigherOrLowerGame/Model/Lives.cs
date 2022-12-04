namespace NftHigherOrLowerGame.Model
{
    class Lives
    {
        public int Total { get; set; }
        public Lives() { }

        public void LoseLife()
        {
            Total -= 1;
        }

        public void GainLife()
        {
            Total += 1;
        }
    }
}
