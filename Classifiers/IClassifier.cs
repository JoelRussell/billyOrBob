using System.Collections.Generic;

namespace billyOrBob.Classifiers
{
    public interface IClassifier {
        public string Classify(string inputText);

        public void SetConfig(string key, string value);
    }

}