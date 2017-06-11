using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GuessMyCard
{
    public class CardModel
    {
        private const int cardWidth = 170;
        private const int cardHeight = 230;
        private const string clubs = "♣";
        private const string diamonds = "♦";
        private const string hearts = "♥";
        private const string spades = "♠";
        private const string ace = "A";
        private const string jack = "J";
        private const string queen = "Q";
        private const string king = "K";
        private const int centreNumberFontSize = 55;
        private const int centreFigureFontSize = 90;
        private const int sideValueFontSize = 20;
        private const int sideSuitFontSize = 30;
        private const string suitFontFamily = "Arial";
        private const string valueFontFamily = "Times New Roman";
        public ImageBrush back = new ImageBrush();

        public CardModel()
        {
            back.ImageSource = new BitmapImage(new Uri(@"backImage.png", UriKind.Relative));
        }

        public Canvas setDeckBack(Canvas cardBack)
        {
            cardBack.Background = back;
            cardBack.Width = cardWidth;
            cardBack.Height = cardHeight;
            return cardBack;
        }

        private TextBlock cardText(string content, Color textColor, double fontSize, string fontFamily, double left, double top)
        {
            TextBlock cardText = new TextBlock();
            cardText.FontSize = fontSize;
            cardText.FontFamily = new FontFamily(fontFamily);
            cardText.Text = content;
            cardText.Foreground = new SolidColorBrush(textColor);
            cardText.SetValue(Canvas.LeftProperty, left);
            cardText.SetValue(Canvas.TopProperty, top);
            return cardText;
        }

        private Canvas cardCanvas(ref Card card)
        {
            Canvas canvas = new Canvas();
            Color textColor = Colors.Black;
            string suitText = "";
            string valueText = "";
            switch (card.Suit)
            {
                case Suits.Clubs:
                    textColor = Colors.Black;
                    suitText = clubs;
                    break;
                case Suits.Diamonds:
                    textColor = Colors.Red;
                    suitText = diamonds;
                    break;
                case Suits.Hearts:
                    textColor = Colors.Red;
                    suitText = hearts;
                    break;
                case Suits.Spades:
                    textColor = Colors.Black;
                    suitText = spades;
                    break;
            }
            canvas.Width = cardWidth;
            canvas.Height = cardHeight;
            valueText = ((int)card.Value).ToString();
            canvas.Background = new SolidColorBrush(Colors.White);
            // *** Adding centre image -> set of suits for number card and big figure letter for figure card ***
            switch (card.Value)
            {
                case Values.Nine:
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 20, 20)); // Top Left
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 110, 20)); // Top Right
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 20, 65)); // Centre Left Top
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 20, 110)); // Centre Left Bottom
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 65, 90)); // Centre
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 110, 65)); // Centre Right Top
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 110, 110)); // Centre Right Bottom
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 20, 160)); // Bottom Left
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 110, 160)); // Bottom Right
                    break;
                case Values.Ten:
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 20, 20)); // Top Left
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 110, 20)); // Top Right
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 20, 65)); // Centre Left Top
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 20, 110)); // Centre Left Bottom
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 65, 55)); // Centre Top
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 65, 125)); // Centre Bottom
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 110, 65)); // Centre Right Top
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 110, 110)); // Centre Right Bottom
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 20, 160)); // Bottom Left
                    canvas.Children.Add(cardText(suitText, textColor, centreNumberFontSize, suitFontFamily, 110, 160)); // Bottom Right
                    break;
                case Values.Jack:
                    valueText = jack;
                    canvas.Children.Add(cardText(valueText, textColor, centreFigureFontSize, valueFontFamily, 65, 65)); // Centre
                    break;
                case Values.Queen:
                    valueText = queen;
                    canvas.Children.Add(cardText(valueText, textColor, centreFigureFontSize, valueFontFamily, 50, 65)); // Centre
                    break;
                case Values.King:
                    valueText = king;
                    canvas.Children.Add(cardText(valueText, textColor, centreFigureFontSize, valueFontFamily, 50, 65)); // Centre
                    break;
                case Values.Ace:
                    valueText = ace;
                    canvas.Children.Add(cardText(suitText, textColor, centreFigureFontSize, valueFontFamily, 60, 60)); // Centre
                    break;
            }
            // *** Adding top and bottom value  ***
            if ((valueText.Length == 1)) // To all cards apart from '10'
            {
                canvas.Children.Add(cardText(valueText, textColor, sideValueFontSize, valueFontFamily, 10, 4));
                canvas.Children.Add(cardText(valueText, textColor, sideValueFontSize, valueFontFamily, 148, 182));
            }
            else // To '10' card
            {
                canvas.Children.Add(cardText(valueText, textColor, sideValueFontSize, valueFontFamily, 5, 4));
                canvas.Children.Add(cardText(valueText, textColor, sideValueFontSize, valueFontFamily, 144, 182));
            }
            // *** Adding top and bottom suit ***
            canvas.Children.Add(cardText(suitText, textColor, sideSuitFontSize, suitFontFamily, 7, 16));
            canvas.Children.Add(cardText(suitText, textColor, sideSuitFontSize, suitFontFamily, 145, 195));
            return canvas;
        }

        public Canvas SpinCard(Card card, bool facing)
        {
            if (facing)
            {
                return cardCanvas(ref card);
            }
            else
            {
                Canvas returnCanvas = new Canvas();
                returnCanvas = setDeckBack(returnCanvas);
                return returnCanvas;
            }
        }
    }
}
