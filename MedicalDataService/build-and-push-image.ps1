$ContainerName = "docker.io/sergeybulavskiy/medical-data-service:latest"
$ImageName = "medical-data-service"

docker build -t $ImageName -f Dockerfile ../
docker tag $ImageName $ContainerName
docker push $ContainerName