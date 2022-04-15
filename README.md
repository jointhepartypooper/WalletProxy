
## WalletProxy


This is a aspnet6.0 webservice that act as a REST endpoint for peercoin wallet.

Your peercoin.conf should have the following config:
listen=1
server=1
 
testnet=0
debug=1
txindex=1
rpcuser=helloiamaproxy
rpcpassword=YcF7!&93YTs2Nhc@CJf
rpcport=8332


## How to compile app:
dotnet publish WalletProxy.csproj -c Release --runtime linux-x64 --no-self-contained

## How to run:
either use Dockerfile (Todo: set root to: /bin/Release/net6.0/linux-x64/publish )
or 
compile and run WalletProxy.exe

## endpoints:
http://127.0.0.1:9009/ping
http://127.0.0.1:9009/difficulty
http://127.0.0.1:9009/block/count
http://127.0.0.1:9009/block/{index:long}
http://127.0.0.1:9009/block/hash/{hash}
http://127.0.0.1:9009/transaction/raw/{txId}
http://127.0.0.1:9009/transaction/decode/{transaction}
http://127.0.0.1:9009/listunspents
 