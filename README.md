# TableTendersAPIService

* О сервисе. 

Веб-сервис REST, получающий файл Exel из POST запроса и выдающий их вызывающему в формате JSON. Тип аутентификации – анонимная.

* Используемые технологии

Сервис работает на основе ASP NET Core. Для хранения данных пользователей и данных из Exel файла базы данных MS SQL, для работы с ними ипользую расширения Microsoft.AspNetCore.Identity.EntityFrameworkCore (работа с базой пользователей) и Microsoft.EntityFrameworkCore.SqlServe (работа с базой данных, полученых из Exel файла). Для чтения данных из Exel файла использовал EPPlus.

* Методы доступа.
 
GET api/tender
Если данные в БД обнаружены, выдает JSON файл со всеми данными, в случае отсутвия данных - возращает пустой JSON файл.
POST api/tender
Выполняет загрузку файла на сервер. После загрузки выполняет чтение данных из файла и сохранение их в БД.

* Особености работы.
 
При запуске очищает собственную БД и удаляет загруженый файл.

* Коментарий автора.

Для тестирования отправки файла в сервис использовал Postman.
