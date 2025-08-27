using System;

namespace SistemaVendas
{
    public class Vendedores
    {
        // - osVendedores: Vendedor[] | - max: int | - qtde: int
        public Vendedor[] OsVendedores { get; private set; }
        public int Max { get; private set; }
        public int Qtde { get; private set; }

        public Vendedores(int max = 10)
        {
            Max = Math.Max(1, max);
            OsVendedores = new Vendedor[Max];
            Qtde = 0;
        }

        // + addVendedor(Vendedor v): bool
        public bool AddVendedor(Vendedor v)
        {
            if (v == null) return false;
            if (Qtde >= Max) return false;

            // Não permitir ID duplicado
            if (SearchVendedorPorId(v.Id) != null) return false;

            OsVendedores[Qtde] = v;
            Qtde++;
            return true;
        }

        // + delVendedor(Vendedor v): bool
        // Só exclui se não há nenhuma venda associada.
        public bool DelVendedor(Vendedor v)
        {
            if (v == null) return false;
            int idx = IndexOfId(v.Id);
            if (idx == -1) return false;

            if (OsVendedores[idx].TemAlgumaVenda()) return false;

            // Reorganiza o array deslocando à esquerda
            for (int i = idx; i < Qtde - 1; i++)
                OsVendedores[i] = OsVendedores[i + 1];

            OsVendedores[Qtde - 1] = null;
            Qtde--;
            return true;
        }

        // + searchVendedor(Vendedor v): Vendedor
        // Interpretação: busca por id do vendedor passado
        public Vendedor SearchVendedor(Vendedor v)
        {
            if (v == null) return null;
            return SearchVendedorPorId(v.Id);
        }

        // Auxiliar: buscar por id diretamente
        public Vendedor SearchVendedorPorId(int id)
        {
            for (int i = 0; i < Qtde; i++)
                if (OsVendedores[i].Id == id)
                    return OsVendedores[i];
            return null;
        }

        private int IndexOfId(int id)
        {
            for (int i = 0; i < Qtde; i++)
                if (OsVendedores[i].Id == id)
                    return i;
            return -1;
        }

        // + valorVendas(): double  (total de todos os vendedores)
        public double ValorVendas()
        {
            double total = 0.0;
            for (int i = 0; i < Qtde; i++)
                total += OsVendedores[i].ValorVendas();
            return total;
        }

        // + valorComissao(): double  (total das comissões)
        public double ValorComissao()
        {
            double total = 0.0;
            for (int i = 0; i < Qtde; i++)
                total += OsVendedores[i].ValorComissao();
            return total;
        }
    }
}
