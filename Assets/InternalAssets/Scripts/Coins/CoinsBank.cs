using UnityEngine;
using System.ComponentModel;

public class CoinsBank : MonoBehaviour, ICoinsBank, INotifyPropertyChanged
{
    private int _totalTokens;
    private const string TotalTokensKey = "TotalTokens";

    public event PropertyChangedEventHandler PropertyChanged;

    public int TotalTokens => _totalTokens;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(TotalTokensKey))
        {
            _totalTokens = PlayerPrefs.GetInt(TotalTokensKey);
        }
    }

    public void AddTokens(int amount)
    {
        _totalTokens += amount;
        PlayerPrefs.SetInt(TotalTokensKey, _totalTokens);
        PlayerPrefs.Save();
        OnPropertyChanged(nameof(TotalTokens));
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey(TotalTokensKey);
        PlayerPrefs.Save();
    }
}
