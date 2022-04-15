using Microsoft.AspNetCore.Mvc;

namespace WalletProxy.Controller
{
    [ApiController]
    public class WalletController
    {
        private readonly IRpcClient rpcClient;

        public WalletController(IRpcClient rpcClient)
        {
            this.rpcClient = rpcClient;
        }

        [HttpGet, Route("/difficulty")]
        public async Task<DifficultyResponse> GetDifficulty()
        {
            return await rpcClient.GetDifficulty();
        }

        [HttpGet, Route("/block/count")]
        public async Task<uint> GetBlockCount()
        {
            return await rpcClient.GetBlockCount();
        }
        
        [HttpGet, Route("/block/{index:long}")]
        public async Task<string> GetBlockHash(long index)
        {
            return await rpcClient.GetBlockHash(index);
        }

        [HttpGet, Route("/block/hash/{hash}")]
        public async Task<BlockResponse> GetBlock(string hash)
        {
            return await rpcClient.GetBlock(hash);
        }
        
        [HttpGet, Route("/transaction/raw/{txId}")]
        public async Task<RawTransactionResponse> GetRawTransaction(string txId)
        {
            return await rpcClient.GetRawTransaction(txId);
        }

        [HttpGet, Route("/transaction/decode/{transaction}")]
        public async Task<DecodeRawTransactionResponse> DecodeRawTransaction(string transaction)
        {
            return await rpcClient.DecodeRawTransaction(transaction);
        }
        
        [HttpGet, Route("/listunspents")]
        public async Task<List<Unspent>> GetUnspents()
        {
            return await rpcClient.GetUnspents();
        }

    }
}
