using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CPMA_Core_APP.Utils
{
    public static class CameraUtils
    {
        public static async Task<string> TakePicture(IPageDialogService dialogService)
        {
            var initialized = await CrossMedia.Current.Initialize();
            if (!initialized)
            {
                await dialogService.DisplayAlertAsync("Error", "Unable to initialize camera", "Ok");
                return null;
            }

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await dialogService.DisplayAlertAsync("Error", "Device is unable to take photos", "Ok");
                return null;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                CompressionQuality = 30
            });

            if (file == null)
                return null;
            else
            {
                return file.Path;
            }
        }
        public static async Task<string> LoadPicture(IPageDialogService dialogService)
        {
            var initialized = await CrossMedia.Current.Initialize();
            if (!initialized)
            {
                await dialogService.DisplayAlertAsync("Error", "Unable to initialize camera", "Ok");
                return null;
            }

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await dialogService.DisplayAlertAsync("Error", "Your device does not currently support this functionality", "Ok");
                return null;
            }

            var file = await LoadPhotoAsync();

            if (file == null)
                return null;
            else
            {
                return file;
            }
        }

        public static async Task<string> TakePhotoAsync()
        {
            try
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    DefaultCamera = CameraDevice.Rear,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2048
                });

                if (file == null)
                {
                    return null;
                }

                // rotate and resave the image to avoid iOS metadate rotation death
                var rotatedPath = file.Path + "-Rotated";
                using (var fs = new FileStream(rotatedPath, FileMode.Create, FileAccess.Write))
                {
                    await file.GetStreamWithImageRotatedForExternalStorage().CopyToAsync(fs);
                }
                await Task.Run(() => File.Delete(file.Path));

                return rotatedPath;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return null;
            }
        }

        public static async Task<string> LoadPhotoAsync()
        {
            try
            {
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2048
                });

                if (file == null)
                {
                    return null;
                }

                // rotate and resave the image to avoid iOS metadate rotation death
                var rotatedPath = file.Path + "-Rotated";
                using (var fs = new FileStream(rotatedPath, FileMode.Create, FileAccess.Write))
                {
                    await file.GetStreamWithImageRotatedForExternalStorage().CopyToAsync(fs);
                }
                //removed line to delete photo. it was already in their gallery

                return rotatedPath;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return null;
            }
        }
    }
}
