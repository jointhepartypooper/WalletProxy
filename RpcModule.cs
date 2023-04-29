using Carter;

namespace WalletProxy;

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

        app.MapPost("/transaction/raw/decode", async (RawTransaction message, IRpcClient rpcClient) =>
            await rpcClient.DecodeRawTransaction(message?.rawtransaction ?? message?.Transaction ?? "")
        ).Produces<DecodeRawTransactionResponse>(StatusCodes.Status200OK);

        app.MapGet("/transaction/raw/{txId}", async (string txId, IRpcClient rpcClient) =>
            await rpcClient.GetRawTransaction(txId)
        ).Produces<RawTransactionResponse>(StatusCodes.Status200OK);
        
        app.MapPost("/transaction/raw/coinstake", async (CoinStakeTransaction message, IRpcClient rpcClient) =>
            {
                var result = await rpcClient.CreateRawCoinStakeTransaction(new List<RawTxStakeInputs>
                    {
                        new RawTxStakeInputs
                        {
                            redeemScript = message.redeemScript,
                            txid = message.txid,
                            vout = message.vout,
                        }
                    },
                    new List<RawTxStakeOutput>
                    {
                        new RawTxStakeOutput(message.minterPubkey, (double) (message.minterReward ?? 0)),
                        new RawTxStakeOutput(message.address, (double) message.futureOutput)
                    },
                    message.futureTimestamp);

                return result;
            }
        ).Produces<string>(StatusCodes.Status200OK);
        
        //obsolete as transaction might get to long:
        app.MapGet("/transaction/decode/{transaction}", async (string transaction, IRpcClient rpcClient) =>
            await rpcClient.DecodeRawTransaction(transaction)
        ).Produces<DecodeRawTransactionResponse>(StatusCodes.Status200OK);

        //obsolete as importaddress is needed 
        app.MapGet("/listunspents", async (IRpcClient rpcClient) =>
            await rpcClient.GetUnspents()
        ).Produces<List<Unspent>>(StatusCodes.Status200OK);
    }

    // ReSharper disable All
    public class RawTransaction
    {
        //old interface
        public string? Transaction { get; set; }

        public string? rawtransaction { get; set;}
    }

    public class CoinStakeTransaction
    {
        //unspent tx
        public string txid { get; set; } = null!;

        // index of unspent
        public int vout { get; set; }

        // to spent with multisig address
        public string redeemScript { get; set; } = null!;

        // to spent P2SH address
        public string address { get; set; } = null!;
        
        // original input + stakereward
        public decimal futureOutput { get; set; }

        // unix time in seconds
        public long futureTimestamp { get; set; }

        // pubkey of bob the builder
        public string minterPubkey { get; set; } = null!;

        // reward for bob
        public decimal? minterReward { get; set; }
    }
    // ReSharper restore All
}