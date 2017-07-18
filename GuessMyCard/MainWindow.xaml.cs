using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuessMyCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game theGame = new Game(1);
        public MainWindow()
        {
            InitializeComponent();
            WindowAdjust();
            PageSwitcher.Switch(this, new StartingPage());
        }

        private void menuNew_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Think of a card from Nine to Ace along with its suit", "Before going further...");
            this.menuSave.IsEnabled = true;
            Stage1 stage1 = new Stage1();
            PageSwitcher.Switch(this, stage1);
            theGame = new Game();
            theGame.StartStage(stage1, 4);
            UpdateStatusBar(theGame.Stage);
        }

        private void WindowAdjust()
        {
            this.Width = System.Windows.SystemParameters.WorkArea.Width;
            this.Height = System.Windows.SystemParameters.WorkArea.Height;
            this.Left = 0;
            this.Top = 0;
            this.WindowState = WindowState.Normal;
        }

        private async void UpdateStatusBar(int stageNumber)
        {
            stageBar.Text = "Stage " + stageNumber + " of 4";            
            pbStatus.Value = stageNumber * 30;
            Grid buttonGrid = new Grid();

            string oldGridName;
            string newGridName;
            if (stageNumber == 1)
            {
                oldGridName = "buttonsStage" + (stageNumber);
                newGridName = "buttonsStage" + (stageNumber);
            }
            else
            {
                oldGridName = "buttonsStage" + (stageNumber - 1);
                newGridName = "buttonsStage" + (stageNumber);
            }

            buttonGrid = (Grid)this.FindName(oldGridName);
            buttonGrid.Visibility = Visibility.Collapsed;

            if (stageNumber < 4)
            {


                barInstruction.Foreground = new SolidColorBrush(Colors.Red);
                barInstruction.Text = "Please choose the pile in which you see your card!";
                buttonGrid = (Grid)this.FindName(newGridName);
                await Task.Delay(5400);
                buttonGrid.Visibility = Visibility.Visible;
            }
            else
            {
                barInstruction.Foreground = new SolidColorBrush(Colors.MediumTurquoise);
                barInstruction.Text = "Is this Your card? :D";
            }

        }

        private void stage1buttton_Click(object sender, RoutedEventArgs e)
        {
            string buttonNumber = (sender as Button).Name.Substring(7, 1);
            theGame.PickedPile = Int32.Parse(buttonNumber);
            theGame.Stage = 2;
            Stage2 stage2 = new Stage2();
            PageSwitcher.Switch(this, stage2);
            theGame.StartStage(stage2, 3);
            UpdateStatusBar(theGame.Stage);
        }

        private void stage2buttton_Click(object sender, RoutedEventArgs e)
        {
            string buttonNumber = (sender as Button).Name.Substring(7, 1);
            theGame.PickedPile = Int32.Parse(buttonNumber);
            theGame.Stage = 3;
            Stage3 stage3 = new Stage3();
            PageSwitcher.Switch(this, stage3);
            theGame.StartStage(stage3, 2);
            UpdateStatusBar(theGame.Stage);
        }

        private void stage3buttton_Click(object sender, RoutedEventArgs e)
        {
            string buttonNumber = (sender as Button).Name.Substring(7, 1);
            theGame.PickedPile = Int32.Parse(buttonNumber);
            theGame.Stage = 4;
            Stage4 stage4 = new Stage4();
            PageSwitcher.Switch(this, stage4);
            theGame.StartStage(stage4, 1);
            UpdateStatusBar(theGame.Stage);
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            GameSaver.SaveGame(theGame);
        }

        private void menuLoad_Click(object sender, RoutedEventArgs e)
        {
            int currentStage = theGame.Stage;
            bool loaded = GameSaver.LoadGame(ref theGame);

            if(loaded)
            {
                try
                {
                    //theGame = new Game(loaded);
                    this.menuSave.IsEnabled = true;
                    string stageName = "GuessMyCard.Stage" + theGame.Stage;
                    Type elementType = Type.GetType(stageName);
                    object stageInstance = Activator.CreateInstance(elementType);
                    Page stage = (Page)stageInstance;
                    PageSwitcher.Switch(this, stage);
                    theGame.StartLoadedStage(stage, theGame.StagePiles);
                    UpdateLoadedStatusBar(currentStage, theGame.Stage);
                }
                catch
                {
                    MessageBox.Show("Dane w pliku były niepoprawne");
                }
            }
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void helpAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void helpView_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            help.Show();
        }

        private async void UpdateLoadedStatusBar(int stageNumber, int loadedStage)
        {
            stageBar.Text = "Stage " + loadedStage + " of 4";
            pbStatus.Value = loadedStage * 30;
            Grid buttonGrid = new Grid();

            string oldGridName = "buttonsStage" + (stageNumber);
            string newGridName = "buttonsStage" + (loadedStage);

            if (stageNumber > 0 && stageNumber < 4)
            {
                buttonGrid = (Grid)this.FindName(oldGridName);
                buttonGrid.Visibility = Visibility.Collapsed;
            }

            if (loadedStage < 4)
            {
                barInstruction.Foreground = new SolidColorBrush(Colors.Red);
                barInstruction.Text = "Please choose the pile in which you see your card!";
                buttonGrid = (Grid)this.FindName(newGridName);
                await Task.Delay(5400);
                buttonGrid.Visibility = Visibility.Visible;
            }
            else
            {
                barInstruction.Foreground = new SolidColorBrush(Colors.MediumTurquoise);
                barInstruction.Text = "Is this Your card? :D";
            }

        }
    }
}
