import { AzureFunction, Context, HttpRequest } from "@azure/functions"

import * as sql from "mssql";

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log('HTTP trigger function processed a request.');

    const connectionString = process.env.DB_CON;
    if (connectionString == undefined)
        throw new Error("Environment variable DB_CON not defined");

    const pool = await new sql.ConnectionPool(connectionString).connect();

    const { productGroupName } = req.body;


    if (productGroupName) {
        
            const result = await pool.request()
                .input("ProductGroupName", productGroupName)
                .execute("getInfo");

                if (result.recordsets.length == 0){
                throw new Error("Error in base api request");}

                
                context.res.end(JSON.stringify(result.recordset.map(row => ({
                productGroupName: row["ProductGroupName"] as string,
                recycleSteps: row["RecycleSteps"] as string,
                imageURLID: row["ImageURLID"] as number,
                material: row["Material"] as string,
                isRecyclable: row["IsRecyclable"] as boolean,
                materialImageID: row["MaterialImageID"] as number,
                recycleBin: row["RecycleBin"] as string,
                warning: row["Warning"] as string }
                ))))
              
        }

    else {
        context.res = {
            body: "Please pass a valid product group name"
        };
    }

    
};

export default httpTrigger;
