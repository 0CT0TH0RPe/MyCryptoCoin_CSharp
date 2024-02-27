# MyCryptoCoin Deployment Guide

This guide provides step-by-step instructions on how to deploy the MyCryptoCoin project in a production environment. Before proceeding with the deployment, please ensure that you have the necessary infrastructure and dependencies ready.

## Prerequisites

1. **Operating System**: The deployment process is compatible with Linux-based systems (Ubuntu, CentOS, etc.) and Windows Server.

2. **.NET Core SDK**: Ensure that you have the latest .NET Core SDK installed on the server to run the MyCryptoCoin application.

3. **Database**: Set up a compatible database server (e.g., MySQL, PostgreSQL, or SQL Server) to store blockchain data.

4. **Node.js**: For the web application, you will need Node.js and npm (Node Package Manager) installed on the server.

5. **Reverse Proxy (Optional)**: If you want to serve the web application via a reverse proxy (e.g., Nginx or Apache), install and configure it accordingly.

## Deployment Steps

1. **Clone the Repository**: Clone the MyCryptoCoin repository from the project's version control system to your deployment server.

```bash
git clone https://github.com/your_username/MyCryptoCoin.git
```

2. **Configure Database**: Update the `appsettings.json` file in the `MyCryptoCoinAPI` project with the database connection string for your chosen database system.

```json
"ConnectionStrings": {
  "MyCryptoCoinDB": "Your_Database_Connection_String"
},
```

3. **Build and Publish the API**: Navigate to the `MyCryptoCoinAPI` directory and publish the API project.

```bash
cd MyCryptoCoinAPI
dotnet publish -c Release -o /path/to/publish_folder
```

4. **Build and Publish the Worker**: Similarly, navigate to the `MyCryptoCoinWorker` directory and publish the Worker project.

```bash
cd MyCryptoCoinWorker
dotnet publish -c Release -o /path/to/publish_folder
```

5. **Build and Publish the Web Application**: Go to the `MyCryptoCoinWebApp` directory and build the web application.

```bash
cd MyCryptoCoinWebApp
npm install
npm run build
```

6. **Configure API URL in Web Application**: In the web application's source code, update the API base URL to point to your deployed API server.

```javascript
// In a JavaScript file (e.g., axios.js)
export const API_BASE_URL = 'https://api.mycryptocoin.com'; // Replace with your API URL
```

7. **Set Up Web Application Hosting**: Host the web application using a web server like Nginx or Apache. Configure the server to serve the web application from the output directory of the build (usually `dist` folder).

8. **Run the API**: Deploy the API by running the published `MyCryptoCoinAPI.dll` on the server.

```bash
dotnet /path/to/publish_folder/MyCryptoCoinAPI.dll
```

9. **Run the Worker**: Deploy the Worker by running the published `MyCryptoCoinWorker.dll` on the server.

```bash
dotnet /path/to/publish_folder/MyCryptoCoinWorker.dll
```

10. **Set Up Database**: Create the necessary tables and schema in your database using the provided SQL scripts or Entity Framework migrations.

11. **Configure Firewall (Optional)**: If required, configure the server's firewall settings to allow incoming connections on the necessary ports for API and networking.

12. **Set Up SSL (Optional)**: For secure communication, obtain an SSL certificate and configure your web server to enable HTTPS.

## Conclusion

Congratulations! You have successfully deployed the MyCryptoCoin project in a production environment. Your blockchain platform is now accessible to external clients and users, and you can start exploring its various features and functionalities. Ensure that you monitor the system regularly and keep it up to date with security patches and updates. Enjoy using MyCryptoCoin!