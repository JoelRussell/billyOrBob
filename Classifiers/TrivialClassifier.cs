using System.Collections.Generic;

namespace billyOrBob.Classifiers
{
    public class TrivialClassifier : IClassifier {
        
        public Dictionary<string, string> config = new Dictionary<string, string>();
        public TrivialClassifier() {
            SetConfig("word","auld");
        }

        public string Classify(string inputText) {
            if (inputText.Contains(config["word"])) {
                return "This is Burns";
            }
            else {
                return "This is Shakespeare";
            }
        }
        public void SetConfig(string key, string value) {
            config.Add(key, value);
        }
    }

}