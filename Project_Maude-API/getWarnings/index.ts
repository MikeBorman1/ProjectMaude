import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    
    // Gets a list of all warnings in the db
    const Warnings = await pool.request()
    .execute("getWarnings");
    
    const warnings = Warnings.recordset.map((row) => ({ warning: row["Warning"] as string, 
                                                            warningId: row["WarningId"] as number,
                                                        }));
    context.res.end(JSON.stringify(warnings));
}
export default httpTrigger;
