import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    const {id , question , accepted , rejected} = req.body;
    
    const data = accepted + '||'+ rejected;
    
    await pool.request()
    .input("Id", id)
    .input("Question", question)
    .input("Data", data)
    .execute("setRecycleInfo");

   

    
    
}
    

export default httpTrigger;
