using System;
using System.Globalization;
using System.Text;

namespace SistemaVendas
{
    class Program
    {
        static Vendedores vendedores = new Vendedores(10);

        static void Main(string[] args)
        {
            // Configurar cultura pt-BR para exibir R$ corretamente
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== SISTEMA DE VENDAS (Console) =====");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar vendedor (*)");
                Console.WriteLine("2. Consultar vendedor (**)");
                Console.WriteLine("3. Excluir vendedor (***)");
                Console.WriteLine("4. Registrar venda");
                Console.WriteLine("5. Listar vendedores (****)");
                Console.WriteLine("---------------------------------------");

                int opc = LerInt("Escolha uma opção: ", 0, 5);

                switch (opc)
                {
                    case 0:
                        Console.WriteLine("Encerrando...");
                        return;
                    case 1:
                        OpcCadastrarVendedor();
                        break;
                    case 2:
                        OpcConsultarVendedor();
                        break;
                    case 3:
                        OpcExcluirVendedor();
                        break;
                    case 4:
                        OpcRegistrarVenda();
                        break;
                    case 5:
                        OpcListarVendedores();
                        break;
                }

                Pausa();
            }
        }

        // ---------- Opção 1 ----------
        static void OpcCadastrarVendedor()
        {
            Console.Clear();
            Console.WriteLine("== Cadastrar Vendedor ==");

            if (vendedores.Qtde >= vendedores.Max)
            {
                Console.WriteLine("Limite máximo de vendedores atingido (10).");
                return;
            }

            int id = LerInt("ID do vendedor (inteiro): ", 1, int.MaxValue);

            if (vendedores.SearchVendedorPorId(id) != null)
            {
                Console.WriteLine("Já existe vendedor com esse ID.");
                return;
            }

            Console.Write("Nome do vendedor: ");
            string nome = (Console.ReadLine() ?? "").Trim();

            double perc = LerDouble("Percentual de comissão (%), ex: 5 para 5%: ");

            Vendedor v = new Vendedor(id, nome, perc);
            bool ok = vendedores.AddVendedor(v);

            Console.WriteLine(ok ? "Vendedor cadastrado com sucesso." : "Falha ao cadastrar vendedor.");
        }

        // ---------- Opção 2 ----------
        static void OpcConsultarVendedor()
        {
            Console.Clear();
            Console.WriteLine("== Consultar Vendedor ==");

            int id = LerInt("Informe o ID do vendedor: ", 1, int.MaxValue);
            Vendedor v = vendedores.SearchVendedorPorId(id);

            if (v == null)
            {
                Console.WriteLine("Vendedor não encontrado.");
                return;
            }

            double totalVendas = v.ValorVendas();
            double comissao = v.ValorComissao();
            double mediaDiaria = v.ValorMedioDiario();

            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"ID: {v.Id}");
            Console.WriteLine($"Nome: {v.Nome}");
            Console.WriteLine($"Valor total das vendas: {totalVendas:C}");
            Console.WriteLine($"Valor da comissão devida: {comissao:C}");
            Console.WriteLine($"Valor médio das vendas DIÁRIAS: {mediaDiaria:C}");
            Console.WriteLine("---------------------------------------");
        }

        // ---------- Opção 3 ----------
        static void OpcExcluirVendedor()
        {
            Console.Clear();
            Console.WriteLine("== Excluir Vendedor ==");

            int id = LerInt("Informe o ID do vendedor: ", 1, int.MaxValue);
            Vendedor v = vendedores.SearchVendedorPorId(id);

            if (v == null)
            {
                Console.WriteLine("Vendedor não encontrado.");
                return;
            }

            if (v.TemAlgumaVenda())
            {
                Console.WriteLine("Não é possível excluir. O vendedor possui vendas registradas.");
                return;
            }

            bool ok = vendedores.DelVendedor(v);
            Console.WriteLine(ok ? "Vendedor excluído." : "Falha ao excluir vendedor.");
        }

        // ---------- Opção 4 ----------
        static void OpcRegistrarVenda()
        {
            Console.Clear();
            Console.WriteLine("== Registrar Venda ==");

            int id = LerInt("ID do vendedor: ", 1, int.MaxValue);
            Vendedor v = vendedores.SearchVendedorPorId(id);

            if (v == null)
            {
                Console.WriteLine("Vendedor não encontrado.");
                return;
            }

            int dia = LerInt("Dia do mês (1 a 31): ", 1, 31);
            int qtde = LerInt("Quantidade de vendas do DIA: ", 0, int.MaxValue);
            double valor = LerDouble("Valor TOTAL das vendas do DIA (R$): ");

            bool jaTinha = v.AsVendas[dia - 1].TemRegistro();

            v.RegistrarVenda(dia, new Venda(qtde, valor));

            Console.WriteLine(jaTinha ? "Venda do dia atualizada com sucesso." : "Venda do dia registrada com sucesso.");
        }

        // ---------- Opção 5 ----------
        static void OpcListarVendedores()
        {
            Console.Clear();
            Console.WriteLine("== Listar Vendedores ==");

            if (vendedores.Qtde == 0)
            {
                Console.WriteLine("Nenhum vendedor cadastrado.");
                return;
            }

            double somaVendas = 0.0;
            double somaComissoes = 0.0;

            Console.WriteLine("ID | Nome                          | Total Vendas       | Comissão Devida");
            Console.WriteLine("--------------------------------------------------------------------------");

            for (int i = 0; i < vendedores.Qtde; i++)
            {
                Vendedor v = vendedores.OsVendedores[i];
                double tot = v.ValorVendas();
                double com = v.ValorComissao();

                somaVendas += tot;
                somaComissoes += com;

                Console.WriteLine($"{v.Id,2} | {Trunc(v.Nome, 28),-28} | {tot,16:C} | {com,15:C}");
            }

            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine($"TOTAIS                             | {somaVendas,16:C} | {somaComissoes,15:C}");
        }

        // ===== Helpers =====
        static int LerInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = (Console.ReadLine() ?? "").Trim();

                if (int.TryParse(s, out int v))
                {
                    if (v >= min && v <= max) return v;
                }
                Console.WriteLine($"Valor inválido. Digite um inteiro entre {min} e {max}.");
            }
        }

        static double LerDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = (Console.ReadLine() ?? "").Trim();

                // aceita vírgula ou ponto
                if (double.TryParse(s, NumberStyles.Any, new CultureInfo("pt-BR"), out double v) ||
                    double.TryParse(s.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out v))
                {
                    if (v >= 0.0) return v;
                }

                Console.WriteLine("Valor inválido. Digite um número maior ou igual a 0 (aceita vírgula ou ponto).");
            }
        }

        static void Pausa()
        {
            Console.WriteLine();
            Console.Write("Pressione ENTER para continuar...");
            Console.ReadLine();
        }

        static string Trunc(string s, int maxLen)
        {
            s ??= "";
            return s.Length <= maxLen ? s : s.Substring(0, maxLen);
        }
    }
}
