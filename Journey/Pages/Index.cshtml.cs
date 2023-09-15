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
        string currentStep = "FirstStep";

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
            currentStep = "FirstStep";

            // TODO: Test and ensure that the navigation json loads only once and is held in memory
            LoadJson();
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
            ViewData["Heading"] = steps[step].Title;
            // TODO: need to format Text to include Input (name and such) in the output
            ViewData["Text"] = steps[step].Text;

            // TODO: dynamically generate buttons in html layout

            // TODO: use Functions

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

        public void OnButtonPress()
        {
            Console.WriteLine("Button Pressed");
            // TODO: Store off user's progress in journey - step name
            // TODO: Redraw page based on next step and what button was clicked
            // TODO: change currentStep
            // TODO: call LoadPage() with new step
        }

        public IActionResult OnPost(string action)
        {
            if (action == "Action1")
            {
                Console.WriteLine("Button1 Pressed");
                // Handle the click event for Button 1
                // You can perform any necessary actions for Button 1
            }
            else if (action == "Action2")
            {
                // Handle the click event for Button 2
                // You can perform any necessary actions for Button 2
            }
            else if (action == "StepA")
            {
                MethodInfo methodInfo = this.GetType().GetMethod(action);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(this, null);
                }
            }
            else
            {
                // Handle other cases or errors
            }

            return Page();
        }

        public void StepA()
        {
            Console.WriteLine("StepA has been called");
        }
    }
}