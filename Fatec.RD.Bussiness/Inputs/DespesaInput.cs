using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatec.RD.Bussiness.Inputs
{
    public class DespesaInput
    {
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Comentario { get; set; }
        public int IdTipoPagamento { get; set; }
        public int IdTipoDespesa { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
