Desafio Desenvolvedor
======================================

#### Desafio

[Detalhamento](https://github.com/jeduardocosta/desafio-dev/blob/master/challenge.md)

#### Continuous Delivery service

* Build and tests

[![Build status](https://ci.appveyor.com/api/projects/status/38spj92u1vo778iy?svg=true)](https://ci.appveyor.com/project/jeduardocosta/desafio-dev)

#### Libraries utilizadas

- [NancyFX](https://github.com/NancyFx/Nancy) - Lightweight, low-ceremony, framework for building HTTP based services
- [FluentAssertions](https://github.com/dennisdoomen/FluentAssertions) - Fluent Assertions is a set of .NET extension methods that allow you to more naturally specify the expected outcome of a TDD or BDD-style test
- [FluentValidation](https://github.com/JeremySkinner/FluentValidation) - A small validation library for .NET that uses a fluent interface and lambda expressions for building validation rules.
- [LightInject](https://github.com/seesharper/LightInject) - An ultra lightweight IoC container
- [NUnit](https://github.com/nunit/nunit) - Unit-testing framework for all .Net languages
- [RestSharp](https://github.com/restsharp/RestSharp) - Simple REST and HTTP API Client

#### Como utilizar?

Através do recurso "routes", realizar ação POST e informar lista de endereços e tipo da rota (shortest ou fastest) a ser calculada.

```json
/routes
```

#### Exemplo de request

```json
{
	"addresses": [{
		"street": "av paulista",
		"number": "1",
		"city": "sao paulo",
		"state": "sp"
	}, {
		"street": "av paulista",
		"number": "200",
		"city": "sp",
		"state": "sp"
	}],
	"type": "shortest"
}
```

#### Exemplo de response

##### Status code 200

```json
{
	"data": [{
		"totalTime": 5646,
		"totalDistance": 76408,
		"fuelCost": 0,
		"totalCostWithToll": 0
	}],
	"errors": [],
	"success": true
}
```

##### Status code 422
```json
{
  "data": [],
  "errors": [
    "street information should be informed",
    "entry state information is not valid"
  ],
  "success": false
}
```

##### Status code 500
```json
{
  "data": [],
  "errors": [
    "an error occurred"
  ],
  "success": false
}
```

#### Próximos passos

- Converter projeto para [.NET Core](https://dotnet.github.io/)
- Construir imagem no [Docker](https://www.docker.com/) e assim disponibilizar um ambiente pronto para testes funcionais da aplicação 
