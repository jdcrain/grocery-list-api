# Grocery List API

## Description
A simple API that allows users to create and update a grocery list. Built using .Net Core and Postgres. The API uses JWTs for authentication and uses the claims to manage if actions can be taken by users depending on if the grocery list belongs to them.

## Prerequisites
The following items should be installed
- pgAdmin4 (for Postgres)
- dotnet cli
- Create a user in Postgres with the credentials specified in appsettings.json (or simply change the User Id and Password values to whichever user you prefer)

Run the following before executing the code
- dotnet ef database update

## Notes
Postman collection can be found here for manual testing https://www.getpostman.com/collections/4429907557522181e782

I opted to just simply do bulk updates to the grocery list instead of adding and updating individual list items. For large lists this would be a little more inefficient with the amount of data that is being sent, but it avoids muddling the overall list state on the UI and allows the UI to be overall more simplistic.
