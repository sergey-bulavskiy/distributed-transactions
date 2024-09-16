$ContainerName = "docker.io/sergeybulavskiy/medical-data-service:latest"

docker pull $ContainerName
docker rm $(docker stop $(docker ps -a -q --filter ancestor=$ContainerName --format="{{.ID}}"))
docker run -d -t --name medical-data-service -p 6000:6000 $ContainerName