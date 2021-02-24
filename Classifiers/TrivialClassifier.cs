using System.Collections.Generic;

namespace billyOrBob.Classifiers
{
    public class TrivialClassifier : IClassifier {
        
        public Dictionary<string, string> Config = new Dictionary<string, string>();
        public TrivialClassifier() {
            SetConfig("word","auld");
        }

        public string Classify(string inputText) {
            if (inputText.Contains(Config["word"])) {
                return "This is Burns";
            }
            else {
                return "This is Shakespeare";
            }
        }
        public void SetConfig(string key, string value) {
            Config.Add(key, value);
        }
    }

}