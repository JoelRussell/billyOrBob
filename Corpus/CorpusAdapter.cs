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
        public string[] CorpusFiles { get; }
        
        private Dictionary<string, int> WordStats;
        
        public CorpusAdapter(string directoryPath) {
            this.DirectoryPath = directoryPath;
            this.WordStats = new Dictionary<string, int>();
            if (!Directory.Exists(directoryPath)) {
                // TODO Handle directory error
            }
            this.CorpusFiles = Directory.GetFiles(directoryPath);
            foreach (var filename in this.CorpusFiles)
            {
                // TODO check if excluded file
                string text = File.ReadAllText(filename);
                text = TextUtilities.CleanText(text);
                string[] words = text.Split(' ');
                foreach (var word in words)
                {
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