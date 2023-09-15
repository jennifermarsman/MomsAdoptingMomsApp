using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Journey.Pages
{
    public class IndexModel : PageModel
    {
        Dictionary<string, Step> steps;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // TODO: Pull the identity and current step from somewhere
            // TODO: Based on identity, retrieve information (hardcoding for now)
            string FirstName = "Mary";
            //string Address = "100 Peachtree Lane";
            //string City = "Atlanta";
            //string State = "GA";
            string ZipCode = "30303";
            string currentStep = "FirstStep";

            // TODO: Test and ensure that the navigation json loads only once and is held in memory
            //LoadJson();

            // TODO: maybe some error checking here?  Send a text or email to Mary if the currentStep isn't found, to show that there is a typo in the Excel spreadsheet?
            LoadPage(currentStep);

            // TODO: reward screen separate?  or add image to step layout?  
            // TODO: how do we accommodate feedback from peer mentor?  Notification?  
        }

        private void LoadJson()
        {
            using (StreamReader r = new StreamReader("navigation.json"))
            {
                string strJson = r.ReadToEnd();
                steps = JsonConvert.DeserializeObject<Dictionary<string, Step>>(strJson);
            }
        }

        private void LoadPage(string step)
        {
            // TODO: how do I not have to do this here?  Would prefer one load.
            LoadJson();

            ViewData["Heading"] = steps[step].Title;
            // TODO: need to format Text to include Input (name and such) in the output
            // TODO: use Functions

            ViewData["Text"] = steps[step].Text;
            
            // Rewards
            string? reward = steps[step].Reward;
            if (reward == null || reward == "none")
            {
                ViewData["Reward"] = @"img\clear.png";
            }
            else
            {
                ViewData["Reward"] = @"img\" + reward;
            }

            // Dynamically generate buttons in html layout
            int numberOfButtons = steps[step].Responses.Count();
            ViewData["NumberOfButtons"] = numberOfButtons;
            for (int i = 0; i < numberOfButtons; i++)
            {
                ViewData["ResponsesText" + i] = steps[step].Responses.Keys.ElementAt(i).ToString();
                ViewData["ResponsesAction" + i] = steps[step].Responses.Values.ElementAt(i).ToString();
            }

            Console.WriteLine("Responses: ", ViewData["Responses"]);
            // TODO: any sizing work that needs to be done here?  Proper Grid/layout for phone?
        }

        public IActionResult OnPost(string action)
        {
            // TODO: Store off user's progress in journey - step name

            // Redraw page based on next step and what button was clicked
            LoadPage(action);
            return Page();
        }

        private void InvokeFunction(string functionName)
        {
            MethodInfo methodInfo = this.GetType().GetMethod(functionName);
            if (methodInfo != null)
            {
                methodInfo.Invoke(this, null);
            }
        }

        public void SearchForDetox()
        {
            // TODO: Bing Search code here, using zip code
            Console.WriteLine("SearchForDetox has been called");
        }
    }
}