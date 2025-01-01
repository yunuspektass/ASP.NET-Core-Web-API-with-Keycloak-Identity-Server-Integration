## ASP.NET Core Web API with Keycloak Integration

This project demonstrates how to integrate **Keycloak Identity Server** with an **ASP.NET Core Web API**. It provides secure authentication and authorization using JWT tokens.

### Features
- **Authentication**: Secure login with Keycloak.
- **Authorization**: Role-based access control (RBAC) for API endpoints.

### Technologies
- **ASP.NET Core 8**
- **Keycloak**
- **JWT**

### How to Run
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yunuspektass/ASP.NET-Core-Web-API-Keycloak-Integration.git
   ```
2. **Set Up Keycloak**:
   - Install and configure Keycloak (local or remote).
   - Create a realm, client, and roles in Keycloak.
   - Update the Keycloak configuration in `appsettings.json`:
     ```json
     "Keycloak": {
       "Authority": "https://your-keycloak-server/auth/realms/your-realm",
       "Audience": "your-client-id"
     }
     ```
3. **Run the Project**:
   - Install dependencies:
     ```bash
     dotnet restore
     ```
   - Run the project:
     ```bash
     dotnet run
     ```

### Example API Endpoints
- **Login**: `POST /api/auth/login`
- **Get User Info**: `GET /api/user`
- **Protected Endpoint**: `GET /api/protected`

### Contributing
Contributions are welcome! Fork the repository, create a new branch, and submit a pull request.

### License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

### Why Keycloak?
Keycloak is an open-source identity and access management solution that provides secure authentication and authorization for modern applications.

### Why ASP.NET Core?
ASP.NET Core is a powerful framework for building high-performance, cross-platform web APIs.

---

### Links
- [Keycloak Documentation](https://www.keycloak.org/documentation)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core)
```
