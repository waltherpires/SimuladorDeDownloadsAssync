namespace ListaDeDownloads.Services;

public class DownloadSimulator
{
    private bool _cursorMovido = false;
    private int _linhaBarra;
    public string Nome { get; set; }
    public int Tamanho { get; set; }
    private readonly object _consoleLock;

    public DownloadSimulator(string nome, int tamanho, object consoleLock){
        Nome = nome;
        Tamanho = tamanho;
        _consoleLock = consoleLock;
    }

    public async Task SimularDownload()
    {
        for(int i = 0; i <= Tamanho; i++)
        {
            AtualizarBarraProgresso(i);
            await Task.Delay(100);
        }
    }

    public void AtualizarBarraProgresso(int ProgressoAtual)
    {
        double percentual = (double)ProgressoAtual / Tamanho * 100;

        int larguraBarra = 50;

        int progressoNaBarra = (int) (larguraBarra * ProgressoAtual / Tamanho);
 

        lock (_consoleLock){    
            if(!_cursorMovido)
            {
                _linhaBarra = Console.CursorTop;
                _cursorMovido = true;
            }
        
            Console.SetCursorPosition(1, _linhaBarra);
            Console.Write(Nome + ": [");
            Console.Write(new string('#', progressoNaBarra));
            Console.Write(new string('-', larguraBarra - progressoNaBarra));
            Console.Write($"] {percentual:0.00}%");
            Console.WriteLine();
        }
    }
}