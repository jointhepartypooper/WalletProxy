
## WalletProxy


This is a aspnet6.0 webservice that act as a REST endpoint for peercoin wallet.

Your peercoin.conf should have the following config:
```
listen=1
server=1
 
testnet=0
debug=1
txindex=1
rpcuser=helloiamaproxy
rpcpassword=YcF7!&93YTs2Nhc@CJf
rpcport=8332
```
The username and password can be changed accordingly in settings.json

## How to compile app:
dotnet publish WalletProxy.csproj -c Release --runtime linux-x64 --no-self-contained

(or use self-contained if there isnt a runtime net 6 installed)

## How to run:
~~either use Dockerfile~~ (Todo: set root to: /bin/Release/net6.0/linux-x64/publish and setup ports etc)

or 

WalletProxy.exe 

or 

./WalletProxy (might need to chmod 777 first)

## endpoints:
port 9009 is hardcoded, todo: add port to settings.json
```
http://127.0.0.1:9009/ping
http://127.0.0.1:9009/difficulty
http://127.0.0.1:9009/block/count
http://127.0.0.1:9009/block/{index:long}
http://127.0.0.1:9009/block/hash/{hash}
http://127.0.0.1:9009/transaction/raw/{txId}
http://127.0.0.1:9009/transaction/decode/{transaction}
http://127.0.0.1:9009/listunspents
``` 