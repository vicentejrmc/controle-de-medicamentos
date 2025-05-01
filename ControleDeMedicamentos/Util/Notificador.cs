namespace ControleDeMedicamentos.Util
{
    internal class Notificador
    {
        public static void ExibirMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine();
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadLine();
        }

        public static void ExibirCorDeFonte(string descricao, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(descricao);
            Console.ResetColor();
        }
    }
}