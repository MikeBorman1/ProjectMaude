import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    const results = [];
    
    const terms = await pool.request()
                        
                        .execute("getSearchWords");

    for(const term of terms.recordset){
        results.push({
            searchTerm: term.SearchTerm,
            frequency: term.Frequency
        });
    }

    context.res.end(JSON.stringify(results));

}
export default httpTrigger;
