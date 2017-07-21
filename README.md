取OAuth認證Token
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
        
使用token取得個人資料
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
        
