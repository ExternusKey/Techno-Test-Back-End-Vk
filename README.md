# Профильное задание на позицию стажера "Back-End Developer"
## _Стажировка Вконтакте_
Условия задачи:  
Необходимо разработать упрощенный сервис с регистрацией и авторизацией.
Хранить данные можно в любой реляционной СУБД.

Сервис должен иметь 3 апи-метода (REST):
1. /register
    - Входные параметры: string email, string password.
    - Необходимо проверить, что пользователя с таким email еще нет в базе и email валидный.
    - Также нужно добавить проверку надежности пароля - если пароль легко подобрать, выкидывать ошибку weak_password.
    - На выходе возвращать int user_id, string password_check_status (good, perfect).
2. /authorize
    - Входные параметры: string email, string password.
    - В ответе - _string acces_token JWT_, в котором будет лежать user_id.
3. /feed
    - Идем в метод с access_token.
    - Возвращаем 200 код, если токен валиден, и выкидываем ошибку unauthorized, если нет.
      
***
## _Инструкция_
Установновленные NuGet пакеты:
   ```
   dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Design
   ```
В качестве СУБД был выбран локальный MSSQL Server. База данных создаётся при первом запросе (регистрация).  
Docker контейнер присутствует (AuthServicePublished). 
***
## _Примеры запросов_
В качестве приложения для тестирования API-запросов был выбран Postman.  
При запуске приложение через контейнер порт соответствует 5000. При запуске через IDE - выбирается случайно.  
1. Регистрация нового пользователя
   - Метод: POST
   - URL: ```http://localhost:5000/register```
   - Тело запроса (form-data):
     ```json
        {
          "Email": "example@example.com",
          "Password": "password123"
        }
     ```
2. Аутентификация пользователя
   - Метод: POST
   - URL: ```http://localhost:5000/authorize```
   - Тело запроса (form-data):
     ```json
        {
          "Email": "example@example.com",
          "Password": "password123"
        }
     ```
3. Проверка авторизации
   - Метод: GET
   - URL: ```http://localhost:5000/feed```
   - Заголовок Authorization: Bearer {AccessToken}
   - {AccessToken} - токен доступа, полученный после аутентификации.
