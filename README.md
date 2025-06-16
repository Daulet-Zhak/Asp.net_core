Используйте postman, регистрируйтесь через URL http://localhost:5228/api/auth/register POST запросом. Затем, получите jwt токен через URL http://localhost:5228/api/auth/login.
Используйте этот токен в меню Authorization и выберите Bearer Token.
Теперь вы сможете использовать этот токен для GET, POST, PUT, DELETE запросов через URL http://localhost:5228/tasks/Task.
