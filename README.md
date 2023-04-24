# Proj3 REST API

+ [Rest API (ASP.NET 6)](#rest-api)
+ [Requirements](#requirements)
+ [Run Project](#run-project)
+ [Features](#features)
    - [Auth](#auth)
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
            - [Change Password Response](#change-password-response)
        - [Email Confirmation](#email-confirmation)
            - [Email Confirmation Request](#email-confirmation-request)    
            - [Email Confirmation Response](#email-confirmation-response)                   

        
# Requirements
    
- [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download)

# Run Project
    
- Enter the project directory and run the command:``` > dotnet run --project .\Proj3.Api\```

<br>

# Features

<br>

## Auth

### SignUp Ngo

```js
POST {{host}}/auth/signup-ngo
```

#### SignUp Ngo Request

```json
{
    "firstName": "Foo",
    "lastName": "Bar",
    "email": "foobar@email.com",
    "password": "Passwd#FooBar42",
}
```

#### SignUp Ngo Response

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