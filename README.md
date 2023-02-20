# JWTAuthAPI

Leveraging the power of ASP.NET Core, this simple project features an example of a secure API with JWT Authentication, Entity Framework for data manipulation, Specification Pattern for querying complex criteria, and a Service/Repository/Controller pattern for code organization. Additionally, the API has an authorization handler to restrict modifications to resource owners only.

## Features
* JWT Authentication: Implements a secure authentication system with JSON Web Tokens (JWT) for enhanced security.
* Entity Framework: Uses Entity Framework to simplify data access and manipulate data from the database.
* Specification Pattern: Uses the specification pattern to query the database with complex criteria to retrieve entities that match the criteria.
* Service/Repository/Controller pattern: Uses the Service/Repository/Controller pattern to improve code organization and maintainability.
* Authorization Handler: Forbid users who are not owners from modifying resources to improve data security.


## Usage
This API provides the following endpoints:

* POST /api/v1/accounts/login
* POST /api/v1/accounts/register

[Secure Endpoints]
* GET /api/v1/accounts/users/id
* PUT /api/v1/accounts/users/id
* DELETE /api/v1/accounts/users/id 

* POST /api/v1/products/
* GET /api/v1/products/id
* PUT /api/v1/products/id
* DELETE /api/v1/products/id 

You can use the following HTTP methods to interact with these endpoints:

* GET: retrieve data
* POST: create new data
* PUT: update existing data
* DELETE: delete data

Make sure to include a valid JWT token in the Authorization header when making requests to the Secure Endpoints. You will receive the token once you have registered the user and have logged in!