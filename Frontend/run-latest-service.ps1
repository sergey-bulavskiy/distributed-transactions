$ContainerName = "docker.io/sergeybulavskiy/patients-frontend-app:latest"
$ImageName = "patients-frontend-app"

docker pull $ContainerName
docker rm $(docker stop $(docker ps -a -q --filter ancestor=$ContainerName --format="{{.ID}}"))
docker run -d -t --name $ImageName -p 5500:8080 $ContainerName