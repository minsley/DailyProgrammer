using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DailyProg
{
    class Challenge161E
    {
        public static void MainMethod()
        {
            Console.WriteLine("Playing C161E - Blackjack");

            int nPacks;
            int nPlayers;
            while (true)
            {
                try
                {
                    Console.Write("# packs of cards to use: ");
                    nPacks = int.Parse(Console.ReadLine(), NumberStyles.None);
                    if (nPacks == 0) throw new Exception();

                    Console.Write("# players: ");
                    nPlayers = int.Parse(Console.ReadLine(), NumberStyles.None);
                    if (nPlayers == 0) throw new Exception();
                    break;
                }
                catch
                {
                    Console.WriteLine("\nError: invalid input. Please try again...\n");
                }
            }

            var game = new Game(nPlayers, nPacks);

            var playing = true;
            while (playing)
            {
                game.DealHands();
                game.PlayHands();
                game.LogRound();
                game.DeclareWinners();

                Console.Write("Keep playing? (y/n): ");
                if (Console.ReadLine() == "n")
                {
                    game.ShowStats();
                    playing = false;
                }
            }

        }

        public class Game
        {
            private int nPlayers;
            private Deck deck;
            private Dictionary<string, Hand> hands;
            private Stats gameStats;

            public Game() : this(1, 10) { }

            public Game(int players, int packs)
            {
                gameStats = new Stats();
                nPlayers = players;

                Console.Write("Shuffling the deck...");
                deck = new Deck(packs);
                Console.WriteLine("Done.");

            }

            public void DealHands()
            {
                hands = new Dictionary<string, Hand> {{"Dealer", deck.DealHand()}};
                for (var i = 1; i <= nPlayers; i++)
                {
                    hands.Add("Player" + i, deck.DealHand());
                }
            }

            public void PlayHands()
            {
                for (var i = 1; i < hands.Count; i++)
                {
                    Console.WriteLine("\n** Player{0}'s turn! **\n", i);
                    GetPlayerMoves(hands["Player" + i]);
                }
                Console.WriteLine("\n** Dealer's turn! **\n");
                GetDealerMoves(hands["Dealer"]);
            }

            private void GetPlayerMoves(Hand hand)
            {
                var stay = false;
                while (!stay)
                {
                    var handValue = hand.GetValue();
                    Console.WriteLine("Your hand: {0}", hand);
                    Console.WriteLine("Hand value: {0}", handValue);

                    if (handValue == 21)
                    {
                        stay = true;
                    }
                    else if (handValue > 21)
                    {
                        stay = true;
                    }
                    else
                    {
                        Console.Write("Hit? (y/n): ");
                        if (Console.ReadLine() == "n") { stay = true; }
                        else { hand.Cards.Add(deck.Hit()); }
                    }
                }
            }

            private void GetDealerMoves(Hand hand)
            {
                var stay = false;
                while (!stay)
                {
                    var handValue = hand.GetValue();
                    if (handValue > 21)
                    {
                        Console.WriteLine("Dealer busts!");
                        stay = true;
                    }
                    else if (handValue > 16)
                    {
                        Console.WriteLine("Dealer has {0}... dealer stays.", handValue);
                        stay = true;
                    }
                    else
                    {
                        Console.WriteLine("Dealer has {0}... dealer hits!", handValue);
                        hand.Cards.Add(deck.Hit());
                    }
                }
            }

            public void DeclareWinners()
            {
                var dealerScore = hands["Dealer"].GetValue();
                foreach (var hand in hands.Where(x => x.Key.StartsWith("Player")))
                {
                    var playerScore = hand.Value.GetValue();

                    // evaluate winner, assuming dealer wins w/o blackjack 
                    var blackjack = false;
                    var dealerWins = true;

                    if (dealerScore == 21)
                    {
                        blackjack = true;
                    }
                    else if (dealerScore > 21)
                    {
                        if (playerScore == 21)
                        {
                            dealerWins = false;
                            blackjack = true;
                        }
                        else if (playerScore < 21)
                        {
                            dealerWins = false;
                        }
                    }
                    else
                    {
                        if (playerScore == 21)
                        {
                            dealerWins = false;
                            blackjack = true;
                        }
                        else if (playerScore < 21 && playerScore > dealerScore)
                        {
                            dealerWins = false;
                        }
                    }

                    if (!dealerWins)
                    {
                        Console.WriteLine("\n{0} beats Dealer! {1}", hand.Key, blackjack ? "Blackjack!" : "");
                    }
                    else
                    {
                        Console.WriteLine("\nDealer beats {0}! {1}", hand.Key, blackjack?"Blackjack!":"");
                    }
                }
            }

            public void LogRound()
            {
                gameStats.LogRound(hands.Values.ToArray());
            }

            public void ShowStats()
            {
                gameStats.ShowStats();
            }
        }

        public class Deck
        {
            private List<Card> cards;
            private Random shuffler = new Random();

            // default to 10 packs of cards
            public Deck() : this(10) { }

            public Deck(int packs)
            {
                cards = new List<Card>(52 * packs);

                var values = new[] { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
                var suits = new[] { '♠', '♥', '♦', '♣' };
                var names = new[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

                // build deck
                for (var i = 0; i < packs; i++)
                {
                    foreach (var suit in suits)
                    {
                        for (var j = 0; j < 13; j++)
                        {
                            cards.Add(new Card(values[j], suit, names[j]));
                        }
                    }
                }

                // shuffle
                for (var i = 0; i < cards.Count; i++)
                {
                    var j = shuffler.Next(0, cards.Count);
                    var temp = cards[i];
                    cards[i] = cards[j];
                    cards[j] = temp;
                }
            }

            public Hand DealHand()
            {
                return new Hand(new List<Card> { Hit(), Hit() });
            }

            public Card Hit()
            {
                var index = shuffler.Next(0, cards.Count);
                var card = cards[index];
                cards.RemoveAt(index);
                return card;
            }
        }

        public class Hand
        {
            public List<Card> Cards;

            public Hand(List<Card> cards)
            {
                Cards = cards;
            }

            public override string ToString()
            {
                var handString = new StringBuilder();
                foreach (Card card in Cards)
                {
                    handString.Append(card + " ");
                }
                return handString.ToString();
            }

            public int GetValue()
            {
                var handValue = 0;
                foreach (Card card in Cards)
                {
                    // if ace high blows the hand, make it ace low
                    if (card.Value == 11 && card.Value + handValue > 21) card.Value = 1;
                    // if this card blows the hand, but we have an ace high, make it ace low
                    else if (card.Value + handValue > 21 && Cards.Any(x => x.Value == 11))
                    {
                        Cards.Find(x => x.Value == 11).Value = 1;
                        handValue -= 10;
                    }
                    handValue += card.Value;
                }
                return handValue;
            }
        }

        public class Card
        {
            public int Value;
            public char Suit;
            public string Name;

            public Card(int value, char suit, string name)
            {
                Value = value;
                Suit = suit;
                Name = name;
            }

            public override string ToString()
            {
                return Name + Suit;
            }
        }

        public class Stats
        {
            private int handsPlayed;
            private int roundsPlayed;
            private int blackjacks;

            public void LogRound(Hand[] hands)
            {
                handsPlayed += hands.Count();
                blackjacks += hands.Count(x => x.GetValue() == 21);
                roundsPlayed++;
            }

            public void ShowStats()
            {
                Console.WriteLine("\n\t***\tGame statistics\t***\t\n");
                Console.WriteLine("Rounds played: {0}", roundsPlayed);
                Console.WriteLine("Hands played: {0}", handsPlayed);
                Console.WriteLine("Blackjacks: {0} ({1:P})", blackjacks, blackjacks/(float)handsPlayed);
            }
        }
    }
}
