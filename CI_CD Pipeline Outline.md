
# Simple CI/CD Pipeline

## Pipeline Steps

### 1. Build Application

Restore dependencies and compile the solution.

```bash
dotnet restore
dotnet build --configuration Release
```

### 2. Run Tests

Execute unit tests.

```bash
dotnet test
```

### 3. Build Docker Image

Create a deployable container image.

```bash
docker build -t transaction-webhook-api .
```

### 4. Push Image to Registry

Push the image to Amazon Elastic Container Registry (ECR).

```bash
docker tag transaction-webhook-api:latest <ecr-repository-url>

docker push <ecr-repository-url>
```

### 5. Deploy to AWS

Update the ECS Fargate service to use the latest image.

Deployment process:

1. New image pushed to ECR.
2. ECS service detects new task definition.
3. ECS performs rolling deployment.
4. Health checks validate new containers.
5. Old containers are terminated after successful deployment.

## Deployment Flow

Developer
→ Git Repository
→ CI Pipeline (Build + Test)
→ Docker Image
→ Amazon ECR
→ ECS Fargate
→ Amazon RDS PostgreSQL
→ CloudWatch Monitoring
