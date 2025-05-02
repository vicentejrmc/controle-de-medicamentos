using ControleDeMedicamentos.Compartilhado;


namespace ControleDeMedicamentos.ModuloRequisicaoEntrada
{
    public class RepositorioRequisicaoEntrada : RepositorioBasEmArquivo<RequisicaoEntrada>, IRepositorioRequisicaoEntrada
    {
        public RepositorioRequisicaoEntrada(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<RequisicaoEntrada> ObterRegistros()
        {
            return contexto.RequisicaoEntradas;
        }
    }
}
