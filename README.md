# AspNet-CoreAPI
Criação de uma aplicação ASP NET CORE WEB API, simples para aplicação de CRUD básico.


## O QUE É UM API REST?

`REST`= Representational state transfer - transferência de estado representacional.
`APIs REST` podem ser baseadas no protocolo HTTP e fornecem aos aplicativos a capacidade de se comunicar usando o formato JSON, sendo executados em servidores web.

`REST` = é um estilo de arquitetura que fornece padrões entre sistemas de computadores na web, com objetivo de facilitar a comunicação entre os sistemas.´

#

## Entidades da arquitetura REST

`RESOURCE`: recursos são entidades identificaveis de forma única.

`ENDPOINT`: É um recurso pode ser acessado atráves de um identificador de URL;

`RESOURCE METHOD`: Embora não seja obrigatório, vamos considerar o método HTTP como sendo o tipo de solicitação que um cliente envia para um servidor web. Os principais métodos HTTP usados nas APIs REST criadas na plataforma .NET são:  GET, POST PUT e DELETE. (Lembre-se que REST não é HTTP)

`HEADER HTTP`: Um cabeçalho HTTP é um par de chave-valor usado para compartilhar informações adicionais entre um cliente e servidor, como:

> Tipo de dados que estão sendo enviados ao servidor (JSON, XML);
> Tipo de criptografia suportada pelo cliente;
> Token relacionado à autenticação;
> Dados do cliente com base na necessidade do aplicativo;
> Formato de dados: JSON é um formato comum para enviar e receber dados por meio de APIs REST;

#

## Token JWT 

JWT significa json web token e, é um padrão aberto que define uma maneira melhor de transferirrr dados com segurança entre duas entidades (empresa e servidor).


O JWT consiste em três partes a seguir:

1. HEADER > dados codificados do tipo e o algoritmo usado para assinar os dados;
2. PAYLOAD > dados codificados de claims ou reivindicações destinadas a compartilhar;
3. SIGNATURE > criada a assinatura (cabeçalho codificado + cargo útil codificada) usando uma chave secreta;


O token JWT final será assim: `Header.Payload.Signature`
#
### Segunda parte do artigo
> endpoint de uma API é a URL onde seu serviço pode ser acessado por uma aplicação cliente 

> Uma API é o conjunto de rotinas, protocolos e ferramentas para construir aplicações


Na implementação da injeção de dependência do ASP.NET core, vemos conceito de lifetimes ou "tempos de vida". Um tempo de vida especifica quando um objeto injetado é criado ou recriado. Existe 3 possibilidades.

Transient > Criado a cada vez que são solicitados
Scoped > Criado uma vez por solicitação
Singleton > Criado na primeira vez que são solcitados. Cada solicitação subsequente usa instancia que foi criada na primeira vez.
#

Referências de estudo 
..
