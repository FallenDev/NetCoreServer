# NetServer

[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE) [![.NET)](https://github.com/FallenDev/NetServer/actions/workflows/dotnet.yml/badge.svg)](https://github.com/FallenDev/NetServer/actions/workflows/dotnet.yml)
<br/>
[![Windows](https://github.com/FallenDev/NetServer/actions/workflows/build-windows.yml/badge.svg)](https://github.com/FallenDev/NetServer/actions/workflows/build-windows.yml) 
[![Linux](https://github.com/FallenDev/NetServer/actions/workflows/build-linux.yml/badge.svg)](https://github.com/FallenDev/NetServer/actions/workflows/build-linux.yml) 
[![MacOS](https://github.com/FallenDev/NetServer/actions/workflows/build-macos.yml/badge.svg)](https://github.com/FallenDev/NetServer/actions/workflows/build-macos.yml) 


This is a reduced version of [NetCoreServer](https://github.com/chronoxor/NetCoreServer) utilizing C#13's capabilities. It retains NetCoreServer's known fast and low latency async server / client logic, with support for TCP, SSL, UDP protocols. The reason I decided to reduce and optimize this already well built library, was due to wanting a library I can stick into Unity or Godot that is easy to manage and use. There are a lot of libraries out there that try to solve a problem by using proprietairy methods and classes. This is simple and uses pure .NET logic. Since this is written in .NET 9, it should still be compatible with Linux and Mac; However I'm not providing support for this library. 

Please see the Wiki above for examples, Enjoy :)

Solves the [10K connections problem](https://en.wikipedia.org/wiki/C10k_problem)

Has integration with high-level message protocol based on [Fast Binary Encoding](https://github.com/chronoxor/FastBinaryEncoding)

If you're interested in NetCoreServer written by Ivan Shynkarenka, follow the below link
[NetCoreServer documentation](https://chronoxor.github.io/NetCoreServer)<br/>

# Contents
  * [Features](#features)
  * [Requirements](#requirements)
  * [OpenSSL certificates](#openssl-certificates)
    * [Production](#production)
    * [Development](#development)
    * [Certificate Authority](#certificate-authority)
    * [SSL Server certificate](#ssl-server-certificate)
    * [SSL Client certificate](#ssl-client-certificate)
    * [Diffie-Hellman key exchange](#diffie-hellman-key-exchange)

# Features
* Cross platform (Linux, MacOS, Windows)
* Asynchronous communication
* Supported transport protocols: [TCP](#example-tcp-chat-server), [SSL](#example-ssl-chat-server),
  [UDP](#example-udp-echo-server), [UDP multicast](#example-udp-multicast-server)
* Supported [Swagger OpenAPI](https://swagger.io/specification/) iterative documentation
* Supported message protocol based on [Fast Binary Encoding](https://github.com/chronoxor/FastBinaryEncoding)

# Requirements
* Linux
* MacOS
* Windows
* [.NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* [Visual Studio](https://www.visualstudio.com)

Optional:
* [Rider](https://www.jetbrains.com/rider)

# OpenSSL certificates
In order to create OpenSSL based server and client you should prepare a set of
SSL certificates.

## Production
Depending on your project, you may need to purchase a traditional SSL
certificate signed by a Certificate Authority. If you, for instance,
want some else's web browser to talk to your WebSocket project, you'll
need a traditional SSL certificate.

## Development
The commands below entered in the order they are listed will generate a
self-signed certificate for development or testing purposes.

## Certificate Authority

* Create CA private key
```shell
openssl genrsa -passout pass:qwerty -out ca-secret.key 4096
```

* Remove passphrase
```shell
openssl rsa -passin pass:qwerty -in ca-secret.key -out ca.key
```

* Create CA self-signed certificate
```shell
openssl req -new -x509 -days 3650 -subj '/C=BY/ST=Belarus/L=Minsk/O=Example root CA/OU=Example CA unit/CN=example.com' -key ca.key -out ca.crt
```

* Convert CA self-signed certificate to PFX
```shell
openssl pkcs12 -export -passout pass:qwerty -inkey ca.key -in ca.crt -out ca.pfx
```

* Convert CA self-signed certificate to PEM
```shell
openssl pkcs12 -passin pass:qwerty -passout pass:qwerty -in ca.pfx -out ca.pem
```

## SSL Server certificate

* Create private key for the server
```shell
openssl genrsa -passout pass:qwerty -out server-secret.key 4096
```

* Remove passphrase
```shell
openssl rsa -passin pass:qwerty -in server-secret.key -out server.key
```

* Create CSR for the server
```shell
openssl req -new -subj '/C=BY/ST=Belarus/L=Minsk/O=Example server/OU=Example server unit/CN=server.example.com' -key server.key -out server.csr
```

* Create certificate for the server
```shell
openssl x509 -req -days 3650 -in server.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out server.crt
```

* Convert the server certificate to PFX
```shell
openssl pkcs12 -export -passout pass:qwerty -inkey server.key -in server.crt -out server.pfx
```

* Convert the server certificate to PEM
```shell
openssl pkcs12 -passin pass:qwerty -passout pass:qwerty -in server.pfx -out server.pem
```

## SSL Client certificate

* Create private key for the client
```shell
openssl genrsa -passout pass:qwerty -out client-secret.key 4096
```

* Remove passphrase
```shell
openssl rsa -passin pass:qwerty -in client-secret.key -out client.key
```

* Create CSR for the client
```shell
openssl req -new -subj '/C=BY/ST=Belarus/L=Minsk/O=Example client/OU=Example client unit/CN=client.example.com' -key client.key -out client.csr
```

* Create the client certificate
```shell
openssl x509 -req -days 3650 -in client.csr -CA ca.crt -CAkey ca.key -set_serial 01 -out client.crt
```

* Convert the client certificate to PFX
```shell
openssl pkcs12 -export -passout pass:qwerty -inkey client.key -in client.crt -out client.pfx
```

* Convert the client certificate to PEM
```shell
openssl pkcs12 -passin pass:qwerty -passout pass:qwerty -in client.pfx -out client.pem
```

## Diffie-Hellman key exchange

* Create DH parameters
```shell
openssl dhparam -out dh4096.pem 4096
```
