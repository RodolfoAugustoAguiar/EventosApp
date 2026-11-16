using System.Globalization;

namespace EventosApp;

public partial class App : Application
{
    public App()
    {
        // Datas e moeda no padrão brasileiro
        var cultura = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = cultura;
        CultureInfo.DefaultThreadCurrentUICulture = cultura;
        CultureInfo.CurrentCulture = cultura;

        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
        => new Window(new AppShell());
}
