using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Bingo
{
    public sealed partial class MainPage : Page
    {
        public const int MIN = 1;
        public const int MAX = 60;
        public const string LETTERS = "BINGO";
        public List<int> numbers;

        private SortedList<int, string>[] buckets;

        public MainPage()
        {
            this.InitializeComponent();
            numbers = Enumerable.Range(MIN, MAX).ToList();
            buckets = new SortedList<int, string>[LETTERS.Length];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = new SortedList<int, string>();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            int index = r.Next(0, numbers.Count());
            int number = numbers[index];
            numbers.RemoveAt(index);
            char letter = SetBucket(number);
            txtCurrent.Text = letter + number.ToString();
            RefreshBuckets();
            if (numbers.Count == 0)
            {
                btnGo.IsEnabled = false;
                btnGo.Content = "Done!";
            }
        }

        private char SetBucket(int number)
        {
            int numbersPerColumn = (MAX - MIN + 1) /  LETTERS.Length;
            int index = (number - 1) / numbersPerColumn;
            char letter = LETTERS[index];
            buckets[index].Add(number, letter + number.ToString());
            return letter;
        }

        private void RefreshBuckets()
        {
            UIElementCollection textBlocks = stkLetters.Children;
            for (int i = 0; i < buckets.Count(); i++)
            {
                string result = "";
                foreach (var kvp in buckets[i])
                {
                    result += kvp.Value + "\r\n";
                }
                ((TextBlock)textBlocks[i]).Text = result;
            }
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
