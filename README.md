#  User Management System (ASP.NET Core)

A robust user management system built using **ASP.NET Core**, **Entity Framework Core**, and **ASP.NET Identity**, supporting:

- User registration and login
- Email confirmation
- Password reset
- Generic repository pattern
- Pagination, sorting, and filtering
- Token-based authentication with JWT
- Postman-tested APIs

##  Features

1. User Registration & Login  
2. Email Confirmation via SendGrid  
3. Password Reset Flow   
4. JWT Authentication  
5. Global Exception Handling via Middleware  
6. Dynamic Pagination, Sorting & Search using `System.Linq.Dynamic.Core`  
7. Clean Architecture using Generic Repository Pattern  
8. Secure Password Hashing with ASP.NET Identity  
9. DTO-based Mapping using AutoMapper  
10. Fully tested via Postman  
11. Configurable via `appsettings.json`

##  Technologies Used

- ASP.NET Core 7 / 8
- Entity Framework Core
- ASP.NET Core Identity
- AutoMapper
- SendGrid (for email)
- System.Linq.Dynamic.Core
- JWT Authentication
- SQL Server (or any EF-compatible DB)
- Swagger (optional for API documentation)
- Postman (for testing)

##  Sample API End Points

 -> POST	/api/auth/login	    Login user and receive token.
 -> GET	/api/auth/confirm-email	Confirms user email.
 -> POST	/api/auth/set-password	Sets new password. (Authorized By JWT)
 -> POST	/api/user/registeruser	Registers the users. (Authorized By JWT)
 -> GET	/api/user/getpagedusers	  Get paginated user list. (Authorized By JWT)
 -> GET	/api/user/getallusers   Gets the data of All users. (Authorized By JWT)
 -> GET	/api/user/getuserbyid/id   Gets the data of user by id. (Authorized By JWT)
 -> PUT	/api/user/updateuserdetails/id   Updates the data of user by id. (Authorized By JWT)
 -> DELETE	/api/user/deleteuser/id   Deletes the data of user by id. (Authorized By JWT)

##  Authentication

-> JWT-based token generation during login.

-> [Authorize] attribute secures all endpoints. (except login/confirm-email)

-> Email confirmation required before login.

##  How to run

-> Clone the repository

-> Configure your database & email keys in appsettings.json

-> Run migrations:

   dotnet ef database update
  
-> Run the app:

   dotnet run
  
-> Test using Postman or Swagger

## Contact
Developed by : Muaaz Shoaib
Email: muhammad.muaaz@hazelsoft.org
GitHub: github.com/MuaazShoaib-HazelSoft/MuaazShoaib-ProbationAssignments
