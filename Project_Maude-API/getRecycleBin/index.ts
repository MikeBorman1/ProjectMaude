import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    
    // Gets a list of all warnings in the db
    const bin = await pool.request()
    .execute("getRecycleBin");
    
    const bins = bin.recordset.map((row) => ({ recycleCodeId: row["RecycleCodeID"] as number, 
                                                            recycleBin: row["RecycleBin"] as string,
                                                        }));
    context.res.end(JSON.stringify(bins));
}
export default httpTrigger;
