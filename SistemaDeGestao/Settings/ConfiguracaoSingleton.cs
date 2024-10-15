public class ConfiguracaoSingleton
{
    private static readonly Lazy<ConfiguracaoSingleton> _instance = new(() => new ConfiguracaoSingleton());

    public static ConfiguracaoSingleton Instance => _instance.Value;

    public string Configuracao { get; private set; }

    private ConfiguracaoSingleton()
    {
        Configuracao = "Configuração padrão";
    }
}
