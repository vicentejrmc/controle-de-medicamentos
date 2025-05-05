using System.Globalization;
namespace ControleDeMedicamentos.Util
{
    public static class Convertor
    {
        public static int ConverterStringParaInt()
        {
            int numero = 0;
            if (int.TryParse(Console.ReadLine(), out numero))
            {
                if(numero == 0) 
                {
                    Notificador.ExibirMensagem("Numero Inválido, Tente novamente", ConsoleColor.Red);
                    return 0;
                }
                return numero;
            } else
            {
                Notificador.ExibirMensagem("Numero Inválido, Tente novamente", ConsoleColor.Red);
                return 0;
            }
        }

        public static DateTime? ConverterStringParaDate(string texto)
        {
            if (DateTime.TryParseExact(texto,"dd/MM/yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None, out DateTime data))
            {
                return data;
            }
            else
            {
                Notificador.ExibirMensagem("Data inválida! Use o formato dd/mm/aaaa.", ConsoleColor.Red);
                return null;
            }
        }
    }
}
