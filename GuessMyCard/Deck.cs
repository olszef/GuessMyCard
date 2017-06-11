using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Timers;
using System.Windows;


namespace GuessMyCard
{
    public class Deck
    {
        private List<Card> cards;
        private List<Card>[] pile;
        public Card[][] cardArray; //for the serialization purposes
        private CardModel cardModel = new CardModel();

        public Deck()
        {
            cards = new List<Card>();
            for (int suit = 0; suit <= 3; suit++)
            {
                for (int value = 9; value <= 14; value++)
                    cards.Add(new Card((Suits)suit, (Values)value));
            }
        }

        public Deck(Deck loadDeck)
        {
            cards = new List<Card>();
            this.cardArray = loadDeck.cardArray;
            pile = new List<Card>[loadDeck.cardArray.Length];
            try
            {
                for (int i = 0; i < loadDeck.cardArray.Length; i++)
                {
                    pile[i] = loadDeck.cardArray[i].OfType<Card>().ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public int Count()
        {

                return cards.Count;
        }

        public int CountPile()
        {

            return pile.Length;
        }

        public void Deal (int nrOfLists)
        {
            pile = new List<Card>[nrOfLists];
            for (int listTableIndex = 0; listTableIndex < nrOfLists; listTableIndex++)
            {
                pile[listTableIndex] = new List<Card>();
            }
            while (Count() > 0)
            {
                for (int pileNr = 0; pileNr < nrOfLists; pileNr++)
                {
                    pile[pileNr].Add(cards[Count() - 1]);
                    cards.RemoveAt(Count() - 1);
                }
            }
            try
            {
                cardArray = Array.ConvertAll(pile, x => x.ToArray()); //for serialization purposes only

            }
            catch
            {
                MessageBox.Show("Błąd podczas konwersji danych. Gra nie będzie mogłą być zapisana.", "Uwaga...");
            }
        }

        public void Shuffle()
        {
            Random random = new Random();
            List<Card> NewCards = new List<Card>();
            while (cards.Count > 0)
            {
                int CardToMove = random.Next(cards.Count);
                NewCards.Add(cards[CardToMove]);
                cards.RemoveAt(CardToMove);
            }
            cards = NewCards;
        }

        public void CollectCards(int pickedPile, int pilesOnTable)
        {
            for (int i = 0; i < pilesOnTable; i++)
            {
                if (i != pickedPile - 1)
                {
                    cards.AddRange(pile[i]);
                }
            }
            cards.AddRange(pile[pickedPile - 1]);
            pile = new List<Card>[0];
        }

        public List<string> GetCardNames(int pileIndex)
        {
            List<string> pileCardList = new List<string>();
            foreach (Card card in pile[pileIndex])
            {
                pileCardList.Add(card.Name.ToString());
            }
            return pileCardList;
        }

        private async void showBackFront(Page stagePage, int pileNumber, bool facing)
        {
            await Task.Delay(500);
            DockPanel dock = new DockPanel();
            int col;
            for (int row = 0; row < pileNumber; row++)
             {
                col = 1;
                 foreach (Card card in pile[row])
                 {
                    if (facing)
                        await Task.Delay(100);
                    else
                        await Task.Delay(50);

                    string dockName = "dock" + row + col;
                    dock = (DockPanel)stagePage.FindName(dockName);
                    dock.Children.Clear();
                    dock.Children.Add(cardModel.SpinCard(pile[row][col-1], facing));
                    col++;
                }
             }
        }

        public async void ShowCard(Page stagePage, int pileNumber)
        {
            showBackFront(stagePage, pileNumber, false);
            await Task.Delay(2000);
            showBackFront(stagePage, pileNumber, true);
        }

        public async void ShowGuessedCard(Page stagePage, int pickedPile)
        {
            Stage4 stage4 = stagePage as Stage4;

            await Task.Delay(500);
            stage4.dock02.Children.Add(cardModel.SpinCard(pile[pickedPile - 1][3], false));
            await Task.Delay(500);

            for (int i = 3; i > 0; i--)
            {                
                stage4.textCountdown.Text = i.ToString();
                await Task.Delay(500);
                stage4.textCountdown.Text = "";
            }

            await Task.Delay(100);
            stage4.dock02.Children.Clear();
            stage4.dock02.Children.Add(cardModel.SpinCard(pile[pickedPile - 1][3], true));
        }
    }
}
