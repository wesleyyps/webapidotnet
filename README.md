# WebAPI

DFO WebAPI + Angular Test

In order to debug or profile this application you must have Visual Studio 2019 installed on your machine.
Run dotnet restore on nuget console to retrieve all needed packages.
If you want to run the application on IIS Express, you will need to change the apiUrl at frontend app.js to http://localhost:51407 and run on console grunt to compile all assets again.

To run this project on your machine without the need for Visual Studio 2019, download the binary executable from [this link](https://github.com/wesleyyps/webapidotnet/releases/download/1.0.0/WebAPI.rar). Unzip the downloaded file and execute a file named WebAPI.exe. This will start an app responding at http://localhost:5000/api
