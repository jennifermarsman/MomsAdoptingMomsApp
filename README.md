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

# Setup
Use the following commands in a python environment (such as an Anaconda prompt window) to set up your environment.  This creates and activates an environment and installs the required packages.  For subsequent runs after the initial install, you will only need to activate the environment and then run the python script.  

### First run
```
conda create --name moms python=3.9 -y
conda activate moms

pip install -r requirements.txt
python main.py
```

### Subsequent runs
```
conda activate moms
python main.py
```
