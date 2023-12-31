# Moms Adopting Moms
Building a detailed customized plan with local resources to reunite biological mothers with their children in foster care

# Overview
"Moms Adopting Moms" nonprofit founder Mary Rathbone created a step-by-step plan to help biological mothers regain custody of their children in foster care, that is used as a model example to the foster system in her state. This mobile-friendly website can be used on the cell phones of parents who have lost their children, to search for localized resources and create a detailed plan customized to the needs of the particular parent (drug detox, finding housing, job, furniture, clothing, or transportation, how to get supervised visits with their kids, counseling and support of mentors, rehab, etc.) with resources from their area. Our app should find concrete local resources in their area for the specific items they need, resulting in a detailed customized plan to get them back on their feet.

# Background
Right now there are multiple court hearings that take place when a child or children are taken into care. The initial court hearing when a child is removed is called a detention hearing. Shortly thereafter a bio mom is presented with a plan from the Department of Family and Children Services which the majority of the time includes six to seven main requirements:

+ Get off drugs (40-50% of all cases)
+ Get a job
+ Get counseling (mental health, abuse, substance, etc.)
+ Take parenting classes
+ See your kids regularly in supervised visitations
+ Get some sort of mode of transport for you and the kids to get to and from work/school
+ Get a home to live in where you will be safe (in case of domestic violence) and furnish it for the children to return
+ The problem is the bio parent normally feels overwhelmed, helpless, hopeless, may be in some sort of incapacitated state, and really has no idea where to start.

Presented with a case plan the veiled threats then begin as to what will happen if the bio mom does not get everything done by such-and-such a date, culminating in the termination of their parental rights. Although the case workers try to help to send in referrals and assist when they can, they are overworked, underpaid and are quitting the field at an alarming rate which means one bio parent could have to work with multiple case workers over a short period of time, none of which can really assist her and tell her step-by-step what she needs to do.

Because of this, a lot of biological parents lose hope, stay on drugs as an escape from their reality and eventually lose their kids. Or they may try for a while or make minimal progress but eventually they either give up entirely or they make it far enough along where DFCS will reunify them with their kids. This lengthy wait is detrimental to the children because it lengthens their time in foster care and therefore keeps them unsettled and experiencing trauma. If they are fortunate enough to reunify because the parent tries to make better decisions 25% of these cases return to care within an approximate two years time. The result is a broken foster system that needs a drastic overhaul.

Today there are over 391,000 kids in the foster care system in the United States. There are in total 117,000 children in the child welfare system without any legal ties to family and are waiting to be adopted. This means the parental rights have been terminated for all of these kids. And on average more than $9,000,000,000 federal and state dollars are spent on caring for foster children through the Social Security act each year. Even more money is spent for foster children on medical care, food stamps, cash welfare, and child care payments.

There is a desperate need to fix outdated systems, create new portals of assistance to the bio moms and case workers and speed the entire process along thereby saving millions and decreasing the trauma the kids in foster care are experiencing. Unfortunately as of right now there is nothing outside a case plan and a few hours a week from a case worker to assist a bio mom in achieving reunification. Not knowing where to turn for assistance is a big issue in how long the entire process takes.

Moms Adopting Moms is a non-profit in the process of being incorporated to provide peer mentors to bio and foster moms to model what building a relationship of support looks like between these moms for long term support and assistance. We will also address the issue of not knowing where to go to get the resources the bio moms need by building and providing them with a mobile app built to consolidate everything they need in an A to Z step by step guide. Step A might be a list of Detox Centers open for them to move into immediately. Step G might be to attend this parenting class at this location and time. Step M could be reunifying with their children. Step Z would be the bio moms achieving their personal goals, the Department of Family and Childrens Services no longer involved, the family living happily together in a fully furnished home with enough money to pay their bills and no longer relying on assistance. This tool will also eventually be used to help preserve families at risk of losing their kids. This app should be generic, accurate, and functional enough to be able to be deployed in every county in the country that has a foster care system.

# Project Status
This project was developed during the Microsoft Global Hackathon 2023 with a limited time window.  As such, it is prototype code and not ready for production usage yet.  In particular, I want to call out the privacy concerns.  During the course of the journey that this application walks a biological mother through, it may ask personal questions about drug usage and such.  We want a strong authorization and authentication solution in place before production usage.  

