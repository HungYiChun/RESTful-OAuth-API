First
git clone this project.

Second
run this project by Visual studio (build vs2015).

HomePage:
![image](https://github.com/HungYiChun/RESTful-OAuth-API/blob/master/RESTful_API_Homepage.png?raw=true)

DB Design:
![image](https://github.com/HungYiChun/RESTful-OAuth-API/blob/master/DB_Design.png?raw=true)

Postman Result:
![image](https://github.com/HungYiChun/RESTful-OAuth-API/blob/master/Postman_result.png?raw=true)

PS:
First time POST will automatically create Database, So it takes a little time.



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
