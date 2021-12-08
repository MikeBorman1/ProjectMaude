import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    
    

var results = [];
    
       const Result=  await pool.request()
                    .execute("getRecycleCodes");

                    for(const code of Result.recordset){
                        results.push({Id: code["RecycleCodeID"] as number, 
                        recycleBin: code["RecycleBin"] as string, 
                        isBin: code["IsBin"] as boolean})
                    }
                               
                context.res.end(JSON.stringify(results));

}

export default httpTrigger;