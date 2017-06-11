using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GuessMyCard
{
    public class Card
    {
        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
            Name = Value + " of " + Suit;
        }

        public Card()
        { }
            
        public Suits Suit;
        public Values Value;
        public string Name { get; }
    }
}
