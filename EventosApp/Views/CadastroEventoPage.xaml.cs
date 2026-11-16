using EventosApp.Models;

namespace EventosApp.Views;

public partial class CadastroEventoPage : ContentPage
{
    private Evento _evento = new();

    public CadastroEventoPage()
    {
        InitializeComponent();
        BindingContext = _evento;
    }

    private async void OnCadastrarClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_evento.NomeEvento))
        {
            await DisplayAlert("Atenção", "Informe o nome do evento.", "OK");
            return;
        }

        if (!_evento.DatasValidas)
        {
            await DisplayAlert("Atenção", "A data de término não pode ser anterior à data de início.", "OK");
            return;
        }

        if (_evento.NumeroParticipantes <= 0)
        {
            await DisplayAlert("Atenção", "Informe um número de participantes maior que zero.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(_evento.LocalEvento))
        {
            await DisplayAlert("Atenção", "Informe o local do evento.", "OK");
            return;
        }

        if (_evento.CustoPorParticipante < 0)
        {
            await DisplayAlert("Atenção", "O custo por participante não pode ser negativo.", "OK");
            return;
        }

        await Navigation.PushAsync(new ResumoEventoPage(_evento));

        // Prepara um novo cadastro para quando o usuário voltar
        _evento = new Evento();
        BindingContext = _evento;
    }
}
