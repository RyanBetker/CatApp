using Windows8_MyFirstApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.Storage.Pickers;
using Windows.Storage;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Windows8_MyFirstApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private FrameworkElementAnimator kittyAnimator; 
        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MainPage()
        {
            this.InitializeComponent();
            kittyAnimator = new FrameworkElementAnimator(imgKitty);

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void pageTitle_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void Image_GotFocus(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("Meow!!");
            await msg.ShowAsync();
        }

        private async void TestButton_Click(object sender, RoutedEventArgs e)
        {
            var thing = new WebRequestorThing();
           
            var success = await thing.GetEventsAsync();

            var msg = new MessageDialog("Done.");
            var result = await msg.ShowAsync();

        }

        private void imgKitty_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            //move the img from right then back to left
            
        }

        private async void imgKitty_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MovePicture();

            //play sound.
            //FileOpenPicker filePicker = new FileOpenPicker();
            //filePicker.FileTypeFilter.Add(".mp3");

            Exception exThrown = null;
            try
            {
                //or play a random one            
                var appPath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                var soundsPath = Path.Combine(appPath, "Assets\\Cat sounds");
                var folderForSounds = await StorageFolder.GetFolderFromPathAsync(soundsPath);

                //these options didn't work for me. ex thrown: Access denied.
                //var folderForSounds2 = await StorageFolder.GetFolderFromPathAsync("\\Assets\\Cat sounds");
                //var folderForSounds3 = await StorageFolder.GetFolderFromPathAsync("Assets\\Cat sounds");
                //var filesForSounds = await var folderForSounds = await StorageFolder.GetFolderFromPathAsync(Path.Combine(appPath, "\\Assets\\Cat sounds"));

                var soundFiles = await folderForSounds.GetFilesAsync();
                var soundToPlay = soundFiles.OrderBy(f => Guid.NewGuid()).First();

                mediaElement.SetSource(await soundToPlay.OpenAsync(FileAccessMode.Read), "mp3");
                mediaElement.Play();
            }
            catch (Exception ex)
            {
                exThrown = ex;
            }

            if (exThrown != null)
            {
                await new MessageDialog(exThrown.Message).ShowAsync();
            }
        }

        private void MovePicture()
        {
            if (kittyAnimator.CurrentDirection == FrameworkElementAnimator.Direction.Left)
            {
                kittyAnimator.MoveToRightBorder(movementSize: 1);
            }
            else
            {
                kittyAnimator.MoveToLeftBorder(movementSize: 1);
            }
        }

    }
}
