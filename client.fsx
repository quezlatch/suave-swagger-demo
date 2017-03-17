#load "./.paket/load/swaggerprovider.fsx"
open SwaggerProvider
let [<Literal>] Schema = "http://localhost:8080/swagger/v2/swagger.json"
type OperandClient = SwaggerProvider<Schema>

let o = OperandClient.Operands(Left = Some 4 ,Right = Some 4 )
let result = OperandClient(Host = "http://localhost:8080").PostAdd(o).Result

printfn "result is %A" result