using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace Kolko_i_krzyzyk
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Game2 game = new Game2();
        char mark = 'X';
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void ClickButton(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var x = Grid.GetRow(button);
            var y = Grid.GetColumn(button);

            if(game.AddToTab(x,y,mark))
            {
                button.Content = mark;
                if(mark =='X')
                {
                    mark = 'O';
                }
                else
                {
                    mark = 'X';
                }
            }

            if(game.CheckGameStatus())
            {
                var a = game.WinnerMark;

                var dialog = new MessageDialog($"Wygrał: {a}");
                await dialog.ShowAsync();
                ClearGame();
            }
        }

        private void ClearGame()
        {
            game = new Game2();

            B1.Content = " ";
            B2.Content = " ";
            B3.Content = " ";
            B4.Content = " ";
            B5.Content = " ";
            B6.Content = " ";
            B7.Content = " ";
            B8.Content = " ";
            B9.Content = " ";
        }

        public class Game2
        {
            private char[,] gameTab;
            public char WinnerMark;

            public Game2()
            {
                gameTab = new char[3, 3];
            }

            public bool AddToTab(int x, int y, char mark)
            {
                if (!Char.IsLetter(gameTab[x, y]))
                {
                    gameTab[x, y] = mark;
                    return true;
                }
                return false;
            }

            public bool CheckGameStatus()
            {
                if(CheckRows() || CheckCols()|| CheckCross()||CheckFull())
                {
                    return true;
                }
                return false;
            }

            private bool CheckRows()
            {
                char mark = 'X';
                for(int k = 0; k < 2; k++)
                {
                    for(int i =0;i<3;i++)
                    {
                        if(gameTab[i,0]==mark && gameTab[i,1]== mark && gameTab[i,2]==mark)
                        {
                            WinnerMark = mark;
                            return true;
                        }
                    }
                    mark = 'O';
                }
                return false;
            }

            private bool CheckCols()
            {
                char mark = 'X';
                for (int k = 0; k < 2; k++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (gameTab[0, i] == mark && gameTab[1, i] == mark && gameTab[2, i] == mark)
                        {
                            WinnerMark = mark;
                            return true;
                        }
                    }
                    mark = 'O';
                }
                return false;
            }

            private bool CheckCross()
            {
                char mark = 'X';
                for (int i = 0; i < 2; i++)
                {
                    if (gameTab[0, 0] == mark && gameTab[1, 1] == mark && gameTab[2, 2] == mark)
                    {
                        WinnerMark = mark;
                        return true;
                    }
                    if (gameTab[0, 2] == mark && gameTab[1, 1] == mark && gameTab[2, 0] == mark)
                    {
                        WinnerMark = mark;
                        return true;
                    }
                    mark = 'O';
                }
                return false;
            }

            private bool CheckFull()
            {
                foreach(char x in gameTab) //for automatyczny
                {
                    if(!Char.IsLetter(x))
                    {
                        return false;
                    }
                }
                WinnerMark = 'D';
                return true;
            }
        }
    }
}
