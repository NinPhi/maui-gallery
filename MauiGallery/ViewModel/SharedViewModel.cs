using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiGallery.ViewModel;

public partial class SharedViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<FileResult> images;

    [ObservableProperty]
    private FileResult target;

    public SharedViewModel()
    {
        var dir = FileSystem.AppDataDirectory;
        Images = new ObservableCollection<FileResult>(
            Directory.GetFiles(dir).Select(path => new FileResult(path)));
    }

    [RelayCommand]
    private void SetTarget(FileResult image)
    {
        if (image != null)
            Target = image;
    }

    [RelayCommand]
    private void Show()
    {

    }

    [RelayCommand]
    private async Task Add()
    {

    }

    [RelayCommand]
    private void Delete()
    {
        if (Images.Contains(Target))
            Images.Remove(Target);
    }
}
