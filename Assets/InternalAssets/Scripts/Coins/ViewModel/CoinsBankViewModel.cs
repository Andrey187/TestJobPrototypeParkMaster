using System.ComponentModel;

public class CoinsBankViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private CoinsBank _coinsBank;

    public int TotalTokens => _coinsBank.TotalTokens;

    public CoinsBankViewModel(CoinsBank CoinsBank)
    {
        _coinsBank = CoinsBank;
      
        _coinsBank.PropertyChanged += HandlePropertyChanged;
    }
    protected void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_coinsBank.TotalTokens):
                OnPropertyChanged(nameof(TotalTokens));
                break;
        }
    }
}
