namespace billyOrBob.Classifiers
{
    public class TrivialClassifier : IClassifier {
        public TrivialClassifier() {

        }

        public string Classify(string inputText) {
            if (inputText.Contains("auld")) {
                return "This is Burns";
            }
            else {
                return "This is Shakespeare";
            }
        }
    }

}