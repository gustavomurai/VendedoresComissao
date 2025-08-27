# 📊 Sistema de Gerenciamento de Vendedores

## 👨‍💻 Autores
- **Gustavo Cerqueira Murai**  
- **Igor Cerqueira Murai**  


Projeto desenvolvido para a disciplina **Estrutura de Dados II**.  
O objetivo é simular o controle de vendas de uma equipe comercial composta por 10 vendedores, registrando suas vendas diárias, calculando comissões e exibindo relatórios.

---

## 📌 Descrição do Projeto
- Cada vendedor pode registrar suas vendas diárias (até **31 dias por mês**).  
- O sistema calcula:
  - Valor total vendido.
  - Comissão devida ao vendedor.
  - Valor médio diário das vendas realizadas.  
- É possível:
  1. **Cadastrar vendedor** (máximo 10).  
  2. **Consultar vendedor** (exibir dados completos).  
  3. **Excluir vendedor** (somente se não houver vendas registradas).  
  4. **Registrar venda** (informando o dia e os valores).  
  5. **Listar vendedores** (com totais de vendas e comissão).  
  0. **Sair do sistema**.  

---

## 🛠️ Tecnologias Utilizadas
- **C#**  
- **.NET Console Application**  
- **Visual Studio (Microsoft)**  

---

## 📂 Estrutura das Classes

### Classe `Venda`
| Atributo | Tipo   | Descrição |
|----------|--------|------------|
| qtde     | int    | Quantidade de vendas |
| valor    | double | Valor total da venda |

**Métodos**  
- `valorMedio() : double` → calcula o valor médio por item vendido.

---

### Classe `Vendedor`
| Atributo       | Tipo    | Descrição |
|----------------|---------|------------|
| id             | int     | Identificador do vendedor |
| nome           | string  | Nome do vendedor |
| percComissao   | double  | Percentual da comissão |
| asVendas[31]   | Venda[] | Vendas diárias (máx. 31 dias) |

**Métodos**  
- `registrarVenda(int dia, Venda venda)` → registra uma venda em determinado dia.  
- `valorVendas() : double` → soma total de vendas do vendedor.  
- `valorComissao() : double` → calcula o valor da comissão devida.  

---

### Classe `Vendedores`
| Atributo       | Tipo        | Descrição |
|----------------|-------------|------------|
| osVendedores   | Vendedor[]  | Lista de vendedores |
| max            | int         | Quantidade máxima (10) |
| qtde           | int         | Quantidade atual |

**Métodos**  
- `addVendedor(Vendedor v) : bool` → adiciona vendedor.  
- `delVendedor(Vendedor v) : bool` → exclui vendedor.  
- `searchVendedor(Vendedor v) : Vendedor` → busca vendedor.  
- `valorVendas() : double` → total de vendas de todos os vendedores.  
- `valorComissao() : double` → total de comissões de todos os vendedores.  

---

## ▶️ Como Executar o Projeto
1. Abra o **Visual Studio**.  
2. Clique em **Criar um novo projeto**.  
3. Escolha **Aplicativo de Console (.NET Core ou .NET 6)**.  
4. Nomeie o projeto como `SistemaVendedores`.  
5. Copie o código fonte das classes (`Venda.cs`, `Vendedor.cs`, `Vendedores.cs`, `Program.cs`) para o projeto.  
6. Clique em **Executar (F5)** para rodar.  

---

