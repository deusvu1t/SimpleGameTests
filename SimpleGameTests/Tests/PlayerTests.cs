using Allure.NUnit;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;
using NUnit.Framework;
using SimpleGameTests.Core;
using SimpleGameTests.Utils;

namespace SimpleGameTests.Tests
{
    [AllureNUnit]
    [AllureSuite("Player")]
    public class PlayerTests
    {
        private GameAnalytics _analytics;
        private Logger _logger;
        private Player _player;

        [SetUp]
        public void Setup()
        {
            _analytics = new GameAnalytics();
            _logger = new Logger();
            _player = new Player(_analytics, _logger);
        }

        [AllureStep("Добавляем {amount} монет")]
        public void AddCoinsStep(int amount)
        {
            _player.AddCoins(amount);
        }

        [AllureStep("Тратим {amount} монет")]
        public bool SpendCoinsStep(int amount)
        {
            return _player.SpendCoins(amount);
        }

        [Test]
        [AllureSubSuite("Coins")]
        [AllureSeverity(SeverityLevel.normal)]
        public void AddCoins_ShouldIncreaseCoins()
        {
            AddCoinsStep(100);

            Assert.That(_player.Coins, Is.EqualTo(100));
        }

        [Test]
        [AllureSubSuite("Coins")]
        [AllureSeverity(SeverityLevel.normal)]
        public void SpendCoins_ShouldDecreaseBalance_WhenEnoughCoins()
        {
            AddCoinsStep(100);
            var result = SpendCoinsStep(50);

            Assert.That(result, Is.True);
            Assert.That(_player.Coins, Is.EqualTo(50));
        }

        [Test]
        [AllureSubSuite("Coins")]
        [AllureSeverity(SeverityLevel.critical)]
        public void SpendCoins_ShouldFail_WhenNotEnoughCoins()
        {
            AddCoinsStep(30);
            var result = SpendCoinsStep(50);

            Assert.That(result, Is.False);
            Assert.That(_player.Coins, Is.EqualTo(30));
        }

        [Test]
        [AllureSubSuite("Analytics")]
        [AllureSeverity(SeverityLevel.critical)]
        public void SpendCoins_ShouldTrackSuccessEvent()
        {
            AddCoinsStep(100);
            SpendCoinsStep(50);

            Assert.That(_analytics.Events.Contains("purchase_success"), Is.True);
        }

        [Test]
        [AllureSubSuite("Logs")]
        [AllureSeverity(SeverityLevel.normal)]
        public void AddCoins_ShouldWriteLog()
        {
            AddCoinsStep(100);

            Assert.That(_logger.Logs.Contains("Coins added: 100"), Is.True);
        }

        [Test]
        [AllureSubSuite("Logs")]
        [AllureSeverity(SeverityLevel.normal)]
        public void SpendCoins_ShouldWriteFailureLog()
        {
            SpendCoinsStep(50);

            Assert.That(_logger.Logs.Contains("Purchase failed: 50"), Is.True);
        }
    }
}
