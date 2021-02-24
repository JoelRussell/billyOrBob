using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.Extensions.Configuration;
using billyOrBob.Corpus;

namespace billyOrBob.Classifiers
{
    public class FrequencyClassifier : IClassifier {
        
        public Dictionary<string, string> config = new Dictionary<string, string>();
        public FrequencyClassifier() {

        }

        public string Classify(string inputText) {
            CorpusAdapter shakespeareWordStats = new CorpusAdapter(config["ShakespeareCorpus"]);
            CorpusAdapter burnsWordStats = new CorpusAdapter(config["BurnsCorpus"]);
            // Dictionary<string,int> shakespeareWordStats = CorpusWordCount(config["ShakespeareCorpus"]);
            // Dictionary<string,int> burnsWordStats = CorpusWordCount(config["BurnsCorpus"]);
            float shakespeareWordCountMultiplier = (float) burnsWordStats.TotalWordCountOfCorpus() / (float) shakespeareWordStats.TotalWordCountOfCorpus();
            int score = 0;
            inputText = TextUtilities.CleanText(inputText);
            string[] inputWords = inputText.Split(' ');
            foreach (var word in inputWords)
            {
                // Take the word count for this word in shakespeare, if it exists, else burns
                float shakespeareScore = shakespeareWordStats.CountWordInCorpus(word) * shakespeareWordCountMultiplier;
                float burnsScore = burnsWordStats.CountWordInCorpus(word);

                // Increments the score by 1 if Shakespeare used the word more
                // Decrements the score by 1 if Burns uses the word more
                // Doesn't change the score if they are equal
                score += shakespeareScore.CompareTo(burnsScore);

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

        
        public void SetConfig(string key, string value) {
            config.Add(key, value);
        }


    }

}