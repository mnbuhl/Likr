# Likr - Social Media
Likr is a social media developed with microservice technologies for my self chosen 4th Semester Computer Science exam.

Project can be viewed and used in real time here: [TODO]

### Technologies used:
Client service
* Blazor
* ASP.NET Core
* Tailwind CSS

Posts service
* ASP.NET Core Web API
* SQL Server
* MassTransit w/RabbitMQ

Comments service
* ASP.NET Core Web API
* RavenDB
* MassTransit w/RabbitMQ

Likes service
* ASP.NET Core Web API
* SQL Server
* MassTransit w/RabbitMQ

Identity service
* ASP.NET Core Web API
* IdentityServer4
* SQL Server

Gateway service
* ASP.NET Core Web App
* Ocelot API Gateway

Orchestration
* Docker & Docker Compose

### Useful documentation:

#### Docker commands:

Run: ```docker compose up -d```

Build & run:
``` docker compose up -d --build ```

Rebuild: ```docker compose up -d --build --force-recreate```

Run deployment:  
```docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d```

Stop: ```docker compose down```

#### Images

![Homepage](https://i.ibb.co/j5rD1Xr/Screenshot-2021-12-01-163316.png)
![Post-page](https://i.ibb.co/NN1Dpps/Screenshot-2021-12-01-163421.png)
![Profile-page](https://i.ibb.co/JsFqNYQ/Screenshot-2021-12-01-163455.png)
![Identity-server-login](https://i.ibb.co/p4x7GtX/Screenshot-2021-12-01-163523.png)