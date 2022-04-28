var builder = WebApplication.CreateBuilder(args);
            
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IRpcClient>(new RPCClient(new StaticRestClientFactory()));

await using var app = builder.Build();

app.UseCors();

//config endpoints:
//app.MapGet("/", () => "Hello! This is .NET 6 Minimal API App Service").ExcludeFromDescription();
app.MapGet("/ping", () => "pong").ExcludeFromDescription();

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

app.MapGet("/transaction/raw/{txId}", async (string txId, IRpcClient rpcClient) =>
    await rpcClient.GetRawTransaction(txId)
).Produces<RawTransactionResponse>(StatusCodes.Status200OK);

app.MapGet("/transaction/decode/{transaction}", async (string transaction, IRpcClient rpcClient) =>
    await  rpcClient.DecodeRawTransaction(transaction)
).Produces<DecodeRawTransactionResponse>(StatusCodes.Status200OK);

app.MapGet("/listunspents", async (IRpcClient rpcClient) =>
    await rpcClient.GetUnspents() 
).Produces<List<Unspent>>(StatusCodes.Status200OK);



//Run the application.
app.Run("http://127.0.0.1:9009");

