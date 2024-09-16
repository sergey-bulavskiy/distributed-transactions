$ContainerName = "docker.io/sergeybulavskiy/patients-service:latest"

docker build -t patients-service -f Dockerfile .
docker tag patients-service ContainerName
docker push ContainerName