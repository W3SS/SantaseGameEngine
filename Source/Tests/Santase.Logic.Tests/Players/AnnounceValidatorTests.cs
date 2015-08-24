﻿namespace Santase.Logic.Tests.Players
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Santase.Logic.Cards;
    using Santase.Logic.Players;

    [TestFixture]
    public class AnnounceValidatorTests
    {
        private readonly IEnumerable<Card> playerCards = new List<Card>
                                                             {
                                                                 new Card(CardSuit.Club, CardType.Queen),
                                                                 new Card(CardSuit.Club, CardType.King),
                                                                 new Card(CardSuit.Diamond, CardType.Queen),
                                                                 new Card(CardSuit.Diamond, CardType.King),
                                                                 new Card(CardSuit.Heart, CardType.Queen),
                                                                 new Card(CardSuit.Heart, CardType.King)
                                                             };

        [TestCase(CardType.Nine)]
        [TestCase(CardType.Ten)]
        [TestCase(CardType.Jack)]
        [TestCase(CardType.Ace)]
        public void GetPossibleAnnounceShouldReturnNoAnnounceWhenNoKingOrQueenIsPlayed(CardType cardType)
        {
            IAnnounceValidator validator = new AnnounceValidator();
            var announce = validator.GetPossibleAnnounce(
                this.playerCards,
                new Card(CardSuit.Club, cardType),
                new Card(CardSuit.Club, CardType.Ace));
            Assert.AreEqual(Announce.None, announce);
        }

        [Test]
        public void GetPossibleAnnounceShouldReturnNoAnnounceWhenQueenIsPlayedButTheRespectiveKingIsMissing()
        {
            IAnnounceValidator validator = new AnnounceValidator();
            var announce = validator.GetPossibleAnnounce(
                this.playerCards,
                new Card(CardSuit.Spade, CardType.Queen),
                new Card(CardSuit.Heart, CardType.Ace));
            Assert.AreEqual(Announce.None, announce);
        }

        [Test]
        public void GetPossibleAnnounceShouldReturnNoAnnounceWhenKingIsPlayedButTheRespectiveQueenIsMissing()
        {
            IAnnounceValidator validator = new AnnounceValidator();
            var announce = validator.GetPossibleAnnounce(
                this.playerCards,
                new Card(CardSuit.Spade, CardType.King),
                new Card(CardSuit.Heart, CardType.Ace));
            Assert.AreEqual(Announce.None, announce);
        }

        [Test]
        public void GetPossibleAnnounceShouldReturnTwentyWhenQueenIsPlayedTheKingIsPresentAndTheTrumpIsDifferentSuit()
        {
            IAnnounceValidator validator = new AnnounceValidator();
            var announce = validator.GetPossibleAnnounce(
                this.playerCards,
                new Card(CardSuit.Club, CardType.Queen),
                new Card(CardSuit.Heart, CardType.Ace));
            Assert.AreEqual(Announce.Twenty, announce);
        }

        [Test]
        public void GetPossibleAnnounceShouldReturnTwentyWhenKingIsPlayedTheQueenIsPresentAndTheTrumpIsDifferentSuit()
        {
            IAnnounceValidator validator = new AnnounceValidator();
            var announce = validator.GetPossibleAnnounce(
                this.playerCards,
                new Card(CardSuit.Diamond, CardType.King),
                new Card(CardSuit.Heart, CardType.Ace));
            Assert.AreEqual(Announce.Twenty, announce);
        }

        [Test]
        public void GetPossibleAnnounceShouldReturnFourtyWhenQueenIsPlayedTheKingIsPresentAndTheTrumpIsTheSameSuit()
        {
            IAnnounceValidator validator = new AnnounceValidator();
            var announce = validator.GetPossibleAnnounce(
                this.playerCards,
                new Card(CardSuit.Diamond, CardType.Queen),
                new Card(CardSuit.Diamond, CardType.Ace));
            Assert.AreEqual(Announce.Fourty, announce);
        }

        [Test]
        public void GetPossibleAnnounceShouldReturnFourtyWhenKingIsPlayedTheQueenIsPresentAndTheTrumpIsTheSameSuit()
        {
            IAnnounceValidator validator = new AnnounceValidator();
            var announce = validator.GetPossibleAnnounce(
                this.playerCards,
                new Card(CardSuit.Heart, CardType.King),
                new Card(CardSuit.Heart, CardType.Nine));
            Assert.AreEqual(Announce.Fourty, announce);
        }
    }
}