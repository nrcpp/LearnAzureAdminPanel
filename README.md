
# Learn Azure Admin Panel
This project is a simple CMS to populate data for Learn Azure [mobile](https://learnazure.app) &amp; [web](https://learnazure.pro) app

**Core features:**
- Role-based authentication and authorization   
- Models, Views and Controllers to populate content for Educational projects like [Learn Azure](https://learnazure.app)
- Entity Framework for MySQL with Code-First approach. Tables and columns will be created automatically. Auto-migration is enabled
- Modern UI/UX with Razor and bootstrap
- Deployed and tested with [Azure App Services](https://portal.azure.com) and Smarterasp.net hosting

## Technology Stack

 - .NET 4.6 or higher
 - ASP.NET MVC
 - MySQL

## Getting started
- Download and install [MySQL 8 database](https://www.mysql.com/downloads/) engine on localhost if not yet. Then setup its superuser and save credentials
- Create databases with such names: *learn_azure_admin* and *learn_azure_admin_users* (although you can use your own names, those one just inside connectionStrings section of config)
-  Pull the sources and edit Web.config *connectionStrings* section to connect to your database. Edit uid and password there to match your MySQL configuration. Note that there are different connection string for localhost and production environments 
- Compile and run web-app in your local environment. You can use this admin panel to populate database with learning content. 

## Demo
**https://admin.learnazure.app**

**Also**

You may check of how the output of Learn Azure Admin Panel uses in mobile app with >50,000 of users:

For iOS: 
https://https://apps.apple.com/us/app/learn-azure-fundamentals/id1531326622

For Android: 
https://play.google.com/store/apps/details?id=com.learnazure.app


