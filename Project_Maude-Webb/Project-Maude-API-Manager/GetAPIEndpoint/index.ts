import { AzureFunction, Context, HttpRequest } from "@azure/functions"

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const devSlotMasterKey = process.env.DEV_URI;
    if (devSlotMasterKey == undefined)
        throw new Error("Environment variable DEV_URI not defined");

    const prodSlotMasterKey = process.env.PROD_URI;
        if (prodSlotMasterKey == undefined)
            throw new Error("Environment variable PROD_URI not defined");
    
    const { versionCode, device } = req.body;

    
    
    
    if (versionCode == "DEV") {
        context.res = {
            // status: 200, /* Defaults to 200 */
            body: devSlotMasterKey.toString()
        };
    }
    else {
        context.res = {
            // status: 200, /* Defaults to 200 */
            body: prodSlotMasterKey.toString()
        };
    }
};

export default httpTrigger;
