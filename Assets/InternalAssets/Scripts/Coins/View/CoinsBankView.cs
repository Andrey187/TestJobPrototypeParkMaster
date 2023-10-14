using System.ComponentModel;
using TMPro;
using UnityEngine;

public class CoinsBankView : MonoBehaviour
{
    [SerializeField] private CoinsBank _coinsBank;
    [SerializeField] private TextMeshProUGUI _countCoins;
    private CoinsBankViewModel _viewModel;
    private void Start()
    {
        var coinsBank = _coinsBank;
        _viewModel = new CoinsBankViewModel(_coinsBank);
        _viewModel.PropertyChanged += HandlePropertyChanged;
        _countCoins.SetText("Collect coins: " + _viewModel.TotalTokens.ToString());
    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_viewModel.TotalTokens):
                _countCoins.SetText("Collect coins: " + _viewModel.TotalTokens.ToString());
                break;
        }
    }
}
