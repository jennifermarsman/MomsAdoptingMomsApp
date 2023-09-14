using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

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
            if ( reward == null || reward == "none")
            {
                ViewData["Reward"] = @"img\clear.png";
            }
            else
            {
                ViewData["Reward"] = @"img\" + reward;
            }
            // TODO: any sizing work that needs to be done here?  Proper Grid/layout for phone?
        }

        public void OnButtonPress()
        {
            // TODO: Store off user's progress in journey - step name
            // TODO: Redraw page based on next step and what button was clicked
            // TODO: change currentStep
            // TODO: call LoadPage() with new step
        }
    }
}