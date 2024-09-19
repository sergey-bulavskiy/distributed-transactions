$ContainerName = "docker.io/sergeybulavskiy/patients-frontend-app:latest"
$ImageName = "patients-frontend-app"

docker build -t $ImageName -f Dockerfile ../
docker tag $ImageName $ContainerName
docker push $ContainerName