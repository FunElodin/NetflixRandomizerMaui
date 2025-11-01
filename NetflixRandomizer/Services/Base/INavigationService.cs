using NetflixRandomizer.Views;


namespace NetflixRandomizer.Services.Base
{
    public interface INavigationService
    {
        Task PushAsync(string route, Dictionary<string, object> arguments);
        Task ShowErrorAsync(string error = "");
        Task ShowPopupAsync(string msg = "");
        Task<string> DisplayActionSheet(string message, params string[] options);
        Task DisplayAlert(string title, string message, string confirm);
        Task GoBackAsync();
        Page GetCurrentPage();
    }

    public class NavigationService : INavigationService
    {
        public async Task PushAsync(string route, Dictionary<string, object> arguments)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.GoToAsync(route, arguments);
            });
        }

        public async Task ShowErrorAsync(string error = "")
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(ErrorPage)}", new Dictionary<string, object> { { "Error", error } });
            });
        }

        public async Task ShowPopupAsync(string msg = "")
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(PopupPage)}", new Dictionary<string, object> { { "Descr", msg } });
            });
        }

        public async Task<string> DisplayActionSheet(string message, params string[] options)
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await Shell.Current.DisplayActionSheet(message, "Cancelar", null, options);
            });
        }

        public async Task DisplayAlert(string title, string message, string confirm)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.DisplayAlert(title, message, confirm);
            });
        }
        public async Task GoBackAsync()
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });
        }

        public Page GetCurrentPage()
        {
            return Shell.Current.CurrentPage;
        }

    }
}
