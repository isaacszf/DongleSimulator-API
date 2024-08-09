⚠️ **Projeto em Desenvolvimento** ⚠️

# DongleSimulator - Sobre

Apresentando o **DongleSimulator** - Totalmente inspirado no [FeijoadaSimulator](https://x.com/FeijoadaSim), o DongleSimulator é um ecosistema onde você pode enviar Templates e Sources para aprovação dos Administradores. Depois que forem aprovados, esses templates e sources poderão ser utilizados como input **para gerar imagens aleatórias**!

A **API** oferece suporte para **PostgreSQL** como opção para banco de dados e o **Cloudinary** como host de imagens. Ela permite que usuários cadastrados possam enviar **sources e templates** (imagens). Essas imagens podem ser **aprovadas** pelos Administradores, que então estarão aptas a serem utilizadas pelo "Bot" que gera as imagens.

Outras tecnologias e práticas adotadas incluem o **Entity Framework** para o mapeamento de objetos relacionais e a implementação de **Tokens JWT** para autenticação segura. As migrações do banco de dados são gerenciadas para assegurar uma evolução controlada do esquema de dados.

## Features

- **Dashboard de Sources e Templates**: Todos irão poder ver os sources e templates pendentes, aprovados e negados. 📋
- **CRUD de Sources e Templates**: Usuários normais podem enviar e deletar seus próprios sources e templates. Usuários Admin podem gerenciar essas imagens, aprovando, negando ou excluindo-as. ⬆️🖼️
- **Autenticação e Autorização**: Usuários normais e administradores têm rotas específicas e personalizadas para suas funcionalidades. 🔒
- **Geração de Imagens**: Usuários admin têm acesso a uma rota personalizada que permite gerar imagens a partir dos sources e templates aprovados, de forma aleatória ou conforme especificado no corpo da requisição. 🤖 
- **Domain-Driven Design (DDD)**: Estrutura modular que facilita a manutenção da aplicação. 🖥️

## Funcionalidade dos Templates

Os templates devem seguir um padrão específico. Se o número de substituições ("replaces") em um template for igual a 2, o template deve incluir uma área nas cores verde (#00ff00) e roxo (#800080). Se o número de substituições for igual a 1, apenas a área verde (#00ff00) é necessária.

EX:
<div>
  <image src="./images/ex.png" width="100">
</div>

O Bot irá procurar por essas cores e substituí-las pelas sources escolhidas.

## Ferramentas

![badge-dot-net]
![badge-swagger]
![badge-postgres]


## TO-DO

- Documentação Swagger - [ ]
- Seed para criar um usuário admin - [X]
- Admin
  - Rota para deletar qualquer template - [X]
  - Rota para negar qualquer template - [X]
  - Gerenciar usuários cadastrados - [ ]
- User
  - Rotas para alternar informações pessoais como username e senha - [ ]
- Dashboard
  - Filtro avançado de sources e templates - [ ]

## Rodando

Para obter uma cópia local funcionando, siga estes passos simples.

### Instalação & Utilização

Usuário Admin:
```txt
E-mail: dglsim@email.com
Senha: dongle123456
```

1. Clone o repo
2. Preencha as informações no arquivo `appsettings.Development.Example.json`
3. Remova o `.Example` do nome do arquivo
4. Execute a API

## License
Sinta-se à vontade para usar este projeto para se divertir. No entanto, advirto que a distribuição ou comercialização não é permitida.


<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge
[badge-postgres]: https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white
