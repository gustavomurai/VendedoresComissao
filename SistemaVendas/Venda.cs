using System;

namespace SistemaVendas
{
    // Representa a venda de um dia: qtde e valor total do dia
    public class Venda
    {
        // Conforme enunciado: - qtde: int | - valor: double
        public int Qtde { get; private set; }
        public double Valor { get; private set; }

        public Venda()
        {
            Qtde = 0;
            Valor = 0.0;
        }

        public Venda(int qtde, double valor)
        {
            Qtde = Math.Max(0, qtde);
            Valor = Math.Max(0.0, valor);
        }

        // + valorMedio(): double  (valor / qtde). Se qtde==0 retorna 0
        public double ValorMedio()
        {
            if (Qtde <= 0) return 0.0;
            return Valor / Qtde;
        }

        // Atualiza os dados da venda do dia
        public void Definir(int qtde, double valor)
        {
            Qtde = Math.Max(0, qtde);
            Valor = Math.Max(0.0, valor);
        }

        // Indica se há registro neste dia (qtde>0 ou valor>0)
        public bool TemRegistro()
        {
            return Qtde > 0 || Valor > 0.0;
        }
    }
}
