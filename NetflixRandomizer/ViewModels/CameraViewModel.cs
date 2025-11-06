using NetflixRandomizer.Services.Base;
using System.Windows.Input;

namespace NetflixRandomizer.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {

        public ICommand PhotoCommand { get; set; }

        private string photoSrc;
        public string PhotoSrc
        {
            get => photoSrc;
            set
            {
                photoSrc = value;
                NotifyPropertyChanged();
            }
        }
        public CameraViewModel(INavigationService navigationService, ICrashlyticsService crashlyticsService) : base(navigationService, crashlyticsService)
        {
            this.PhotoCommand = new Command(async () => await this.TakePhoto());
        }

        private async Task TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();
                if (photo != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    using Stream source = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await source.CopyToAsync(localFileStream);

                    PhotoSrc = localFilePath;
                }
            }
            else
            {
                await _navigationService.ShowErrorAsync("DISPOSITIVO SIN CAMARA O NO PERMITIDA");
            }
        }
    }
}
