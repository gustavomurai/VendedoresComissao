using System;

namespace SistemaVendas
{
    public class Vendedor
    {
        // - id: int | - nome: string | - percComissao: double | - asVendas: Venda[31]
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public double PercComissao { get; private set; } // percentual, ex: 5 => 5%

        public Venda[] AsVendas { get; private set; } = new Venda[31];

        public Vendedor(int id, string nome, double percComissao)
        {
            Id = id;
            Nome = nome ?? "";
            PercComissao = Math.Max(0.0, percComissao);

            // Inicializa os 31 dias com objetos Venda para evitar null
            for (int i = 0; i < 31; i++)
                AsVendas[i] = new Venda();
        }

        // + registrarVenda(int dia, Venda venda): void
        public void RegistrarVenda(int dia, Venda venda)
        {
            if (dia < 1 || dia > 31) throw new ArgumentOutOfRangeException(nameof(dia), "Dia deve ser 1..31");
            if (venda == null) throw new ArgumentNullException(nameof(venda));

            // Substitui/define o registro do dia
            AsVendas[dia - 1].Definir(venda.Qtde, venda.Valor);
        }

        // + valorVendas(): double  (soma de todos os dias)
        public double ValorVendas()
        {
            double soma = 0.0;
            for (int i = 0; i < 31; i++)
                soma += AsVendas[i].Valor;
            return soma;
        }

        // + valorComissao(): double
        public double ValorComissao()
        {
            return ValorVendas() * (PercComissao / 100.0);
        }

        // Verifica se há qualquer venda registrada (usado para exclusão)
        public bool TemAlgumaVenda()
        {
            for (int i = 0; i < 31; i++)
                if (AsVendas[i].TemRegistro())
                    return true;
            return false;
        }

        // Valor médio das vendas DIÁRIAS (média dos totais dos dias que possuem registro)
        public double ValorMedioDiario()
        {
            double somaDias = 0.0;
            int diasCom = 0;
            for (int i = 0; i < 31; i++)
            {
                if (AsVendas[i].TemRegistro())
                {
                    somaDias += AsVendas[i].Valor; // total do dia
                    diasCom++;
                }
            }
            if (diasCom == 0) return 0.0;
            return somaDias / diasCom;
        }
    }
}
