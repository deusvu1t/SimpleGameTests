using SimpleGameTests.Utils;

namespace SimpleGameTests.Core
{
    public class Player
    {
        private readonly GameAnalytics _analytics;
        public readonly Logger _logger;

        public int Coins { get; private set; }

        public Player(GameAnalytics analytics, Logger logger)
        {
            _analytics = analytics;
            _logger = logger;
        }

        public void AddCoins(int amount)
        {
            Coins += amount;
            _analytics.TrackEvent("coins_added");
            _logger.Log($"Coins added: {amount}");
        }

        public bool SpendCoins(int amount)
        {
            if (Coins >= amount)
            {
                Coins -= amount;

                _analytics.TrackEvent("purchase_success");
                _logger.Log($"Purchase success: {amount}");

                return true;
            }

            _analytics.TrackEvent("purchase_failed");
            _logger.Log($"Purchase failed: {amount}");

            return false;
        }
    }
}
