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
        public string Result { get; private set; } = null;
        public string TextToTest { get; private set; } = "";
        public string[] ShakespeareExclude { get; private set; } = {};
        public string[] BurnsExclude { get; private set; } = {};
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
            ShakespeareFiles = FileNames(Configuration["ShakespeareCorpus"]);
            BurnsFiles = FileNames(Configuration["BurnsCorpus"]);
        }

        public void OnGet()
        {

        }

        public void OnPost() {
            TextToTest = Request.Form["testTextInput"];
            IClassifier classifier;
            if (Request.Form["optionClassifier"] == "frequency") {
                classifier = new FrequencyClassifier();
                classifier.SetConfig("ShakespeareCorpus", Configuration["ShakespeareCorpus"]);
                if (!String.IsNullOrEmpty(Request.Form["optionShakespeareExclude"])){
                    classifier.SetConfig("shakespeareExclude", Request.Form["optionShakespeareExclude"]);
                    ShakespeareExclude = Request.Form["optionShakespeareExclude"].ToString().Split(',');
                }
                if (!String.IsNullOrEmpty(Request.Form["optionBurnsExclude"])){
                    classifier.SetConfig("burnsExclude", Request.Form["optionBurnsExclude"]);
                    BurnsExclude = Request.Form["optionBurnsExclude"].ToString().Split(',');
                }
                classifier.SetConfig("BurnsCorpus", Configuration["BurnsCorpus"]);
            } else {
                classifier = new TrivialClassifier();
            }
            Result = classifier.Classify(TextToTest);
        }

        public string[] FileNames(string directory) {
            return new DirectoryInfo(directory).GetFiles().Select(f => f.Name).ToArray();
        }
    }
}
