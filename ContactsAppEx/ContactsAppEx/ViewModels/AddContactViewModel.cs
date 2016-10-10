﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsAppEx.ViewModels
{
  public class AddContactViewModel : INotifyPropertyChanged
  {
    public AddContactViewModel()
    {
      LaunchWebsiteCommand = new Command(LaunchWebsite, () => !IsBusy);
      SaveContactCommand = new Command(async() => await SaveContact(), () => !IsBusy );
    }

    string name = "David Brillon";
    string website = "http://www.davidbrillon.com";
    bool bestFriend;
    bool isBusy;

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged( [CallerMemberName] string name = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public bool BestFriend
    {
      get { return bestFriend; }
      set
      {
        bestFriend = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(DisplayMessage));
      }
    }

    public string Name
    {
      get { return name; }
      set
      {
        name = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(DisplayMessage));
      }
    }

    public string DisplayMessage
    {
      get { return $"Your new friend is name {name} and " +
                   $"{(bestFriend ? "is" : "is not")} your best friend";
          }
    }

    public string Website
    {
      get { return website; }
      set
      {
        website = value;
        OnPropertyChanged();
      }
    }

    public bool IsBusy
    {
      get { return isBusy; }
      set
      {
        isBusy = value;
        OnPropertyChanged();
        LaunchWebsiteCommand.ChangeCanExecute();
        SaveContactCommand.ChangeCanExecute();
      }
    }

    public Command LaunchWebsiteCommand { get; }
    public Command SaveContactCommand { get; }

    void LaunchWebsite()
    {
      try
      {
        Device.OpenUri(new Uri(website));
      }
      catch 
      {

      }
    }

    async Task SaveContact()
    {
      IsBusy = true;
      await Task.Delay(4000);

      IsBusy = false;

      await Application.Current.MainPage.DisplayAlert("Save", "sadasdadsas", "OK" );
    }

  }
}
