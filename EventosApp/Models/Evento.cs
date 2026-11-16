using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EventosApp.Models;

public class Evento : INotifyPropertyChanged
{
    private string _nomeEvento = string.Empty;
    private DateTime _dataInicio = DateTime.Today;
    private DateTime _dataTermino = DateTime.Today;
    private int _numeroParticipantes;
    private string _localEvento = string.Empty;
    private decimal _custoPorParticipante;

    public string NomeEvento
    {
        get => _nomeEvento;
        set => SetField(ref _nomeEvento, value);
    }

    public DateTime DataInicio
    {
        get => _dataInicio;
        set
        {
            if (SetField(ref _dataInicio, value))
                NotificarDuracao();
        }
    }

    public DateTime DataTermino
    {
        get => _dataTermino;
        set
        {
            if (SetField(ref _dataTermino, value))
                NotificarDuracao();
        }
    }

    public int NumeroParticipantes
    {
        get => _numeroParticipantes;
        set
        {
            if (SetField(ref _numeroParticipantes, value))
                OnPropertyChanged(nameof(CustoTotal));
        }
    }

    public string LocalEvento
    {
        get => _localEvento;
        set => SetField(ref _localEvento, value);
    }

    public decimal CustoPorParticipante
    {
        get => _custoPorParticipante;
        set
        {
            if (SetField(ref _custoPorParticipante, value))
                OnPropertyChanged(nameof(CustoTotal));
        }
    }

    // ---------- Propriedades calculadas ----------

    /// <summary>Diferença entre as datas (TimeSpan).</summary>
    public TimeSpan Duracao => DataTermino.Date - DataInicio.Date;

    /// <summary>Duração em dias, contando o dia de início e o de término (10 a 12 = 3 dias).</summary>
    public int DuracaoEmDias => Duracao < TimeSpan.Zero ? 0 : Duracao.Days + 1;

    public string DuracaoTexto => $"{DuracaoEmDias} dia(s)";

    public decimal CustoTotal => NumeroParticipantes * CustoPorParticipante;

    public bool DatasValidas => DataTermino.Date >= DataInicio.Date;

    private void NotificarDuracao()
    {
        OnPropertyChanged(nameof(Duracao));
        OnPropertyChanged(nameof(DuracaoEmDias));
        OnPropertyChanged(nameof(DuracaoTexto));
        OnPropertyChanged(nameof(DatasValidas));
    }

    // ---------- INotifyPropertyChanged ----------
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? nome = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));

    private bool SetField<T>(ref T campo, T valor, [CallerMemberName] string? nome = null)
    {
        if (EqualityComparer<T>.Default.Equals(campo, valor))
            return false;
        campo = valor;
        OnPropertyChanged(nome);
        return true;
    }
}
