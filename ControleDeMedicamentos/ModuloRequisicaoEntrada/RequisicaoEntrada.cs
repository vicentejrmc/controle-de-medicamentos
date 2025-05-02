using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloMedicamento;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ModuloRequisicaoEntrada;

public class RequisicaoEntrada : EntidadeBase<RequisicaoEntrada>
{
    public DateTime Data { get; set; }
    public Medicamento Medicamento { get; set; }
    public Funcionario Funcionario { get; set; }
    public int Quantidade { get; set; }

    public RequisicaoEntrada(DateTime data, Medicamento medicamento, Funcionario funcionario, int quantidade)
    {
        Data = data;
        Medicamento = medicamento;
        Funcionario = funcionario;
        Quantidade = quantidade;
    }

    public override void AtualizarRegistro(RequisicaoEntrada resgitroEditado)
    {
        Data = resgitroEditado.Data;
        Medicamento = resgitroEditado.Medicamento;
        Funcionario = resgitroEditado.Funcionario;
        Quantidade = resgitroEditado.Quantidade;
    }

    public override string Validar()
    {
        string erros = "";

        if (Data == null)
            erros += "Erro! O campo Data é obrigatório.\n";

        else if(DateTime.Now > Data)
            erros += "Erro! A data não pode ser menor que a data atual.\n";

        if (!Regex.IsMatch(Data.ToString(), @"^\d{2}/\.\d{2}/\.\d{4}$"))
            erros += "Erro! O campo Data deve estar no formato dd/MM/yyyy.\n";

        if (Medicamento == null)
            erros += "Erro! O campo Medicamento é obrigatório.\n";

        if(Funcionario == null)
            erros += "Erro! O campo Funcionario é obrigatório.\n";

        if (Quantidade <= 0)
            erros += "Erro! O campo Quantidade deve ser maior que zero.\n";

        return erros;
    }
}