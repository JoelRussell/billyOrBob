﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using billyOrBob.Classifiers;

namespace billyOrBob.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration Configuration;

        // TODO Reference actual files
        public string[] ShakespeareFiles { get; private set; } = {};
        public string[] BurnsFiles { get; private set; } = {};
        public string result { get; private set; } = null;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
            ShakespeareFiles = fileNames(Configuration["ShakespeareCorpus"]);
            BurnsFiles = fileNames(Configuration["BurnsCorpus"]);
        }

        public void OnGet()
        {
            //string text = Request.Form["t"];

        }

        public void OnPost() {
            string textToTest = Request.Form["testTextInput"];
            IClassifier classifier = new TrivialClassifier();
            result = classifier.Classify(textToTest);
        }

        public string[] fileNames(string directory) {
            return new DirectoryInfo(directory).GetFiles().Select(f => f.Name).ToArray();
        }
    }
}