We have documented some of the remaining work in the [GitHub issues](https://github.com/jennifermarsman/MomsAdoptingMomsApp/issues) section.  

# Setup
The project was developed using Visual Studio.  You can download the free Community edition at https://aka.ms/VSDownload.  

Clone or download this project to your local machine.  Open the .sln file and press "Run" or F5 to build and run the project and test locally.  

# Troubleshooting Guide
Here are some common errors that you may see:

+ **"Error saving file C:\Code\MomsAdoptingMomsApp\Journey\Moms Adopting Moms.xlsx"** or **"The process cannot access the file 'C:\Code\MomsAdoptingMomsApp\Journey\Moms Adopting Moms.xlsx' because it is being used by another process."** - You may have the "Moms Adopting Moms" spreadsheet open in Excel, and this code is fighting with the Excel process over who gets to modify it.  Close Excel and try again.  

+ **Newtonsoft.Json.JsonReaderException: 'After parsing a value an unexpected character was encountered'** - JSON exceptions may indicate that there is an error with the navigation.json file and it is no longer in proper json format.  Run navigation.json through a free JSON formatter online (like https://www.jsonformatter.io) to ensure that the JSON is valid.  

+ **System.Collections.Generic.KeyNotFoundException: 'The given key 'ThirdStep1' was not present in the dictionary.'** - An error like this indicates that the navigation steps don't align.  For example, you may have put something like this:

```
  "SecondStep": {
    "Title": "Congrats!",
    "Text": "Every journey begins with a single step. Imagine yourself on this journey with us. You will have help and encouragement along the way. There will be mountains to climb, rivers to cross and obstacles that might slow you down but you can do it! \n\nBe the hero for your children and be the one responsible for bringing them home! ",
    "Inputs": null,
    "Responses": {
      "Next": "ThirdStep1"
    },
    "Reward": "none"
  },
  "ThirdStep": {
    "Title": "Let's do this",
    "Text": "So are you READY? Let’s do this!!!!",
    "Inputs": null,
    "Responses": {
      "Next": "StepA"
    },
    "Reward": "none"
  },
  ```

  In this scenario, the "Next" step for SecondStep is called "ThirdStep1", but the actual step name is "ThirdStep", so it can't find the step when doing a lookup.  To fix: 
  1. Ensure that the values for all Responses keys correspond to valid steps in the navigation.json file.  
  2. You may also have to edit the "Moms Adopting Moms" spreadsheet if a bad key got written there, or else the biological parent will be stuck on that step.  Check the "CurrentStep" column and ensure all steps are valid.  

# Modifying the Journey
The steps in the journey are documented in the navigation.json file.  Each step should follow this format:

```
  "StepID": {
    "Title": "Put your title text here!",
    "Text": "Congrats! You have taken the first step in your journey! You logged in! Welcome {FirstName}!  Today is a new day and you got this!  We are 100% on your side and if you follow the suggestions we will give you through this website, you WILL get your kids back faster and then we promise to help you THRIVE post reunification as well.",
    "Inputs": "FirstName",
    "Responses": {
      "ButtonText1": "StepID-if-they-click-this-button"
    },
    "Reward": "medal.gif", 
    "Functions": {
      "Search": "detox centers near me"
    }
  },
  "StepID-if-they-click-this-button": {
    FILL IN ALL OF THE SAME THINGS HERE...
  }
  ```

The **StepID** should be an identifier for the step - this information is not displayed anywhere in the UI, just used internally to identify each step.  

Update the value of the **Title** with the title text that you want shown to the user.

Update the value of the **Text** with the text that should be displayed for that step.  If you want to pull in information from the "Moms Adopting Moms" spreadsheet, include the column name of the field you want enclosed in curly braces like you see above with {FirstName}, and provide a corresponding **Inputs** value.  

You can have multiple **Responses** to the questions asked in the step, which can lead to different paths.  For each item in Responses, the first part is the text that will be displayed in the button (like "ButtonText1"), and the second part is the StepID of the next step that this choice should take them to (like "StepID-if-they-click-this-button").  This StepID must also exist as a separate defined step in the navigation.json file.  

If you want to display a **Reward** image for a momentous step, just add it to the project's img folder, and then reference the name of the image as the value of the Reward, like "medal.gif" above.  

There is an optional **Functions** which contains functions to call during that step.  One function that has been implemented already is Search, and it takes a parameter of the query term to search for (such as "detox centers near me" above).  Other functions could be added as needed, but this will require a code change to add the corresponding code.  The code does support reflection to call the method name found here in the navigation file (such as "Search").  
