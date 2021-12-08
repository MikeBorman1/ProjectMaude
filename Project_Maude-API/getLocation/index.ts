import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();
    const{id} = req.body;
    

    const results = [];
    // Gets a list of all locations in the db
    
    const locationResult = await pool.request()
                .input("Id",id)
                .execute("getLocation");

        for(const location of locationResult.recordset){
            results.push({latitude: location["Latitude"] as number, 
            longitude: location["Longitude"] as number, 
            locationName: location["LocationName"] as string})
        };
                   
    context.res.end(JSON.stringify(results));
};

    
    

export default httpTrigger;
