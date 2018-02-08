using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.ViewModel;
using Fatec.RD.Infra.Repositorio.Base;
using Fatec.RD.SharedKernel.Excecoes;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace Fatec.RD.Bussiness
{
    public sealed class RelatorioNegocio
    {
        RelatorioRepositorio _relatorioRepositorio;
        RelatorioDespesaRepositorio _relatorioDespesaRepositorio;

        public RelatorioNegocio()
        {
            _relatorioRepositorio = new RelatorioRepositorio();
            _relatorioDespesaRepositorio = new RelatorioDespesaRepositorio();
        }       

        public RelatorioViewModel Adicionar(RelatorioInput obj)
        {
            var objRelatorio = new Relatorio()
            {
                IdTipoRelatorio = obj.IdTipoRelatorio,
                Descricao = obj.Descricao,
                Comentario = obj.Comentario,
                DataCriacao = obj.DataCriacao

            };

            var idRelatorio = _relatorioRepositorio.Inserir(objRelatorio);

            return _relatorioRepositorio.SelecionarPorId(idRelatorio);
        }

        public RelatorioViewModel Atualizar(int id, JsonPatchDocument<Relatorio> obj)
        {
            var relatorio = _relatorioRepositorio.SelecionarRelatorioPorId(id);

            if (relatorio == null)
                throw new NaoEncontradoException("Relatório não encontrado!", id);

            obj.ApplyTo(relatorio);

            _relatorioRepositorio.Alterar(relatorio);

            return _relatorioRepositorio.SelecionarPorId(id);
        }

        public RelatorioViewModel Atualizarint (int id, RelatorioInput obj)
        {
            var relatorio = _relatorioRepositorio.SelecionarRelatorioPorId(id);

            if (relatorio == null)
                throw new NaoEncontradoException("Relatorio não encontrado!", id);

            relatorio.IdTipoRelatorio = obj.IdTipoRelatorio;
            relatorio.Descricao = obj.Descricao;
            relatorio.Comentario = obj.Comentario;
            relatorio.DataCriacao = obj.DataCriacao;

            _relatorioRepositorio.Alterar(relatorio);

            return _relatorioRepositorio.SelecionarPorId(id);
        }

        public void Deletar(int id)
        {
            var despesa = _relatorioRepositorio.SelecionarPorRelatorio(id);
            if (despesa == null)
                throw new NaoEncontradoException("Relatório não encontrado!", id);

            _relatorioRepositorio.Delete(id);
        }


        /// <summary>
        /// Método que seleciona uma lista de relatorio...
        /// </summary>
        /// <returns></returns>
        public List<RelatorioViewModel> Selecionar()
        {
            return _relatorioRepositorio.Selecionar();
        }

        /// <summary>
        /// Método que seleciona despesas pelo relatorio...
        /// </summary>
        /// <param name="idRelatorio">Id do relatório</param>
        /// <returns></returns>
        public List<DespesaViewModel> SelecionarDespesasPorRelatorio(int idRelatorio)
        {
            this.SelecionarPorId(idRelatorio);

            return _relatorioRepositorio.SelecionarPorRelatorio(idRelatorio);
        }

        /// <summary>
        /// Método que seleciona despesas sem ser atrelado com o relatorio
        /// </summary>
        /// <returns></returns>
        public List<DespesaViewModel> SelecionarDespesasSemRelatorio()
        {
            return _relatorioRepositorio.SelecionarDespesasSemRelatorio();
        }

        /// <summary>
        /// Método que seleciona um relatorio pelo Id
        /// </summary>
        /// <param name="id">ID do relatório</param>
        /// <returns>Objeto de relatorio</returns>
        public RelatorioViewModel SelecionarPorId(int id)
        {
            var retorno = _relatorioRepositorio.SelecionarPorId(id);

            if (retorno.Id <= 0)
                throw new NaoEncontradoException("Relatório não encontrado", id);

            return retorno;
        }

        /// <summary>
        /// Método que insere a relação de Despesa com relatório...
        /// </summary>
        /// <param name="obj">Obj de Input</param>
        public void InserirRelatorioDespesa(int idRelatorio, RelatorioDespesaInput obj)
        {
            foreach (var item in obj.Chave)
            {
                _relatorioDespesaRepositorio.Inserir(item.IdDespesa, idRelatorio);
            }

        }
        

        public void DeletarPorIdRelatorioDespesa(ChaveRelatorioDespesa obj, int id)
        {                           
                _relatorioDespesaRepositorio.Delete(obj.IdDespesa, id);          
        }






        }
}
