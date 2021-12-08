import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    const{id,recycleBin,isBin } = req.body;
    


    
        await pool.request()
                    .input("RecycleCodeID",id)
                    .input("RecycleBin",recycleBin)
                    .input("IsBin",isBin)
                    .execute("setRecycleCode");

}

    

    

export default httpTrigger;