using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Extensions.DependencyInjection; // Cần để sử dụng IServiceProvider

namespace ChatApp.Navigation
{
    public class NavigationServiceEx
    {
        public event NavigatedEventHandler Navigated;
        public event NavigationFailedEventHandler NavigationFailed;

        private Frame _frame;
        private readonly IServiceProvider _serviceProvider;

        // Constructor nhận vào IServiceProvider
        public NavigationServiceEx(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Frame Frame
        {
            get
            {
                if (this._frame == null)
                {
                    this._frame = new Frame() { NavigationUIVisibility = NavigationUIVisibility.Hidden };
                    this.RegisterFrameEvents();
                }

                return this._frame;
            }
            set
            {
                this.UnregisterFrameEvents();
                this._frame = value;
                this.RegisterFrameEvents();
            }
        }

        public bool CanGoBack => this.Frame.CanGoBack;

        public bool CanGoForward => this.Frame.CanGoForward;

        public void GoBack() => this.Frame.GoBack();

        public void GoForward() => this.Frame.GoForward();

        // Điều hướng bằng URI (có sẵn)
        public bool Navigate(Uri sourcePageUri, object extraData = null)
        {
            if (this.Frame.CurrentSource != sourcePageUri)
            {
                return this.Frame.Navigate(sourcePageUri, extraData);
            }

            return false;
        }

        // Điều hướng bằng kiểu ViewModel đã đăng ký trong DI
        public bool Navigate<T>() where T : UserControl
        {
            var view = _serviceProvider.GetRequiredService<T>();
            if (this.Frame.Content?.GetType() != view.GetType())
            {
                this.Frame.Content = view;
                return true;
            }

            return false;
        }

        public bool Navigate(Type viewType)
        {
            // Resolve the view from the service provider using the Type
            var view = _serviceProvider.GetRequiredService(viewType) as UserControl;

            if (view != null && this.Frame.Content?.GetType() != view.GetType())
            {
                this.Frame.Content = view;
                return true;
            }

            return false;
        }

        private void RegisterFrameEvents()
        {
            if (this._frame != null)
            {
                this._frame.Navigated += this.Frame_Navigated;
                this._frame.NavigationFailed += this.Frame_NavigationFailed;
            }
        }

        private void UnregisterFrameEvents()
        {
            if (this._frame != null)
            {
                this._frame.Navigated -= this.Frame_Navigated;
                this._frame.NavigationFailed -= this.Frame_NavigationFailed;
            }
        }

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e) => this.NavigationFailed?.Invoke(sender, e);

        private void Frame_Navigated(object sender, NavigationEventArgs e) => this.Navigated?.Invoke(sender, e);
    }
}
