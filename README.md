# suave-swagger-demo
short demo on using swagger with suave

To build:

``` bash
mono ./.paket/paket.bootstrapper.exe
mono .paket/paket.exe restore
mono .paket/paket.exe generate-include-scripts
```

To run basic api:

``` bash
fsharpi basic.fsx
```

To call api:
``` bash
curl --data '{"left":3,"right":4}' http://127.0.0.1:8080/add
curl --data '{"left":3,"right":4}' http://127.0.0.1:8080/multiple
```