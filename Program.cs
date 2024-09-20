using System.Collections;
using ListaDeDownloads.Services;

namespace ListaDeDownloads;

class Program
{
    private static readonly object consoleLock = new object();
    static async Task Main(string[] args)
    {
        List<DownloadSimulator> downloads = new List<DownloadSimulator>();
        Console.Clear();
        Console.Write("Quantidade de arquivos para Download: ");
        int qtdArquivos = int.Parse(Console.ReadLine());

        for(int i = 0; i < qtdArquivos; i++) 
        {
            Console.Write("Nome: ");
            string? nome = Console.ReadLine();
            Console.Write("Tamanho: ");
            int tamanho = int.Parse(Console.ReadLine());
            downloads.Add(new DownloadSimulator(nome, tamanho, consoleLock));
        }

        List<Task> tasks = new List<Task>();

        foreach(DownloadSimulator download in downloads)
        {
            tasks.Add(download.SimularDownload());            
        }

        await Task.WhenAll(tasks);
    }
}
