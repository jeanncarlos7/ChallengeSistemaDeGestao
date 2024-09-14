public class ConfiguracaoSingleton
{
    private static ConfiguracaoSingleton _instance;
    private static readonly object _lock = new object();
    public string Configuracao { get; private set; }

    private ConfiguracaoSingleton()
    {
        Configuracao = "Configuração padrão";
    }

    public static ConfiguracaoSingleton Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ConfiguracaoSingleton();
                }
                return _instance;
            }
        }
    }
}
