using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace billyOrBob.Corpus
{
    public class CorpusAdapter {
        public string DirectoryPath { get; }
        public string[] ExcludeFiles { get; set; }
        private Dictionary<string, int> WordStats;
        
        public CorpusAdapter(string directoryPath) {
            this.DirectoryPath = directoryPath;
            this.WordStats = new Dictionary<string, int>();


        }

        public void ProcessCorpus() {
            DirectoryInfo directoryInfo = new DirectoryInfo(DirectoryPath);
            if (!directoryInfo.Exists) {
                // TODO Handle directory error
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                // Check the file has not been excluded
                if (ExcludeFiles == null || !ExcludeFiles.Contains(file.Name)) {
                    // Process this file and add its stats to the corpus stats
                    addFile(file.FullName);
                }
            }
        }
        
        private void addFile(string path) {
            string text = File.ReadAllText(path);
            // Prepare text for processing
            text = TextUtilities.CleanText(text);
            string[] words = text.Split(' ');
            foreach (var word in words)
            {
                if (word.Length >= 1) {
                    if (!this.WordStats.ContainsKey(word)) {
                        this.WordStats.Add(word, 1);
                    }
                    else {
                        this.WordStats[word] += 1;
                    }
                }
            }
        }
        

        public int CountWordInCorpus(string word) {
            if (this.WordStats.ContainsKey(word)) {
                return this.WordStats[word];
            } else {
                return 0;
            }
        }

        public int TotalWordCountOfCorpus() {
            return this.WordStats.Sum(x => x.Value);
        }
    }
}