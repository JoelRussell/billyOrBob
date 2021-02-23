using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace billyOrBob.Classifiers
{
    public class FrequencyClassifier : IClassifier {
        
        public Dictionary<string, string> config = new Dictionary<string, string>();
        public FrequencyClassifier() {

        }

        public string Classify(string inputText) {

            Dictionary<string,int> shakespeareWordStats = CorpusWordCount(config["ShakespeareCorpus"]);
            Dictionary<string,int> burnsWordStats = CorpusWordCount(config["BurnsCorpus"]);
            float shakespeareWordCountMultiplier = (float) burnsWordStats.Sum(x => x.Value) / (float) shakespeareWordStats.Sum(x => x.Value);
            int score = 0;

            inputText = cleanText(inputText);
            string[] inputWords = inputText.Split(' ');
            foreach (var word in inputWords)
            {
                Console.WriteLine("#### Word " + word);
                // Take the word count for this word in shakespeare, if it exists, else burns
                float shakespeareScore = shakespeareWordStats.ContainsKey(word)? shakespeareWordStats[word] : 0;
                float burnsScore = burnsWordStats.ContainsKey(word)? burnsWordStats[word] : 0;

                Console.WriteLine("    Shakespeare: " + shakespeareScore);
                Console.WriteLine("    Burns: " + burnsScore);

                // Multiply the shakespeare score accounting for the difference in corpus size
                shakespeareScore = shakespeareScore * shakespeareWordCountMultiplier;
                
                Console.WriteLine("    Adjusted Shakespeare: " + shakespeareScore);
                // Increments the score by 1 if Shakespeare used the word more
                // Decrements the score by 1 if Burns uses the word more
                // Doesn't change the score if they are equal
                score += shakespeareScore.CompareTo(burnsScore);
                
                Console.WriteLine("    Score is: " + score);

            }

            if (score > 0) {
                return "Billy";
            }
            else if (score < 0 ) {
                return "Bob";
            }
            else {
                return "No idea";
            }
        }

        public Dictionary<string, int> CorpusWordCount(string directory) {
            Dictionary<string, int> wordStats = new Dictionary<string, int>();
            string[] files = Directory.GetFiles(directory);
            foreach (var filename in files)
            {
                string text = File.ReadAllText(filename);
                text = cleanText(text);
                string[] words = text.Split(' ');
                foreach (var word in words)
                {
                    if (!wordStats.ContainsKey(word)) {
                        wordStats.Add(word, 1);
                    }
                    else {
                        wordStats[word] += 1;
                    }
                }
            }
            return wordStats;
        }
        public void SetConfig(string key, string value) {
            config.Add(key, value);
        }

        public string cleanText(string inputText) {
            // Characters to simply remove from the text
            string removeChars = "[-']";
            inputText = Regex.Replace(inputText, removeChars, String.Empty);

            // Characters to replace with spaces
            string spaceChars = "[^A-Za-z]";
            inputText = Regex.Replace(inputText, spaceChars, " ");
            inputText = Regex.Replace(inputText, "\\ {2,}", " ");
            return inputText.ToLower();
        }
    }

}