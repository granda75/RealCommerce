# RealCommerce
Server side WEB.API project for Web application that shows the weather of some city.

The project was developed on the server with .NET Framework(Web API) with Visual Studio Community 2019, on the client side with Angular 9 and Bootstrap CSS with Visual Studio Code, version 1.47.1.

To create database run please the script from the the file WeatherDB.sql. The file exists in the root of RealCommerce repository.

To run the servers project you need download code from RealCommerce repository and open RealCommerce.sln - server side solution in the Visual Studio, run the command in the Package Manager Console: Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r and start the project. 
On my computer the server was started with IIS Express with project URL: https://localhost:44382/. This way the server will be started.

To run clients side you need open the RealCommerceClient repository, download the real-weather-site.zip file and extract files from it.
After that open the real-weather-site folder in the Visual Studio Code and execute npm install and ng serve commands.
After the commands the clients side will be available with the URL http://localhost:4200/. 
In the environments\environment.ts file you can find the configuration for development.
I used the configuration to define endpoint to connect to the server's service. If you change servers port it is need to change the clients side endpoint URL too.

export const environment = {
  production: false,
  apiEndpoint: 'https://localhost:44382/api'
};

Best regards !
