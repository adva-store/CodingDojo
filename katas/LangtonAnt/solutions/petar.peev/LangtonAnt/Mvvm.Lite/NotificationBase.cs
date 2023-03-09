using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LangtonAnt.Mvvm.Lite;

public abstract class NotificationBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    // SetField (Name, value); // where there is a data member
    protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string property = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        RaisePropertyChanged(property);
        return true;
    }

    // SetField(()=> somewhere.Name = value; somewhere.Name, value) // Advanced case where you rely on another property
    protected virtual void SetProperty<T>(T currentValue, T newValue, Action doSet, [CallerMemberName] string property = null)
    {
        if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return;
        doSet.Invoke();
        RaisePropertyChanged(property);
    }

    protected void RaisePropertyChanged(string property)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}

public abstract class NotificationBase<T> : NotificationBase where T : class, new()
{
    protected readonly T This;

    public static implicit operator T(NotificationBase<T> thing) { return thing.This; }

    protected NotificationBase(T thing = null)
    {
        This = thing ?? new T();
    }
}
