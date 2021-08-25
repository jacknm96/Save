using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            char command;
            int money = 100;                // amount of money the player starts with
            Card[] cards = new Card[10];    // array to store the cards the player is dealt. Reused for each hand
            int cardCount = 0;              // number of cards the player has been dealt

            // game loop. To exit, the player can choose to quit at the end of each hand
            while (true)
            {
                // reset the number of cards in the player's hand
                cardCount = 0;
                int bet = 0;
                Console.WriteLine("How much would you like to bet this hand? ");
                                
                bool success = int.TryParse(Console.ReadLine(), out bet);
                if(success == false)
                {
                    Console.WriteLine("Illegal input.");
                    continue;
                }

                // deal the first card and increment the card count
                cards[cardCount++] = DealCard();
                cards[cardCount++] = DealCard();
                // print all cards in the player's hand to the console
                PrintCards(cards, cardCount);
                // calculate the sum of all cards in the player's hand
                int total = CalculateTotal(cards, cardCount);

                // player can keep drawing cards until total is over 21
                while (total < 21)
                {
                    Console.WriteLine("(H)it or (S)tand? ");
                    success = char.TryParse(Console.ReadLine(), out command);                                        
                    if (success == false)
                    {
                        Console.WriteLine("Illegal input.");  
                        continue;
                    }

                    // if player wants a new card, deal card
                    if (command == 'h' || command == 'H')
                    {
                        // draw a new card, output to console, calculate total
                        cards[cardCount++] = DealCard();
                        PrintCards(cards, cardCount);
                        total = CalculateTotal(cards, cardCount);
                    }
                    else
                    {
                        // any input except 'hit' will be interpreted as 'stand'
                        break;
                    }
                }


                // this is a super simple blackjack program, so just simulate the dealer's hand
                // this will just generate a believable random number for the dealer
                int dealer = 10 + (random.Next() % 15);

                // check the dealer's and player's totals against each other to see who won
                if (total > 21)
                {
                    Console.WriteLine("You bust, dealer wins.");
                    money -= bet;
                }
                else if (dealer > 21)
                {
                    Console.WriteLine("Dealer busts, you win.");
                    money += bet;
                }
                else if (dealer > total)
                {
                    Console.WriteLine("Dealer wins.");
                    money -= bet;
                }
                else
                {
                    Console.WriteLine("You win.");
                    money += bet;
                }

                Console.WriteLine("You have {0} in the bank.", money);
                Console.WriteLine("Play again? (Y/N)");
                //char command;
                success = char.TryParse(Console.ReadLine(), out command);
                if (!(command == 'y' || command == 'Y'))
                    break;
            }
        }

        // Function to deal a new random card. 
        // A random value from 1 to 52 is generated, then we use math to 
        // calculate the suit and value of the card
        static Card DealCard()
        {            
            int cardIndex = random.Next() % 52;        // card index, a value from 1 to 52 inclusive
            int suit = cardIndex % 4;
            int value = (cardIndex % 13) + 1;         // the value of the card, from 1 to 13

            return new Card( (Card.Suit)suit, value );
        }
        
        // Calculates the sum of the value of the cards in the array
        // The first ace is read as 11, and any following ace has a value of 1
        // Other face cards have a value of 10
        // If there is an ace in the cards and the total exceeds 21, then we recount
        // the whole array again but this time all ace cards get a value of 1
        static int CalculateTotal(Card[] cardArray, int size)
        {
            bool hasAce = false;
            bool isFirstTime = true;
            int total = 0;

            for (int i = 0; i < size; i++)
            {
                if (cardArray[i].value == 1)
                {
                    if (hasAce == true)
                        total += 1;
                    else
                        total += 11;
                    hasAce = true;
                }
                else if (cardArray[i].value < 10)
                {
                    total += cardArray[i].value;
                }
                else
                {
                    total += 10;
                }

                if (total > 21 && hasAce && isFirstTime)
                {
                    // if its the first time we've counted, we have an ace in the deck, and
                    // the total value is over 21, then count the whole array again (this time
                    // all aces will have a score of 1 so the total may not go over 21)
                    i = -1;
                    total = 0;
                    isFirstTime = false;
                }
            }
            return total;
        }

        // This function will step through the cards in the input card array
        // and draw each card to the console.
        // We call the 'calculateTotal' function to get the sum of all cards in the array
        static void PrintCards(Card[] cardArray, int size)
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write(cardArray[i].Print());  
            }
            Console.WriteLine();

            int total = CalculateTotal(cardArray, size);

            Console.WriteLine("Current total: {0}", total);
        }

    }
}
