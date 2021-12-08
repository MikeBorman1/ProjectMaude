using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CPMA_Core_APP.Utils
{
    public static class PermissionsUtils
    {
        public static object DialogService { get; private set; }

        public static async Task<bool> RequestPermission(Permission permission, IPageDialogService dialogService)
        {
            try
            {
                return await DevicePermissionManager.Instance.RequestPermissionAsync(permission, async (result) =>
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                    if (status != PermissionStatus.Granted)
                    {
                        var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                        if (results.ContainsKey(permission))
                        {
                            status = results[permission];
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                await dialogService.DisplayAlertAsync("Error", ex.ToString(), "Ok");
                Crashes.TrackError(ex);
                return false;
            }
        }

        public static async Task<bool> CheckPermission(Permission permission)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (status == PermissionStatus.Granted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //Device Manager found on https://github.com/jamesmontemagno/PermissionsPlugin/issues/85 to fix plugin issue
    //When RequestPermissionAsync is called multiple times in quick succession (before the previous call finishes) the task is cancelled
    //This causes the await to crash because of a cancelled task
    public class DevicePermissionManager
    {

        private class RequestPermissionQueueItem
        {
            public Permission Permission { get; set; }
            public Action<Dictionary<Permission, PermissionStatus>> OnPermissionResultCallBack { get; set; }
        }

        public static DevicePermissionManager Instance = new DevicePermissionManager();

        private List<RequestPermissionQueueItem> permissionsToBeRequested = new List<RequestPermissionQueueItem>();
        private bool isBusy;
        private int currentPermissionRequest = -1;

        private DevicePermissionManager()
        {

        }

        private async Task<bool> ExecuteNextPendingPermissionRequests()
        {
            return await RequestPermissionAsync(permissionsToBeRequested[currentPermissionRequest].Permission,
                permissionsToBeRequested[currentPermissionRequest].OnPermissionResultCallBack);
        }

        public async Task<bool> RequestPermissionAsync(Permission permission, Action<Dictionary<Permission, PermissionStatus>> onPermissionResult)
        {

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (status == PermissionStatus.Granted)
            {
                Dictionary<Permission, PermissionStatus> result = new Dictionary<Permission, PermissionStatus>
                {
                    { permission, PermissionStatus.Granted }
                };
                onPermissionResult?.Invoke(result);
                return true;
            }

            if (isBusy)
            {
                RequestPermissionQueueItem requestPermissionQueueItem = new RequestPermissionQueueItem
                {
                    Permission = permission,
                    OnPermissionResultCallBack = onPermissionResult
                };
                permissionsToBeRequested.Add(requestPermissionQueueItem);
                return false;
            }

            isBusy = true;
            Dictionary<Permission, PermissionStatus> results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
            onPermissionResult?.Invoke(results);
            isBusy = false;

            currentPermissionRequest++;
            if (currentPermissionRequest < permissionsToBeRequested.Count)
            {
                return await ExecuteNextPendingPermissionRequests();
            }
            else
            {
                currentPermissionRequest = -1;
                permissionsToBeRequested = new List<RequestPermissionQueueItem>();
                return false;
            }
        }
    }
}
