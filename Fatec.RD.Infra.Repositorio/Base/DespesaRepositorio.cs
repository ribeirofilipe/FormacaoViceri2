using Dapper;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.ViewModel;
using Fatec.RD.Infra.Repositorio.Contexto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Fatec.RD.Infra.Repositorio.Base
{
    public sealed class DespesaRepositorio
    {
        readonly DapperContexto _db;
        readonly IDbConnection _connection;

        public DespesaRepositorio()
        {
            _db = new DapperContexto();
            _connection = _db.Connection;
        }

        public List<DespesaViewModel> Selecionar()
        {
            var sqlCommand = @"SELECT d.Id, td.Descricao as TipoDespesa, tp.Descricao as TipoPagamento, d.Data, d.Valor, d.Comentario, d.DataCriacao
                                    FROM Despesa d
                                        INNER JOIN TipoPagamento tp ON d.IdTipoPagamento = tp.Id
                                        INNER JOIN TipoDespesa td ON d.IdTipoDespesa = td.Id";

            return _connection.Query<DespesaViewModel>(sqlCommand).ToList(); 
        }

        public DespesaViewModel SelecionarPorId(int id)
        {
            var sqlCommand = @"SELECT d.Id, td.Descricao as TipoDespesa, tp.Descricao as TipoPagamento, d.Data, d.Valor, d.Comentario, d.DataCriacao
                                FROM Despesa d
                                INNER JOIN TipoPagamento tp ON d.IdTipoPagamento = tp.Id
                                INNER JOIN TipoDespesa td ON d.IdTipoDespesa = td.Id
                                WHERE d.id = @id";

            return _connection.Query<DespesaViewModel>(sqlCommand, new { id }).SingleOrDefault();
        }

        public Despesa SelecionarPorDespesa(int id)
        {
            var sqlCommand = @"SELECT Id, IdTipoPagamento, IdTipoDespesa, Data ,Valor, Comentario, DataCriacao
                                    FROM Despesa
                                        WHERE Id like @Id";

            return _connection.Query<Despesa>(sqlCommand, new { Id = $"%{id}%" }).FirstOrDefault();


        }

        public int Inserir(Despesa obj){
            return _connection.Query<int>(@" INSERT Despesa (IdTipoPagamento, IdTipoDespesa, Data ,Valor, Comentario, DataCriacao)
                                          VALUES (@IdTipoPagamento, @IdTipoDespesa, @Data, @Valor, @Comentario, @DataCriacao)
                                          SELECT CAST (SCOPE_IDENTITY() as int)", obj).First();
        }

        public void Alterar(Despesa obj)
        {
            var sqlCommand = @"UPDATE Despesa 
                                SET IdTipoPagamento = @IdTipoPagamento, IdTipoDespesa = @IdTipoDespesa, Data = @Data, Valor = @Valor, Comentario = @Comentario, DataCriacao = @DataCriacao)
                               WHERE Id = @Id";


            _connection.Execute(sqlCommand, obj);
        }

        public void Delete(int id)
        {
            _connection.Execute("DELETE FROM Despesa WHERE Id = @Id", new { Id = id });
        }
    }
}
