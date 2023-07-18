# In-memory DB
The chosen in-memory database is Redis, although it is used more as a cache, it can also be used as a primary database that persists on disk, depending on the configuration applied.

### Local URLs
redis insight: http://localhost:8001

### Prepare environment
- The first thing we need to do is install Docker, if it's not already installed. There's a lot of documentation available depending on the OS that is going to be used.
- Create the Docker volumes using `docker volume create redis_volume`
- Use `docker compose up -d` to run the Docker images and start the container defined in the yml file located in the `_localstack` folder.
- In the same `_localstack` folder there is a collection in Postman to test CRUD operations.
