using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.ViewModel;
using Fatec.RD.Infra.Repositorio.Base;
using Fatec.RD.SharedKernel.Excecoes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;


namespace Fatec.RD.Bussiness
{
    public sealed class DespesaNegocio
    {
        DespesaRepositorio _despesaRepositorio;
        TipoDespesaRepositorio _tipoDespesaRepositorio;
        TipoPagamentoRepositorio _tipoPagamentoRepositorio;

        public DespesaNegocio()
        {
            _despesaRepositorio = new DespesaRepositorio();
            _tipoPagamentoRepositorio = new TipoPagamentoRepositorio();
            _tipoDespesaRepositorio = new TipoDespesaRepositorio();
        }

        /// <summary>
        /// Método que seleciona uma lista de despesas...
        /// </summary>
        /// <returns></returns>
        public List<DespesaViewModel> Selecionar()
        {
            return _despesaRepositorio.Selecionar();
        }

        public DespesaViewModel Adicionar(DespesaInput obj)
        {
            var objDespesa = new Despesa()
            {
                IdTipoDespesa = obj.IdTipoDespesa,
                IdTipoPagamento = obj.IdTipoPagamento,
                Valor = obj.Valor,
                Comentario = obj.Comentario,
                Data = obj.Data,
                DataCriacao = obj.DataCriacao

            };

            var idDespesa = _despesaRepositorio.Inserir(objDespesa);

            return _despesaRepositorio.SelecionarPorId(idDespesa);
        }

        public DespesaViewModel Atualizar(int id, DespesaInput obj)
        {
            var despesa = _despesaRepositorio.SelecionarPorDespesa(id);

            if (despesa == null)
                throw new NaoEncontradoException("Despesa não encontrada!", id);

            despesa.IdTipoDespesa = obj.IdTipoDespesa;
            despesa.IdTipoPagamento = obj.IdTipoPagamento;
            despesa.Valor = obj.Valor;
            despesa.Comentario = obj.Comentario;
            despesa.Data = obj.Data;
            despesa.DataCriacao = obj.DataCriacao;

            _despesaRepositorio.Alterar(despesa);

            return _despesaRepositorio.SelecionarPorId(id);
        }

        public DespesaViewModel Atualizar(int id, JsonPatchDocument<Despesa> obj)
        {
            var despesa = _despesaRepositorio.SelecionarPorDespesa(id);
            if (despesa == null)
                throw new NaoEncontradoException("Despesa não encontrada!", id);

            obj.ApplyTo(despesa);

            _despesaRepositorio.Alterar(despesa);

            return _despesaRepositorio.SelecionarPorId(id);
        }

        public void Deletar(int id)
        {
            var despesa = _despesaRepositorio.SelecionarPorDespesa(id);
            if (despesa == null)
                throw new NaoEncontradoException("Despesa não encontrada!", id);

            _despesaRepositorio.Delete(id);
        }

        public DespesaViewModel SelecionarPorId(int id)
        {
            return _despesaRepositorio.SelecionarPorId(id);
        }
    }
}
