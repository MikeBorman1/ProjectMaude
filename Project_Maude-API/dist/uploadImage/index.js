"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const storage = require("azure-storage");
const httpTrigger = function (context, req) {
    return __awaiter(this, void 0, void 0, function* () {
        context.log('HTTP trigger function processed a request.');
        const connectionString = process.env.STORAGE_CON;
        if (connectionString == undefined)
            throw new Error("Environment variable STORAGE_CON not defined");
        const { photo, guid } = req.body;
        var blobPath = "https://inthebag.blob.core.windows.net/app-images/products/" + guid;
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
    });
};
exports.default = httpTrigger;
//# sourceMappingURL=index.js.map