#load "./.paket/load/main.group.fsx"

type Operands = {
    left: int
    right: int
}

type Result = {
    result: int
}

let operandAction f o = {result = f o.left o.right }
