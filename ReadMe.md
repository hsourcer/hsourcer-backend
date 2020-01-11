[![Build status](https://dev.azure.com/HsourcerDevops/HSourcer/_apis/build/status/hsourcerapp%20-%20CI)](https://dev.azure.com/HsourcerDevops/HSourcer/_build/latest?definitionId=1)

## HSourcer - application for absences managament

Application was created in clean architecure design. Well, at least most of it and at the beginning.
### Functionality (CRUD-like)
* users,
* teams,
* organization,
* absences.
### Services
* emails sending, (cshtml -> Azure sendGrid),
* user manager, (EF + identity core)
* JWT tokens,
* CRON scheduler,

DB was MSSQL hosted on Azure, but should work with most relational databases.

The solution was integrated with Azure Pipeline, with some tests.

Architecture was based on:
https://github.com/JasonGT/NorthwindTraders

CRON
https://github.com/pgroene/ASPNETCoreScheduler

Razor to html:
https://github.com/scottsauber/RazorHtmlEmails
