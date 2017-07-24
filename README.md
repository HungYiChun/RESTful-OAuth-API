First
You need to build the DB chart as shown below
![image](https://github.com/HungYiChun/RESTful-OAuth-API/blob/master/User_design.png?raw=true)

Second
Compiled via Visual Studio


Use POSTMAN to verify

Get OAuth authentication Token:
POST /token

Headers :

            {
                "Content-Type" : "application/x-www-form-urlencoded"
            }
        
Body :

            { 
                "grant_type" : "password", 
                "username" : "{username}",
                "password" : "{password}"
            }
        
return :

            {
                "access_token": "{token}",
                "token_type": "bearer",
                "expires_in": 1800
            }
        
Use token to get your profile:
GET api/UserSelf

Headers :

            {
                "Authorization" : "bearer {token}"
            }
        
return :

            {
                "Id": "{Id}",
                "Email": "{Email}",
                "Username": "{Username}",
                "Password": "{Password}",
                "Name": "{Name}",
                "PhoneNumber": "{PhoneNumber}",
                "EditTime": "{EditTime}",
                "RoleName": "{RoleName}",
                "StatusName": "{StatusName}"
            }
        
        
More APIs at / Help
