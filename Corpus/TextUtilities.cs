using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace billyOrBob.Corpus
{
    public static class TextUtilities {

        // Prepares a text for tokenising.
        // Leaves only 'words' in format wanted for tokenising, separated by spaces.
        public static string CleanText(string inputText) {
            // Characters to simply remove from the text
            string removeChars = "[-]";
            inputText = Regex.Replace(inputText, removeChars, String.Empty);

            // Characters to replace with spaces
            string spaceChars = "[^A-Za-z']";
            inputText = Regex.Replace(inputText, spaceChars, " ");
            inputText = Regex.Replace(inputText, "\\ {2,}", " ");
            return inputText.ToLower();
        }
    }
}