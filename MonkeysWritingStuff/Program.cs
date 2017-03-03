using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeysWritingStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            //a decent object model would make this easier

            string firstLine = "ACT I SCENE I Elsinore A platform before the castle at his post Enter to him Whos there Nay answer me stand and unfold yourself Long live the king He You come most carefully upon your hour Tis now struck twelve get thee to bed For this relief much thanks tis bitter cold And I am sick at heart Have you had quiet guard Not a mouse stirring".ToLower();
            //string firstLine = "Elsinore A platform before the castle".ToLower();
            //string firstLine = "ACT I SCENE I".ToLower();
            string[] letters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            Random rnd = new Random();

            string[] firstLineByWord = firstLine.Split(' ');
            double totalAttempts = 0;
            int RepeatCount = 500;
            double averageTotalAttemptsPerLine = 0;
            double totalSeconds = 0;

            //loop to increase estimation accuracy
            for (int j = 0; j < RepeatCount; j++)
            {
                DateTime start = DateTime.Now;

                //each word in the line
                foreach (string s in firstLineByWord)
                {
                    char[] word = s.ToCharArray();
                    double attemptsPerWord = 0;
                    int positionInWord = 0;
                    string GuessWord = "";
                    Console.WriteLine("Current word length: " + word.Length);

                    //each letter in each word in the line
                    do
                    {
                        attemptsPerWord++;

                        var letter = letters[rnd.Next(0, letters.Length)];
                        Console.WriteLine("Guess: " + letter + " Actual: " + word[positionInWord].ToString());
                        if (letter == word[positionInWord].ToString())
                        {
                            Console.WriteLine("-----------------------------------------");
                            Console.WriteLine(GuessWord);
                            Console.WriteLine("-----------------------------------------");
                            GuessWord = GuessWord + letter;
                            positionInWord++;
                        }
                        else
                        {
                            //positionInWord = 0;
                            //GuessWord = "";
                        }
                    } while (GuessWord != s);
                    //a word has been guessed
                    totalAttempts += attemptsPerWord;

                    Console.WriteLine(GuessWord);
                    Console.WriteLine("Attempts to correctly guess:  " + attemptsPerWord);
                    averageTotalAttemptsPerLine += (attemptsPerWord / word.Length);
                    //Console.ReadLine();

                }
                DateTime end = DateTime.Now;
                totalSeconds += (end - start).TotalSeconds;
            }

            //this math probably isn't correct but it's not really important
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Repeated " + RepeatCount + " times.");
            Console.WriteLine("Total Attempts: " + totalAttempts);
            Console.WriteLine("At 10 attempts per Second it would take a monkey " + (totalAttempts / 10) / RepeatCount + " seconds to write this line.");
            Console.WriteLine("Average Attempts per Loop: " + averageTotalAttemptsPerLine / RepeatCount);
            Console.WriteLine("Average Attempts per Word: " + (averageTotalAttemptsPerLine / RepeatCount) / firstLineByWord.Length);
            Console.WriteLine("Avg Seconds per Loop: " + totalSeconds / RepeatCount);
            Console.WriteLine("-----------------------------------------");
            CountOccurenceOfLetterInSentence(firstLine);
            Console.ReadLine();

        }

        //this shit is to make Ben happy with his stupid probabilities
        public static void CountOccurenceOfLetterInSentence(string inputLine)
        {
            string[] words = inputLine.Split(' ');
            List<Occurence> letters = new List<Occurence>();

            foreach (var w in words)
            {
                char[] ls = w.ToCharArray();
                foreach (char c in ls)
                {
                    Occurence o = new Occurence(c);
                    if (letters.Contains(o))
                        letters.Where(l => l.letter == o.letter).First().IncrementCount();
                    else
                        letters.Add(o);
                }
            }

            foreach (Occurence o in letters.OrderBy(le => le.count))
                Console.WriteLine(o.PrintContents());
            Console.WriteLine("The line contained: " + inputLine.Length + " letters.");
        }

        public class Occurence : IEquatable<Occurence>
        {
            public char letter { get; }
            public int count { get; set; }

            public Occurence(char _letter)
            {
                letter = _letter;
                count = 1;
            }

            public void IncrementCount()
            {
                this.count += 1;
            }

            bool IEquatable<Occurence>.Equals(Occurence other)
            {
                if (this.letter == other.letter)
                    return true;
                else
                    return false;
            }

            public string PrintContents()
            {
                return "The letter: " + this.letter + " occurs " + this.count.ToString() + " times in the phrase.";
            }
        }
    }
}
