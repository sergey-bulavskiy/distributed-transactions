$ContainerName = "docker.io/sergeybulavskiy/patients-service:latest"

docker pull $ContainerName
docker rm $(docker stop $(docker ps -a -q --filter ancestor=$ContainerName --format="{{.ID}}"))
docker run -d -t --name patients-service -p 5000:5000 $ContainerName