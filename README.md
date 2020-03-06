Call detail record (CDR) business intelligence platform. The idea behind it is that the call records can be queried to find information such as average call cost, longest calls, average number of calls in a given time period.

Technologies Used:

Latest version of C#, Asp.net core 3.1, MVC, Newtonsoft.Json and other libraries.

Assumptions:
- MVC is used as it could be easy to create new end-points and routes on the fly.
- All of the database table (CDR) fields are set to varchar type instead of using the matching types with the cdr columns. 
  This is to avoid sql bulk inserting errors in case any of the cdrs are not in the correct format but the code will take care of the data types.
- CDR file will always have the fixed headers, the columns are comma delimited and always in a csv format.
- Data transfer could be more efficient as most of the methods are asynchronous.

Further enhancements:
- Many end-points as required
- The same service could be used build frontend user interface as well as provide API in the background.
- Multi tenancy
- Token based authentication using JWT

Required:
- Visual studio 2019 for opening the solution.
- Microsoft SQL server => 2008
- .NET Core SDK 3.1.100 (or newer) (x86/x64 according to your Windows)
- .NET Framework 4.5 (or newer)
- Windows 2008/2012/2016/7/8/10 or newer with Internet Information Services (IIS)
- .NET Core 3.1.0 (or newer) Runtime & Hosting Bundle


To publish the application on IIS:-

- Install asp.net core 3.1 hosting bundle
- Create an application pool with no managed code and set the "Enable 32-bit applications = false" on advanced setting of the pool.
- Create a site and attach the application to the site. Set the virtual folder to the ~\Aurora\bin\Release\netcoreapp3.1\publish folder.
- Instead of using the above method, use the Kestrel to run the app.  At the command prompt, browse to the solution folder and run the command "dotnet run" without the quotes and browse the site at localhost:5000

- Set the connection string in the appsettings.json (and appsettings.development.json) file using youe sql server username, and password and the catelogue name.
- A tsql script (script.sql) is available to create the database and tables. Please change the file paths in the file to your drive locations.

- CDR table can be cleared before loading new files each time. To do this, set the value of the key "ClearCDRTableBeforeLoading": "Yes" in the appsettings.json file. 
	Set the value to "No" if this is not required and this is the default.

Testing:

- Change the host address and port number to your environment accordingly:

- default site is http://localhost:5000

- The return types of the requests are in JSON format

- All the HTTP-Get requests (using the urls below) can be tested using the web browsers and the POST request can be tested using PostMan or Fiddler.

- Upload files using the api:
- Place the cdr files in the site/files folder and then browse the route below.
- http://localhost:5000/api/upload 

- Upto 120MB files are supported.

- To test the API using the Postman:
	(Use POST Method, body form-data,change the text type to file type, set the key as 'name' and select the file to upload).
	The selected file will be bulk inserted in to the database once the uploading has been completed.)


- Use the date query strings as dd-mm-yyyy and slash '/' for seperating parameters. 
	(to get the date range, use the format dd-mm-yyyy / dd-mm-yyyy without spaces). caller_id is optional and it can be used to filter any caller_id pattern.
 	for example http://localhost:5000/api/list/01-08-2016/02-08-2016/44138 list all calls for the period for the caller_id(s) starting    with the number 44138.

- Get a list of calls of a CLI for a period (Change the date range and caller_id '01-08-2016/01-08-2016/441386555335' as required):
	http://localhost:5000/api/list/01-08-2016/01-08-2016/441386555335

- Get Top 10 high duration calls for a period (Change number of calls and date range '10/01-08-2016/01-08-2016/441386555335' as   required):
	http://localhost:5000/api/topdur/10/01-08-2016/01-08-2016/441386555335

- Get Top 10 high cost calls for a period (Change number of calls and date range '10/01-08-2016/01-08-2016/441386555335' as required):
	http://localhost:5000/api/topcost/10/01-08-2016/01-08-2016/441386555335

- Get average cost per day for a period  (Change the date range 01-08-2016/01-08-2016/441386555335 as required):
	http://localhost:5000/api/avgcost/01-08-2016/01-08-2016/441386555335

- Get average calls per day for a period  (Change the date range 01-08-2016/01-08-2016/441386555335 as required):
	http://localhost:5000/api/avgcall/01-08-2016/01-08-2016/441386555335

- Get Total cost for a period  (Change the date range 01-08-2016/01-08-2016/441386555335 as required):
	http://localhost:5000/api/cost/01-08-2016/01-08-2016/441386555335
  
- To clear the table 	http://localhost:5000/api/delete
