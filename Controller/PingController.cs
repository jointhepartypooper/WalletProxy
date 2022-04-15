using Microsoft.AspNetCore.Mvc;

namespace WalletProxy.Controller
{
    [ApiController]
    public class PingController
    {
        [HttpGet, Route("/ping")]
        public string HelloWorld()
        {
            return "pong!";
        }
    }
}
