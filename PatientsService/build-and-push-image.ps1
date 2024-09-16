$ContainerName = "docker.io/sergeybulavskiy/patients-service:latest"
$ImageName = "patients-service"

docker build -t $ImageName -f Dockerfile .
docker tag $ImageName $ContainerName
docker push $ContainerName