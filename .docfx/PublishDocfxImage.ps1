$version = minver -i -t v -v w
docker tag bootstrapper-docfx:$version jcr.codebelt.net/geekle/bootstrapper-docfx:$version
docker push jcr.codebelt.net/geekle/bootstrapper-docfx:$version
