namespace ControleDeMedicamentos.Models
{
    public class NotificacaoViewModel
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }

        public NotificacaoViewModel() { }

        public NotificacaoViewModel(string titulo, string mensagem)
        {
            Titulo = titulo;
            Mensagem = mensagem;
        }
    }
}
