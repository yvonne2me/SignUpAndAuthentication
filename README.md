SIGNUP
------

POST /api/signup
- Use this endpoint and HTTP method to sign up & create a new user

Sample Request Message:

{
	"name" : "TestUser",
	"email" : "testuser@test.com",
	"password" : "password",
	"telephones" : [
		{
			"number" : "12345"
		}
	]
}

Sample Response Message:

{
    "id": "6a207ae1-929f-45fe-982d-41a6259e25f9",
    "createdOn": "2018-04-26T21:46:30.102125Z",
    "lastUpdatedOn": "2018-04-26T21:46:30.102285Z",
    "lastLoginOn": "2018-04-26T21:46:30.102208Z",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1MjQ3ODA5OTAsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAvIn0.yNZeGJXgsOYGx5a5kzgLzXr2qAp_tgBSx3975YgBGHk"
}

SIGNIN
------

POST /api/signin
- Use this endpoint and HTTP Method to sign in

Sample Request Message:

 {
 	"email": "testuser@test.com",
 	"password": "password"
}

Sample Response Message:

{
    "id": "6a207ae1-929f-45fe-982d-41a6259e25f9",
    "createdOn": "2018-04-26T21:46:30.102125Z",
    "lastUpdatedOn": "2018-04-26T21:46:30.102285Z",
    "lastLoginOn": "2018-04-26T21:46:38.073761Z",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1MjQ3ODA5OTgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAvIn0.Fxbf8zslJqbzVDMv_N56-ek6LU8bxEr_hhnGvfM2aGE"
}

AUTHENTICATION
--------------

The Search Endpoint requires Authentication.

Authentication involves sending the Token value from a SignUp / SignIn request
The Token will remain valid for 30 mins, after that you will have to SignIn to request a new one.

Example Authentication Header:

Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1MjQ3ODA5OTgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAvIn0.Fxbf8zslJqbzVDMv_N56-ek6LU8bxEr_hhnGvfM2aGE



SEARCH
------

GET /api/search?userId={userId}
- Use this endpoint and HTTP Method to search for a specific User

**This endpoint requires Authentication **

No request body in a GET request. 
Example URI: /api/search?userId=5a207ae1-929f-45fe-982d-41a6259e25f9

Sample Response Message:

{
    "id": "227db491-275c-4b7d-a7b8-a251396d989b",
    "name": "TestUser2",
    "email": "yvonne2@test.com",
    "password": "6cf615d5bcaac778352a8f1f3360d23f02f34ec182e259897fd6ce485d7870d4",
	"telephones" : [
		{
			"number" : "12345"
		}
	]
}






