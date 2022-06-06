using Carter;
 
public class RpcModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/difficulty", async (IRpcClient rpcClient) =>
            await rpcClient.GetDifficulty() 
        ).Produces<DifficultyResponse>(StatusCodes.Status200OK);

        app.MapGet("/block/count", async (IRpcClient rpcClient) =>
            await rpcClient.GetBlockCount() 
        ).Produces<uint>(StatusCodes.Status200OK);

        app.MapGet("/block/{index:long}", async (long index, IRpcClient rpcClient) =>
            await rpcClient.GetBlockHash(index)
        ).Produces<string>(StatusCodes.Status200OK);
        
        app.MapGet("/block/hash/{hash}", async (string hash, IRpcClient rpcClient) =>
            await rpcClient.GetBlock(hash)
        ).Produces<BlockResponse>(StatusCodes.Status200OK);

        app.MapPost("/transaction/raw/decode", async (RawTransaction message, IRpcClient rpcClient)=>
                await rpcClient.GetRawTransaction(message?.transaction??"")
        ).Produces<DecodeRawTransactionResponse>(StatusCodes.Status200OK);
        
        app.MapGet("/transaction/raw/{txId}", async (string txId, IRpcClient rpcClient) =>
            await rpcClient.GetRawTransaction(txId)
        ).Produces<RawTransactionResponse>(StatusCodes.Status200OK);

        //obsolete as transaction might get to long:
        app.MapGet("/transaction/decode/{transaction}", async (string transaction, IRpcClient rpcClient) =>
            await  rpcClient.DecodeRawTransaction(transaction)
        ).Produces<DecodeRawTransactionResponse>(StatusCodes.Status200OK);

        //obsolete as importaddress is needed 
        app.MapGet("/listunspents", async (IRpcClient rpcClient) =>
            await rpcClient.GetUnspents() 
        ).Produces<List<Unspent>>(StatusCodes.Status200OK);
    }

    public class RawTransaction
    {
            public string? transaction{get;set;}
    }

}