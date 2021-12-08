using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using CPMA_Core_APP.Utils.Interfaces;

namespace CPMA_Core_APP.Utils
{
    public class UpvoteStore : IUpvoteStore
    {

        //Task to check if there are any stored upvotes on the phone. returns a list of Report Ids that the user has upvoted.
        public async Task<List<int>> GetUpvoteData()
        {
            //Check if device type is virtual. Only for emultors.
            if (DeviceInfo.DeviceType != DeviceType.Virtual)
            {
                //Creates a file path to store the upvotes in
                var jsonPath = Path.Combine(Path.Combine(FileSystem.AppDataDirectory, "Upvotes"));
                //Checks if the file already exists
                if (!File.Exists(jsonPath))
                {
                    //Creates the file where the Upvotes are going to be stored
                    var upvoteFile = File.Create(Path.Combine(FileSystem.AppDataDirectory, "Upvotes"));
                    Debug.WriteLine("File Created");
                    //Creates a new null list with one value of 0 in it. This is to set the data type of the JSON object. 
                    List<int> NullList = new List<int>();
                    NullList.Add(0);
                    upvoteFile.Close();
                    //Converts the Null list into a Serialized JSON object that can be saved to the file
                    var json = JsonConvert.SerializeObject(NullList);
                    //This Null list is then writen to the File.
                    File.WriteAllText(jsonPath, json);
                }
                //Reads the data from the file path
                var data = File.ReadAllText(jsonPath);
                //Changes it from a Serialized JSON object into a list of integers.
                var decoded = JsonConvert.DeserializeObject<List<int>>(data);
                return decoded;
            }
            List<int> VirtualList = new List<int>();
            VirtualList.Add(0);
            return VirtualList;
        }

        public async Task Upvote(int rpId)
        {

            if (DeviceInfo.DeviceType != DeviceType.Virtual)
            {
                //Takes the data already stored
                var UpvoteList = await GetUpvoteData();
                //Adds the report Id to the list
                UpvoteList.Add(rpId);
                //Converts list back into a JSON object
                var json = JsonConvert.SerializeObject(UpvoteList);
                //Writes the list to the File overwrites the previous data in the file
                await Task.Run(() => File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, "Upvotes"), json));
            }

        }

        public async Task Downvote(int rpId)
        {
            if (DeviceInfo.DeviceType != DeviceType.Virtual)
            {
                //Takes the data already stored
                var UpvoteList = await GetUpvoteData();
                //Removes the report id from the list
                UpvoteList.Remove(rpId);
                //Converts list back into JSON object
                var json = JsonConvert.SerializeObject(UpvoteList);
                //Writes the list to the File overwrites the previous data in the file
                await Task.Run(() => File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, "Upvotes"), json));
            }
        }
    }
}