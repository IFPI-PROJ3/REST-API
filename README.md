# Proj3 REST API

+ [Rest API (ASP.NET 6)](#rest-api)
+ [Requirements](#requirements)
+ [Run Project](#run-project)
+ [Features](#features)
    + [Auth](#auth)
        - [SignUp](#signup)
            - [SignUp Request](#signup-request)
            - [SignUp Response](#signup-response)
        - [SignIn](#signin)
            - [SignIn Request](#signin-request)
            - [SignIn Response](#signin-response)
        - [Refresh Token](#refresh-token)
            - [Refresh Token Request](#refresh-token-request)
            - [Refresh Token Response](#refresh-token-response)
        - [Change Password](#change-password)
            - [Change Password Request](#change-password-request)
            = [Change Password Response](#change-password-response)
        - [Email Confirmation](#email-confirmation)
            - [Email Confirmation Request](#email-confirmation-request)    
            - [Email Confirmation Response](#email-confirmation-response)
        - [Add Phone Number](#phone-number)
            - [Add Phone Number Request](#phone-number-request)
            - [Add Phone Number Response](#phone-number-response)
        - [Phone Number Confirmation](#phone-number-confirmation)
            - [Phone Number Confirmation Request](#phone-number-confirmation-request)
            - [Phone Number Confirmation Response](#phone-number-confirmation-response)

    + [NGO]
    + [Volunteer]

+ [Directory structure](#directory-structure)

        
# Requirements
    
- [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download)

# Run Project
    
- Entrar no diretório do projeto e executar o comando:``` > dotnet run --project .\Proj3.Api\```

<br>

# Features

<br>

## Auth

### SignUp

```js
POST {{host}}/auth/SignUp
```

#### SignUp Request

```json
{
    "firstName": "Foo",
    "lastName": "Bar",
    "email": "foobar@email.com",
    "password": "Passwd#FooBar42",
}
```

#### SignUp Response

```js
200 OK
```

```json
{
    "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
    "firstName": "Foo",
    "lastName": "Bar",
    "email": "foobar@email.com",
    "password": "Passwd#FooBar42",
    "token": "eyJhb..hbbQ"
}
```

### SignIn

```js
POST {{host}}/auth/SignIn
```

#### SignIn Request

```json
{        
    "email": "Foo@Bar.com",
    "password": "Passwd#FooBar42",    
}
```

#### SignIn Response

```js
200 OK
```

```json
{
  "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
  "firstName": "Foo",
  "lastName": "Bar",
  "email": "foobar@email.com",
  "token": "eyJhb..hbbQ"
}
```

<br>

# Directory structure

**📦Proj3Api**
 ┃ ┃
**┣ 📂Proj3.Api**
 ┃ ┣ 📂Controllers
 ┃ ┃ ┣ 📂Authentication
 ┃ ┣ 📂Middlewares
 ┃ ┃ ┣ 📂Authentication
 ┃ ┣ 📂Properties
 ┃ ┃
**┣ 📂Proj3.Application**
 ┃ ┣ 📂Common
 ┃ ┃ ┣ 📂Errors
 ┃ ┃ ┃ ┣ 📂Authentication
 ┃ ┃ ┗ 📂Interfaces
 ┃ ┃ ┃ ┣ 📂Persistence
 ┃ ┃ ┃ ┃ ┣ 📂Authentication
 ┃ ┃ ┃ ┣ 📂Services
 ┃ ┃ ┃ ┃ ┣ 📂Authentication
 ┃ ┃ ┃ ┃ ┃ ┣ 📂Commands
 ┃ ┃ ┃ ┃ ┃ ┗ 📂Queries
 ┃ ┃ ┃ ┗ 📂Utils
 ┃ ┃ ┃ ┃ ┗ 📂Authentication
 ┃ ┣ 📂Services
 ┃ ┃ ┗ 📂Authentication
 ┃ ┃ ┃ ┣ 📂Commands
 ┃ ┃ ┃ ┣ 📂Queries
 ┃ ┃ ┃ ┗ 📂Result
 ┃ ┣ 📂Utils
 ┃ ┃ ┗ 📂Authentication
 ┃ ┃
**┣ 📂Proj3.Contracts**
 ┃ ┣ 📂Authentication
 ┃ ┃ ┣ 📂Request
 ┃ ┃ ┗ 📂Response
 ┃ ┃
**┣ 📂Proj3.Domain**
 ┃ ┣ 📂Entities
 ┃ ┃ ┗ 📂Authentication
 ┃ ┃
**┣ 📂Proj3.Infrastructure**
 ┃ ┣ 📂Authentication
 ┃ ┃ ┣ 📂Utils
 ┃ ┣ 📂Database
 ┃ ┃ ┣ 📂SQLite
 ┃ ┃ ┃ ┣ 📂AppMigrations
 ┃ ┃ ┣ 📂Utils
 ┃ ┣ 📂Repositories
 ┃ ┃ ┣ 📂Authentication
 ┃ ┗ 📂Services