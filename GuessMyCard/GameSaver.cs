using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Runtime.Serialization;

namespace GuessMyCard
{
    static class GameSaver
    {
        static public void SaveGame(Game game)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.FileName = "GuessMyCard";
            saveFileDialog.DefaultExt = ".xml";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    XmlSerializer serializer = new XmlSerializer(game.GetType(), new Type[] { typeof(Deck) });
                    using (TextWriter stream = new StreamWriter(saveFileDialog.FileName))
                    {
                        serializer.Serialize(stream, game);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nie udało się zapisać gry", "Błąd podczas zapisu do pliku...");
            }

        }

        static public bool LoadGame(ref Game loadGame)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "XML file (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.FileName = "GuessMyCard.xml";
                if (openFileDialog.ShowDialog() == true)
                {
                try
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(Game), new Type[] { typeof(Deck) });
                    using (TextReader stream = new StreamReader(openFileDialog.FileName))
                    {
                        object readFile = deserializer.Deserialize(stream);
                        loadGame = new Game((Game)readFile);
                        
                    }
                    return true;
                }
                catch
                {
                    MessageBox.Show("Nie udało się odczytać zapisanej gry.", "Błąd podczas odczytu pliku...");
                    return false;
                }
            }
                else
                {
                    return false;
                }
        }

    }
}
