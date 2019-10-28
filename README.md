(http://www.worldgolfhalloffame.org/wp-content/uploads/2013/03/PING.png)
# Pinger

Простая программа для опроса заданных хостов, доступно три протокола, **ICMP**, **HTTP** и **TCP**.

По умолчанию, файл со списком хостов для опроса это обычный текстовый документ с названием **Hosts.txt**, лежащий в папке с программой.

Настройки представляют с собой статическую конфигурацию .NET Core и находятся в **Settings.cs**.
Дополнительные пути для сохранения логов можно задать в **Loger.cs**.

*Если файл с настройками удален, программа сгенерирует его при первом запуске, настройки в нем будут по умолчанию.*

**Настройки:**

- Rowhostpath - Путь к файлу с хостами.
(По умрочанию: ./Hosts.txt).

- Logpath - Путь к файлу, в который будут писаться логи.
(По умолчанию ./Logs.txt)

- Protocol - Протокол опроса.
Доступные значения:
1. ICMP - В файле с хостами должны быть IP адреса.
2. HTTP - В файле с хостами должны быть DNS имена, обращение происходит к 80 порту.
3. TCP - В файле с хостами должны быть IP адреса.

**(ВНИМАНИЕ! Только HTTP протокол обращается к DNS именам, остальные протоколы используют обращение напрямую к IP адресу!)**

- Settingspath - Указание пути к файлу с настройками, необходимо для генерации файла после его удаления.

- Period - Период между опросами.

- Httpvalidcode - Если выбран протокол HTTP, программа будет считать именно данный код валидным ответом сервера.

  
