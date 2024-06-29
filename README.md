# toyRobot

**Overview**

My implementation of the Toy Robot application itself is quite simple as I tried to split the focus between creating the app itself and getting it set up and deploying to Azure.
I created the app in C#, using a very basic Blazor frontend. I then use a simple github actions pipeline to build, test and deploy to an Azure web app.

**Running locally**

The two easiest ways to run the app locally would be to either:
- Run directly through Visual Studio, it should automatically open the page in your default browser
- Run `dotnet run` in /RobotApp/RobotApp, then access the app through localhost on whichever port the `dotnet run` command provides


**Components used and reasoning**

Azure 
- A large part of this role is managing Azure infrastructure, so hosting the app on there seemed a no brainer.
- I used a Web App each for a dev and production "environment" but both are identical, just being deployed from different branches in the repo.
- A decent amount of my time at the start of working on this was spent on exploring Azure and playing around with a few different options for how to host the app. I did a little experimenting with Azure Container Apps and Funtion Apps before landing on a simple Web App as it seems the most fit for purpose and simple enough to work with considering the short timeframe of this project.
- I started this project with only the most minimal experience with Azure, so it was quite rewarding to be able to jump in and get something even as simple as this app hosted and automatically deploying so quickly.

Github
- The code for the app and the yml files for the actions workflows live here. I used a simple branching strategy with a main and dev branch, each that have an actions workflow to build and deploy to their Azure Web App. Code changes were made on feature branches off of dev that were then merged in with a pull reuqest.
- The actions workflows are quite simple, they have a build phase that will set up .NET then build the app, run my unit tests and publish. Once the artifact is uploaded the deploy job will start, download the artifact, login to Azure and deploy to the specified Web App.

.NET
- With Azure being decided on, it seemed most natural to write the app in .NET/C#. I'm most familiar with Java, but I have some experience with C# and it's similar enough for me to create a simple app like this without much fuss.
- Since I decided on deploying to an Azure Web App I needed at least a simple front end for the app and this is not an area I am strong with. Looking over the example project you provided me, Blazor seemed the most simple to use to plug in a simple command input to and start processing user commands and show the required outputs. I spent minimal time and effort on the front end, just enough to allow a user to input commands and see the REPORT outputs.
- xUnit was picked for unit testing as it was what the tutorial on Blazor testing I watched used (Though I did not get around to actually getting the tests inputting commands through the UI working, so I could have used any testing framework but they're all quite similar for a simple project like this regardless)
- TDD and some SOLID priciples were kept in mind when developing the app.

**App**

Can be found here: https://toyrobotwebapp.azurewebsites.net/ and in DEV if you're interested here: https://robotwebappdev.azurewebsites.net/

Rules:
- Commands are case insensitive
- First command must be a PLACE command
- Following commands can be any of MOVE, LEFT, RIGHT and REPORT in any order, as many times as you like
- Invalid commands will be ignored, invalid commands are PLACE or MOVE commands that would result in the robot being outside the table bounds or any PLACE commands made after the first valid one.

**Possible improvements and what I would add if continuing the project**

Things to Improve:
- The frontend. There is obviously a lot of room to improve on this barebones UI. With more time to learn Blazor and frontend dev skills in general I would have liked to implement a console-like interface with a rolling command history at least, and ideally some kind of graphical view of the table  and the robot moving on it in response to commands. However, with the nature of this being a devops role I think frontend improvements are the least important here.
- Getting the UI testing through bUnit to work properly and test user input.
- Some general code improvements. With more time I could definitely clean up the code to better follow OOP coding principles like SOLID and probably handle possible errors better. These would be minor tweaks to what I have delivered, not major rewrites.

What I would add:
- Implement logging. I would love to add some simple logging to the app to have available to play around with in Azure, set up some log queries and alerts just to get a feel for how that works on the platform and how it compares to Splunk.
- Do more with the pipelines. My split between dev and prod is just in name only at the moment, functionally they are identical. I would love to make some changes to play around with using environment variables to change the behaviour between the two just as an example.
- Containerise the application. The current deployment is quite simple, I would like to get the app running in Azure on containers to explore how this is done on the platform.
