using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Win32;


namespace GuessMyCard
{
    public class Game
    {
        public int Stage { get; set; }
        public int PickedPile { get; set; }
        public int StagePiles { get; set; }
        public Deck deck;

        public Game()
        {
            Stage = 1;
            deck = new Deck();
            deck.Shuffle();
        }

        public Game(Game loadGame)
        {
            this.Stage = loadGame.Stage;
            this.PickedPile = loadGame.PickedPile;
            this.StagePiles = loadGame.StagePiles;
            this.deck = new Deck(loadGame.deck);
        }

        public Game(int startingStage)
        {
            Stage = startingStage;
        }

        public void StartStage(Page stagePage, int stagePiles)
        {
            StagePiles = stagePiles;
            if (stagePiles < 4 && stagePiles > 1)
                deck.CollectCards(PickedPile, stagePiles + 1);

            if (stagePiles == 1)
            {
                deck.ShowGuessedCard(stagePage, PickedPile);
            }
            else
            {

                deck.Deal(stagePiles);
                deck.ShowCard(stagePage, stagePiles);
            }     
        }

        public void StartLoadedStage(Page stagePage, int stagePiles)
        {
            if (stagePiles == 1)
            {
                deck.ShowGuessedCard(stagePage, PickedPile);
            }
            else
            {
                deck.ShowCard(stagePage, stagePiles);
            }
        }
    }
}
