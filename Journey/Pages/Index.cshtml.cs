using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;
using System.Data;
using OfficeOpenXml;
using System.Net.Mime;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using static OfficeOpenXml.ExcelErrorValue;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Journey.Pages
{
    public class IndexModel : PageModel
    {
        Dictionary<string, Step> steps;
        const string excelFile = @"Moms Adopting Moms.xlsx";

        // We are storing the following in the HttpContext.Session:
        // string ParentID - unique code given to each bio mom
        // int UserRow - row that corresponds to that parent in the Excel

        // TODO: user has to input ParentID
        private string? ParentID;
        public string? FirstName;   // needs to be public to access by reflection
        private string? ZipCode;
        private string? CurrentStep;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public bool IsTextBoxVisible { get; set; } = false; // Set the initial visibility


        public void OnGet()
        {
            // TODO: get parentID from intro screen
            ParentID = "12345";
            HttpContext.Session.SetString("ParentID", ParentID);

            // Based on parentID, retrieve user data and current step from Excel
            LoadExcel();

            // TODO: The navigation json should load only once here and be held in memory
            //HttpContext.Session.Set()
            //LoadJson();

            // TODO: maybe some error checking here?  Send a text or email to Mary if the currentStep isn't found, to show that there is a typo in the Excel spreadsheet?
            LoadPage(CurrentStep);

            // TODO: reward screen separate?  or add image to step layout?  
            // TODO: how do we accommodate feedback from peer mentor?  Notification?  
        }

        private void LoadExcel()
        {
            // Usage by a nonprofit
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(excelFile)))
            {
                var sheet = package.Workbook.Worksheets["Bio Mom Data"];
                //int colCount = sheet.Dimension.End.Column;  //get Column Count
                //int rowCount = sheet.Dimension.End.Row;     //get row count

                // Get important column numbers
                int colParentID = GetColumnByName(sheet, "Parent ID");
                int colFirstName = GetColumnByName(sheet, "First Name");
                int colZipCode = GetColumnByName(sheet, "Zip Code");
                int colCurrentStep = GetColumnByName(sheet, "Current Step");
 
                // Get the row of the user in the spreadsheet
                int userRow = GetUserRow(sheet, ParentID);

                // Get the user data we care about.  
                FirstName = sheet.Cells[userRow, colFirstName].Value?.ToString();
                ZipCode = sheet.Cells[userRow, colZipCode].Value?.ToString();
                CurrentStep = sheet.Cells[userRow, colCurrentStep].Value?.ToString();

                // Add to session state
                HttpContext.Session.SetString("FirstName", FirstName);
                HttpContext.Session.SetString("ZipCode", ZipCode);
                HttpContext.Session.SetString("CurrentStep", CurrentStep);
            }
        }

        private int GetUserRow(ExcelWorksheet sheet, string parentID)
        {
            int? row = HttpContext.Session.GetInt32("UserRow");
            if (row == null)
            {
                var query =
                    from cell in sheet.Cells["A:A"]
                    // TODO: decide which way is better
                    where cell.Value?.ToString() == HttpContext.Session.GetString("ParentID")
                    //where cell.Value?.ToString() == parentID
                    select cell;

                // TODO: confirm that StartRow == EndRow?  
                int userRow = query.Single().EntireRow.StartRow;

                // Store in session so we aren't loading so much
                HttpContext.Session.SetInt32("UserRow", userRow);
                row = userRow;
            }
            return (int)row;
        }       

        private int GetColumnByName(ExcelWorksheet ws, string columnName)
        {
            if (ws == null) throw new ArgumentNullException(nameof(ws));
            return ws.Cells["1:1"].First(c => c.Value.ToString() == columnName).Start.Column;
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

            if (!steps.ContainsKey(step)) {
                // The navigation.json file is messed up.  
                // TODO: send an email to Mary?  
            }

            ViewData["Heading"] = steps[step].Title;

            // Format Text to include Input (name and such) in the output
            if (steps[step].Inputs != null && steps[step].Inputs.ToLower() != "none")
            {
                // TODO: currently, this isn't actually using the Inputs value
                // at all.  Maybe we should remove and just use if string contains
                // curly braces.  
                
                // Scan string, looking for {}.  Remove the variable name between and replace with integers.
                string original = steps[step].Text.ToString();
                string copy = original;
                int i = 0;
                List<string> variables = new List<string>();
                while (copy.IndexOf('{') != -1)
                {
                    int start = copy.IndexOf('{');
                    int end = copy.IndexOf('}');
                    string variableName = copy.Substring(start + 1, end - start - 1);
                    string variable = (string)GetType().GetField(variableName).GetValue(this);
                    original = original.Replace(variableName, i.ToString());
                    // TODO: something like ReplaceAt might be safer
                    variables.Add(variable);
                    copy = copy.Substring(end + 1, copy.Length - end - 1);
                    i++;
                }
                ViewData["Text"] = string.Format(original, variables.ToArray());
            }
            else
            {
                ViewData["Text"] = steps[step].Text;
            }
       
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

            // Functions support
            ViewData["SearchResults"] = "";
            Console.WriteLine("Responses: ", ViewData["Responses"]);

            if (steps[step].Functions != null)
            {
                for (int i = 0; i < steps[step].Functions.Count(); i++)
                {
                    string functionName = steps[step].Functions.ElementAt(i).Key.ToString();
                    // Read the arguments to the function from the step
                    string functionArguments = steps[step].Functions.ElementAt(i).Value.ToString();
                    if (functionName == "Search")
                    {
                        string searchResults = Search(functionArguments);
                        ViewData["SearchResults"] = searchResults;
                    }
                    else
                    {
                        InvokeFunction(functionName, functionArguments);
                    }
                }
            }

            // TODO: any sizing work that needs to be done here?  Proper Grid/layout for phone?
        }

        public IActionResult OnPost(string action)
        {
            // Store off user's progress in journey
            UpdateCurrentStep(action);

            // Redraw page based on next step and what button was clicked
            IsTextBoxVisible = false;
            LoadPage(action);
            return Page();
        }

        private void UpdateCurrentStep(string newStep)
        {
            CurrentStep = newStep;

            // Store (in Excel) the user's progress in journey with new step
            using (var package = new ExcelPackage(new FileInfo(excelFile)))
            {
                var sheet = package.Workbook.Worksheets["Bio Mom Data"];
                int colParentID = GetColumnByName(sheet, "Parent ID");
                int colCurrentStep = GetColumnByName(sheet, "Current Step");
                int userRow = GetUserRow(sheet, ParentID);
                sheet.Cells[userRow, colCurrentStep].Value = newStep;
                package.Save();
            }
        }

        private void InvokeFunction(string functionName, string arguments)
        {
            MethodInfo methodInfo = this.GetType().GetMethod(functionName);
            if (methodInfo != null)
            {
                string[] args = arguments.Split(",");
                methodInfo.Invoke(this, args);
            }
        }

        public string Search(string query)
        {
            if (query.IndexOf("near me", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                string? zip = (ZipCode != null) ? ZipCode : HttpContext.Session.GetString("ZipCode");
                if (zip != null)
                {
                    query = query.Replace("me", zip, StringComparison.OrdinalIgnoreCase);
                }
            }
            var results = GoogleSearch.Search(query);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Here are some resources that might help:");
            foreach (Result result in results)
            {
                sb.AppendFormat("<a href={0}>{1}</a>\n", result.Link, result.Title);
            }
            return sb.ToString();
        }
    }
}