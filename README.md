## Fluxo de Criação de Produto (CQRS + Decorator + Redis)

```
┌──────────────┐       ┌──────────────┐       ┌───────────────────────┐
│   Controller │──────▶│   MediatR     │──────▶│ CreateProdutoHandler  │
└──────────────┘       └──────────────┘       └──────────────┬────────┘
                                                             │
                                                             │ injeta
                                                             ▼
                                              ┌──────────────────────────┐
                                              │ IProdutoRepository        │
                                              │ (Decorator registrado)    │
                                              └──────────────┬───────────┘
                                                             │
                                                             │ chama internamente
                                                             ▼
                                ┌─────────────────────────────────────────────┐
                                │ ProdutoRepositoryCacheDecorator              │
                                │   - Atualiza cache Redis                     │
                                │   - Encaminha operação ao repositório real   │
                                └─────────────────┬───────────────────────────┘
                                                  │
                                                  │ chama
                                                  ▼
                                ┌─────────────────────────────────────────────┐
                                │ ProdutoRepository                           │
                                │   - Usa EF Core                             │
                                │   - Salva no banco SQL                       │
                                └─────────────────┬───────────────────────────┘
                                                  │
                                                  │ commit
                                                  ▼
                                ┌─────────────────────────────────────────────┐
                                │ UnitOfWork                                   │
                                │   - SaveChangesAsync                         │
                                │   - CommitAsync transaction                  │
                                └─────────────────────────────────────────────┘
```
