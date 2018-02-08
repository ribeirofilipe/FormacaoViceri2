using Dapper;
using Fatec.RD.Dominio.ViewModel;
using Fatec.RD.Infra.Repositorio.Contexto;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Fatec.RD.Infra.Repositorio.Base
{
    public class RelatorioDespesaRepositorio
    {
        readonly DapperContexto _db;
        readonly IDbConnection _connection;
        public RelatorioDespesaRepositorio()
        {
            _db = new DapperContexto();
            _connection = _db.Connection;
        }

        /// <summary>
        /// Método que insere a relação entre Despesa e Relatorio...
        /// </summary>
        /// <param name="idDespesa">IdDespesa</param>
        /// <param name="idRelatorio">IdRelatorio</param>
        public void Inserir(int idDespesa, int idRelatorio)
        {
            _connection.Execute(@"INSERT RelatorioDespesa (IdDespesa, IdRelatorio)
                                   VALUES (@IdDespesa, @IdRelatorio)", new { IdDespesa = idDespesa, IdRelatorio = idRelatorio });
        }

        public void Delete(int idDespesa, int idRelatorio)
        {
            _connection.Execute(@"DELETE FROM RelatorioDespesa WHERE IdDespesa = @IdDespesa and IdRelatorio = @IdRelatorio", new { IdDespesa = idDespesa, IdRelatorio = idRelatorio });
        }

        /// <summary>
        /// Método que retorna as despesas de um relatorio...
        /// </summary>
        /// <param name="idRelatorio">Id do relatorio</param>
        /// <returns>Uma lista de despesas</returns>
        public List<DespesaViewModel> SelecionarPorDespesaRelatorio(int idDespesa)
        {
            var sqlCommand = @"SELECT r.Id, td.Descricao as TipoDespesa, tp.Descricao as TipoPagamento, d.Data, d.Valor, d.Comentario
                                    FROM Relatorio r                                     
                                    WHERE r.Id = @IdDespesa";

            return _connection.Query<DespesaViewModel>(sqlCommand, new { IdDespesa = idDespesa }).ToList();
        }
    }
}
