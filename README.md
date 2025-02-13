This project is a web application named "MyAuto" built using ASP.NET Core Razor Pages. It is designed to manage a car inventory, allowing users to add, edit, view, and delete car listings. The application uses Entity Framework Core with a SQLite database to store car data, including details like manufacturer, model, price, year, engine, transmission, fuel type, and photos.

Key components:

Data Layer: Contains the AppDbContext class for database context and migration files for database schema.
Services Layer: Includes the ICARRepository interface and its implementation SqlCarRepository for data operations.
Pages: Razor Pages for various functionalities like listing cars, searching, viewing details, editing, and deleting car entries.
Static Files: CSS and JavaScript files for styling and client-side functionality.
The project is configured to run in a development environment with detailed error logging and supports both HTTP and HTTPS.

https://github.com/user-attachments/assets/a9683744-2294-4c66-a0fa-ec6a8953c579

