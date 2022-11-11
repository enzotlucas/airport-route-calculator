# Airport route calculator
A system that allows you to create plane routes and calculate the best cost one

## Preparing the environment
Follow the steps to be able to run the application.

### Docker
To run the project, run the command bellow:
```bash
# If is your first time using, exec:
docker-compose up --build

# If you used at least one time, just:
docker-compose up
```

The project will open the Swagger on localhost:5001/swagger/index.html

### Database:
In this project, we use Migrations with EF Core. The database will be created at the first time you run the application.