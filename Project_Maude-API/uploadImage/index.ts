import { AzureFunction, Context, HttpRequest } from "@azure/functions"
import path = require('path');
import storage = require('azure-storage');


const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');
   

    const connectionString = process.env.STORAGE_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable STORAGE_CON not defined");


    const {photo, guid} = req.body;
    var blobPath = "https://inthebag.blob.core.windows.net/app-images/products/"+guid;
    //Create connection service
    //-----------------------
    const blobService = storage.createBlobService(connectionString);

    //input file is stream readable. this defines the storage function
    //-----------------------
    // const createPhoto = async (stream: string.readable, blobPath: string): Promise<string> => {
        
    //     const length = await streamLength(stream);

    //     //await async resut of storing file in "Photos" folder
    //     //-----------------------
    //     await new Promise((resolve, reject) => {
    //         blobService.createBlockBlobFromStream(
    //             "photos",
    //             blobPath,
    //             stream,
    //             length,
    //             (err: any) => err ? reject(err) : resolve()
    //         );
    //     });

    //     return blobPath;
    // }

    //call create photo function
    //-----------------------
    //const endPhotoPath = await createPhoto(photo, blobPath);

};

export default httpTrigger;
