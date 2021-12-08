import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();

    const info = await pool.request()
    .execute("getRecycleInfo");

    const result = [];

    for (const item of info.recordset){
            var data = item.Data.split('||')
        result.push({
                id: item.Id as number,
                name: item.Question as string,
                accepted: data[0] as string,
                rejected: data[1] as string
                
                
                    }
            );
    }
    context.res.end(JSON.stringify(result)); 
}
    

export default httpTrigger;
